using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bingzer.Bison.Commons;

namespace Bingzer.Bison
{
    /// <summary>
    /// Represents all Values..
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonValue", Browsable = false)]
    public abstract class JsonValue : Json
    {
        /// <summary>
        /// New instance of json Value
        /// </summary>
        /// <param name="json"></param>
        internal JsonValue(Json json)
            : base(json)
        {
            // nothing
        }

        /// <summary>
        /// Pair
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override Json Pair(string name)
        {
            return Pair(name, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public override Json Pair(string name, object Value)
        {
            return this;
        }
    }
}

namespace Bingzer.Bison{

    /// <summary>
    /// String..
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonString", Browsable = false)]
    public class JsonString : JsonValue
    {
        #region Code
        /// <summary>
        /// New instance
        /// </summary>
        /// <param name="json"></param>
        internal JsonString(Json json) 
            : base(json)
        {
            // nothing
        }

        /// <summary>
        /// Parses json text
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            if (jsonText.Length > 1)
                Value = jsonText.Substring(1, jsonText.Length - 1);
            else Value = jsonText;

            // look for escape characters
            // make sure they escape that character lol!
            for (int i = 0; i < jsonText.Length - 1; i++)
            {
                if (jsonText[0] == Constants.QUOTE || jsonText[i] == Constants.DOUBLE_QUOTE)
                    if (jsonText[i - 1] != Constants.SLASH)
                        throw new JsonException("Escape character is expected: " + jsonText[i] + " " + Value);
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
            return new StringBuilder(Constants.DOUBLE_QUOTE)
                        .Append(Value).Append(Constants.DOUBLE_QUOTE).ToString();
        }
        #endregion
    }// end String.class


    /// <summary>
    /// Number
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonNumber", Browsable = false)]
    public class JsonNumber : JsonValue
    {
        #region Code
        /// <summary>
        /// New instnace of Number
        /// </summary>
        /// <param name="json"></param>
        internal JsonNumber(Json json)
            : base(json)
        {
            // do nothing
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            Value = JsonUtil.ToNumber(jsonText);
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
            return Value.ToString();
        }
        #endregion
    }// end Number.class

    /// <summary>
    /// Json boolean
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonBoolean", Browsable = false)]
    public class JsonBoolean : JsonValue
    {
        #region Code
        /// <summary>
        /// New instnace of Number
        /// </summary>
        /// <param name="json"></param>
        internal JsonBoolean(Json json)
            : base(json)
        {
            // do nothing
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            Value = JsonUtil.ToBoolean(jsonText);
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
            return Value.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Date
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonDate", Browsable = false)]
    public class JsonDate : JsonValue
    {
        #region Code
        /// <summary>
        /// New instnace of Number
        /// </summary>
        /// <param name="json"></param>
        internal JsonDate(Json json)
            : base(json)
        {
            // do nothing
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
            Value = JsonUtil.ToDate(jsonText);
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
            builder.Append(Constants.NEW).Append(Constants.SPACE)
                .Append(Constants.DATE).Append(Constants.PARENTHESE_START)
                .Append(((DateTime)Value).Ticks)
                .Append(Constants.PARENTHESE_END);

            return builder.ToString();
        }
        #endregion
    }

    /// <summary>
    /// Represents null
    /// </summary>
    [Bingzer.Bison.Commons.Entity(Name = "JsonNull", Browsable = false)]
    public class JsonNull : JsonValue
    {
        #region Code
        /// <summary>
        /// New instnace of Number
        /// </summary>
        /// <param name="json"></param>
        internal JsonNull(Json json)
            : base(json)
        {
            // do nothing
        }

        /// <summary>
        /// Parses
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        internal override Json Parse(string jsonText)
        {
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
            return Constants.NULL.ToString();
        }
        #endregion
    }
}
