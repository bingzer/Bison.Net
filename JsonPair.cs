using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bingzer.Bison.Commons;

namespace Bingzer.Bison
{
    /// <summary>
    /// Represents JsonPair
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonPair", Browsable = false)]
    public class JsonPair : Json
    {

        /// <summary>
        /// New instance of Json Pair
        /// </summary>
        /// <param name="json"></param>
        internal JsonPair(Json json)
            : base(json)
        {
            // do nothing
        }

        /// <summary>
        /// Look for pair
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Json Pair(string name)
        {
            if (name.Equals(Name))
                return this;
            else if (Value != null && (Value is JsonValue))
            {
                if (Name.Equals(name)) return (Json)Value;
                else return ((Json)Value).Pair(name);
            }

            // not found
            return null;
        }// end pair

        /// <summary>
        /// Pair
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override Json Pair(string name, object Value)
        {
            Name = name;
            Value = Jsonify(this, Value);
            return this;
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            StringBuilder name = new StringBuilder();
            Value = new StringBuilder();

            char ch;
            bool quoteCount = false;
            bool doubleQuoteCount = false;
            int parseToggle = 0; // 0 == name 1 == ValueOf
            for (int i = 0; i < jsonText.Length; i++)
            {
                ch = jsonText[i];

                try
                {
                    // -- if quote
                    if (ch == Constants.QUOTE)
                    {
                        if ((i > 1 && jsonText[i - 1] != Constants.SLASH) || i == 0)
                            quoteCount = !quoteCount;
                    }
                    else if (ch == Constants.DOUBLE_QUOTE)
                    {
                        if (i > 1 && jsonText[i - 1] != Constants.SLASH || i == 0)
                            doubleQuoteCount = !doubleQuoteCount;
                    }
                    else if (ch == Constants.COLON)
                    {
                        // with all these conditions
                        if (!quoteCount && !doubleQuoteCount &&
                           i > 0 && jsonText[i - 1] != Constants.SLASH &&
                           parseToggle == 0)
                        {
                            parseToggle = 1;
                            // increment by 1
                            // we don't want the stupid colon
                            i++;
                            ch = jsonText[i];
                        }
                    }

                    // -- parses according to the toggle
                    switch (parseToggle)
                    {
                        case 0: ((StringBuilder)name).Append(ch); break;
                        case 1: ((StringBuilder)Value).Append(ch); break;
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    e = null;
                    // incorrect formatted..
                    SyntaxError(this, jsonText);
                }
            }// end for..

            // assign name
            Name = name.ToString();
            // we must append ValueOf..
            if (((StringBuilder)Value).Length == 0) SyntaxError(this, jsonText);
            Value = Jsonify(this, Value);

            //</editor-fold>
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
        
            int rootCount = tabChar == null && lineChar == null ? 0 : RootCount();
            for(int i = 0; i < rootCount; i++)
                builder.Append(tabChar); 
        
            // build the string
            builder.Append(Constants.DOUBLE_QUOTE).Append(Name).Append(Constants.DOUBLE_QUOTE)
                    .Append(Constants.COLON)
                    .Append(Value is Json ? ((Json)Value).ToString(tabChar, lineChar) : Value);
        
            return builder.ToString();
        }
    }
}
