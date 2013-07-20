using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingzer.Bison.Commons
{
    /// <summary>
    /// Provides utility functions
    /// </summary>
    public class JsonUtil
    {

        /// <summary>
        /// Is name valid?
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsNameValid(string jsonText)
        {
            return true;
        }

        /// <summary>
        /// Check to see if jsonText is a number
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsNumber(string jsonText)
        {
            try
            {
                Double.Parse(jsonText);
            }
            catch (FormatException fe)
            {
                return false;
            }

            // is a number
            return true;
        }

        /// <summary>
        /// Check to see if jsontext is a boolean
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsBoolean(string jsonText)
        {
            return jsonText.Equals(Constants.TRUE) || jsonText.Equals(Constants.FALSE);
        }

        /// <summary>
        /// Check to see if jsonText is a JsonObject.
        /// Must start with { or }
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsJsonObject(string jsonText)
        {
            return jsonText.StartsWith(Constants.START.ToString()) && jsonText.EndsWith(Constants.END.ToString());
        }

        /// <summary>
        /// Check to see if jsonText is a JsonArray
        /// Must start with [ or ]
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsJsonArray(string jsonText)
        {
            return jsonText.StartsWith(Constants.ARRAY_START.ToString()) && jsonText.EndsWith(Constants.ARRAY_END.ToString());
        }

        /// <summary>
        /// Check to see if jsonText is date object
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsDate(string jsonText)
        {
            return jsonText.Contains(Constants.NEW) && jsonText.Contains(Constants.DATE);
        }

        /// <summary>
        /// Check to see if jsonText is a Null object (json)
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsNull(string jsonText)
        {
            return jsonText.Equals(Constants.NULL);
        }

        /// <summary>
        /// Check to see if jsonText is a Json string.
        /// Must start with (') or (") and ends with (') or (")
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool IsString(string jsonText)
        {
            // must start and end either with (') or (")

            bool isString = false;
            if (!isString)
                isString = jsonText.StartsWith(Constants.QUOTE.ToString()) && 
                    jsonText.EndsWith(Constants.QUOTE.ToString());
            if (!isString)
                isString = jsonText.StartsWith(Constants.DOUBLE_QUOTE.ToString()) && 
                    jsonText.EndsWith(Constants.DOUBLE_QUOTE.ToString());


            if (isString)
            {
                // can't be confused with pairing
                // ex: 'name':'value' <= starts and ends with (')
                char ch;
                int quoteCount = 0;
                int doubleQuoteCount = 0;
                for (int i = 1; i < jsonText.Length; i++)
                {
                    ch = jsonText[i];
                    if (ch == Constants.QUOTE && (i > 1 && jsonText[i - 1] != Constants.SLASH))
                        quoteCount++;
                    else if (ch == Constants.DOUBLE_QUOTE && (i > 1 && jsonText[i - 1] != Constants.SLASH))
                        doubleQuoteCount++;

                    if (quoteCount > 2 || doubleQuoteCount > 2)
                    {
                        isString = false;
                        break;
                    }
                }
            }

            // return..
            return isString;
        }// end isString

        /// <summary>
        /// To boolean
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static bool ToBoolean(string jsonText)
        {
            return jsonText.Equals(Constants.TRUE) ? true : false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static DateTime ToDate(string jsonText)
        {
            jsonText = jsonText.Trim();
            // 1. must start with new
            if (jsonText.StartsWith(Constants.NEW))
            {
                // 2. Find the Date keyword
                for (int i = 0; i < jsonText.Length; i++)
                {
                    if (jsonText[i] == Constants.DATE[0])
                    {
                        try
                        {
                            if (jsonText.Substring(i, i + Constants.DATE.Length).Equals(Constants.DATE))
                            {
                                // find the date...
                                StringBuilder dateValue = new StringBuilder();
                                // find the end parentheses..
                                bool parsing = false;
                                for (int j = i; j < jsonText.Length; j++)
                                {
                                    if (jsonText[j] == Constants.PARENTHESE_START || jsonText[j] == Constants.PARENTHESE_END)
                                        parsing = !parsing;
                                    else if (parsing)
                                    {
                                        dateValue.Append(jsonText[j]);
                                    }
                                }// end parsing..

                                // is number..
                                if (IsNumber(dateValue.ToString()))
                                {
                                    return new DateTime(long.Parse(dateValue.ToString()));
                                }
                            }
                        }
                        catch (FormatException e)
                        {
                            // ignore it..
                        }
                    }// end start with D
                }

            }

            // not a date!
            throw new JsonException("Unparseable date: " + jsonText);
        }

        /// <summary>
        /// Converts jsonText to number.
        /// The object/struct returned is a type of 'double'
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static double ToNumber(string jsonText)
        {
            try
            {
                return double.Parse(jsonText);
            }
            catch (FormatException fe)
            {
                throw new JsonException(fe);
            }
        }

    }// -- end JsonUtil
}
