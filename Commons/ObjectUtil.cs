using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Bingzer.Bison.Commons
{
    /// <summary>
    /// Provides functions for object testings
    /// </summary>
    public class ObjectUtil
    {

        /// <summary>
        /// Check to see if any is an object of array
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsObjectArray(object any)
        {
            if (any == null) return false;
            else if (any.GetType().IsArray)
                return true;
            else if (any is ICollection)
                return true;
            return false;
        }

        public static object[] ToObjectArray(object any)
        {
            if (any == null)
                return null;
            else if (any is ICollection)
                any = new ArrayList((ICollection)any).ToArray();
            else if (any.GetType().IsPrimitive || any.GetType().GetElementType().IsPrimitive)
            {
                any = (object)Global.ToObjectArray(any);
            }
            return (object[])any;
        }

        /// <summary>
        /// Check to see if any is null
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsNull(object any)
        {
            return any == null;
        }

        /// <summary>
        /// Is a character
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsCharacter(object any)
        {
            return any is char || any is Char;
        }

        /// <summary>
        /// Is a boolean?
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsBoolean(object any)
        {
            return any is bool || any is Boolean;
        }

        /// <summary>
        /// Checks to see if number
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsNumber(object any)
        {
            return any is int || any is Int16 || any is Int32 || any is Int64 ||
                any is uint || any is UInt16 || any is UInt32 || any is UInt64 ||
                any is short || any is sbyte || any is SByte || any is byte || any is Byte ||
                any is long || any is float || any is double || any is Double;
        }

        /// <summary>
        /// Is date?
        /// </summary>
        /// <param name="any"></param>
        /// <returns></returns>
        public static bool IsDateTime(object any)
        {
            return any is DateTime || any is DateTimeOffset;
        }

    }
}
