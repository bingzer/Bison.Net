using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingzer.Bison
{
    /// <summary>
    /// The exception
    /// </summary>
    public class JsonException : Exception
    {
        public JsonException()
            : this((string)null)
        {
            // nothing
        }

        public JsonException(string message)
            : this(message, (Exception) null)
        {
            // nothing
        }

        public JsonException(Exception innerException)
            : this(null, innerException)
        {
            // nothing
        }

        public JsonException(string message, Exception innerException)
            : base(message, innerException)
        {
            // nothing
        }
    }
}
