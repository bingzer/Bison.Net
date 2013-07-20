using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bingzer.Bison.Commons;

namespace Bingzer.Bison
{
    /// <summary>
    /// Represents json array
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonArray", Browsable = false)]
    public class JsonArray : Json
    {

        /// <summary>
        /// New instance
        /// </summary>
        /// <param name="parent"></param>
        internal JsonArray(Json parent)
            : base(parent)
        {
            Value = new List<Json>();
        }

        /// <summary>
        /// Find pair
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Json Pair(string name)
        {
            Json p = null;
            foreach (Json json in ((List<Json>)Value))
            {
                if (p == null)
                {
                    p = json.Pair(name);
                    if (p != null && p.Name == name)
                        break;
                    else p = null; // reset
                }// end if p is null
            }

            // return p
            return p;
        }

        /// <summary>
        /// Creates pair with name + Value
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override Json Pair(string name, object Value)
        {
            // creates an object parent first..
            Json json = new JsonObject(this);
            ((List<Json>)this.Value).Add(json);
            // return the newly created pair
            return json.Pair(name, Value);
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            jsonText = jsonText.Trim();
        
            // must start with [ and end with ]
            if(!jsonText.StartsWith(Constants.ARRAY_START.ToString()) && !jsonText.EndsWith(Constants.END.ToString()))
                SyntaxError(this, jsonText);
        
            bool insideString = false;
            char c;
        
            // split by comma..
            List<string> Values = new List<string>(); 
            StringBuilder parsedText = new StringBuilder();
            bool doneParsing = false;
            int startCounter = 0;
            int arrayCounter = 0;
            // we're parsing inside [ & ] not including those brackets
            for(int i = 1; i < jsonText.Length; i++){
                c = jsonText[i];
            
                if((c == Constants.QUOTE || c == Constants.DOUBLE_QUOTE) && jsonText[i - 1] != Constants.SLASH) insideString = !insideString;
                else if(!insideString && c == Constants.START) startCounter++;
                else if(!insideString && c == Constants.END) startCounter--;
                else if(!insideString && c == Constants.ARRAY_START)  arrayCounter++;
                else if(!insideString && c == Constants.ARRAY_END && (i != jsonText.Length - 1)) arrayCounter--;
            
                // if it's not inside string, object or other array
                if(!insideString && startCounter == 0 && arrayCounter == 0){
                    if(c == Constants.COMMA || i == jsonText.Length - 1){
                        doneParsing = true;
                    }
                }
                if(!doneParsing)
                    parsedText.Append(c);
            
                // if done parsing...
                if(doneParsing){
                    // add parsedText..
                    Values.Add(parsedText.ToString());
                    // empty parsedText
                    parsedText.Clear();
                    doneParsing = false;
                }
            }// end for..
        
            //if(!valid)
                //syntaxError(this, jsonText);
            if(Values.Count == 0)
                SyntaxError(this, jsonText);
        
            // now.. iterate all Values..
            foreach(string text in Values){
                Json val = Jsonify(this, text);
                ((List<Json>)Value).Add(val);
            }
        
            // return this
            return this;
        }

        /// <summary>
        /// To string
        /// </summary>
        /// <param name="tabChar"></param>
        /// <param name="lineChar"></param>
        /// <returns></returns>
        public override string ToString(string tabChar, string lineChar)
        {
            StringBuilder builder = new StringBuilder();

            #region Code
            int rootCount = tabChar == null && lineChar == null ? 0 : RootCount();
        
            // empty both if null so we don't get 'null' string
            if(lineChar == null) lineChar = Constants.EMPTY; // empty out
            if(tabChar == null)  tabChar  = Constants.EMPTY;  // empty out
        
            // if no name..
            if(!(Parent is JsonArray)) builder.Append(lineChar);
            for(int j = 0; j < rootCount - 1; j++) 
                builder.Append(tabChar);
            builder.Append(Constants.ARRAY_START).Append(lineChar);
            if(Value != null && ((List<Json>)Value).Count > 0){
                for(int i = 0; i < ((List<Json>)Value).Count; i++){
                    Json child = ((List<Json>)Value)[i];
                
                    for(int j = 0; child is JsonValue && j < rootCount; j++) 
                        builder.Append(tabChar);
                    builder.Append(child.ToString(tabChar, lineChar));
                    if(i != ((List<Json>)Value).Count - 1){
                        builder.Append(Constants.COMMA);
                        builder.Append(lineChar);
                    }

                }
                builder.Append(lineChar);
            }
            for(int j = 0; j < rootCount - 1; j++) builder.Append(tabChar);
            builder.Append(Constants.ARRAY_END);
            #endregion

            return builder.ToString();
        }

    }
}
