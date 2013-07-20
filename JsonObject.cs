using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bingzer.Bison.Commons;

namespace Bingzer.Bison
{
    /// <summary>
    /// Represents json object. Starts with { and ends with }
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonObject", Browsable = false)]
    public class JsonObject : Json
    {
        internal JsonObject(Json parent)
            : base(parent)
        {
            Value = new List<Json>();
        }

        /// <summary>
        /// Find pair with name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Json Pair(string name)
        {
            Json json = null;
            foreach (Json val in ((List<Json>)Value))
            {
                if (json == null)
                {
                    json = val.Pair(name);
                }
                else break;
            }

            // return json..
            return json;
        }// end pair()

        /// <summary>
        /// Pair
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override Json Pair(string name, object Value)
        {
            Json json = new JsonPair(this);
            json.Name = name;
            json.Value = Jsonify(json, Value);

            // adds to list..
            ((List<Json>)this.Value).Add(json);

            // returns the newly created
            return json;
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            jsonText = jsonText.Trim();
        
            // must start with { and end with }
            if(!jsonText.StartsWith(Constants.START.ToString()) && !jsonText.EndsWith(Constants.END.ToString()))
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
                else if(!insideString && c == Constants.END && (i != jsonText.Length - 1)) startCounter--;
                else if(!insideString && c == Constants.ARRAY_START) arrayCounter++;
                else if(!insideString && c == Constants.ARRAY_END) arrayCounter--;
            
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
                Json val = (Json) Jsonify(this, text);
                ((List<Json>)this.Value).Add(val);
            }
        
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
        
            //<editor-fold defaultstate="collapsed" desc="Code">
            int rootCount = tabChar == null && lineChar == null ? 0 : RootCount();
        
            // empty both if null so we don't get 'null' string
            if(lineChar == null) lineChar = Constants.EMPTY; // empty out
            if(tabChar == null)  tabChar  = Constants.EMPTY;  // empty out
        
            if(!(Parent is JsonArray)) builder.Append(lineChar);
            for(int j = 0; j < rootCount - 1; j++) 
                builder.Append(tabChar);
            builder.Append(Constants.START).Append(lineChar);
            if(Value != null && ((List<Json>)Value).Count > 0){
                for(int i = 0; i < ((List<Json>)Value).Count; i++){
                    Json child = ((List<Json>)Value)[i];
                
                    for(int j = 0; child is JsonValue && j < rootCount + 1; j++) 
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
            builder.Append(Constants.END);
            //</editor-fold>
        
            return builder.ToString();
        }
    }// end JsonObject.cs
}
