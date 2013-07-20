using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingzer.Bison.Commons
{
    public class Constants
    {
        public const char START = '{';
        public const char END = '}';
        public const char ARRAY_START = '[';
        public const char ARRAY_END = ']';
        public const char COMMA = ',';
        public const char COLON = ':';
        public const char DOUBLE_QUOTE = '"';
        public const char QUOTE = '\'';
        public const char SLASH = '\\';
        public const char PARENTHESE_START = '(';
        public const char PARENTHESE_END = ')';
        public const char SPACE = ' ';
        public const char TAB = '\t';

        // some keywords
        public const string TRUE = "true";
        public const string FALSE = "false";
        public const string NULL = "null";
        public const string NEW = "new";
        public const string DATE = "Date";
        public const string NEW_LINE = "\r\n";
        public const string NONE = "";
        public const string EMPTY = NONE;



        /// <summary>
        /// Object
        /// </summary>
        public const string OBJECT = "{}";

        /// <summary>
        /// Array
        /// </summary>
        public const string ARRAY = "[]";

        /// <summary>
        /// Pair
        /// </summary>
        public const string PAIR = EMPTY;
    }
}
