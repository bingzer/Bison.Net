using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bingzer.Bison.Commons
{
    /// <summary>
    /// Mock Json
    /// </summary>
    public class MockJson : Json
    {
        internal MockJson(Json parent)
            : base(parent)
        {
            // do nothing
        }

        public override Json Pair(string name)
        {
            return this;
        }

        public override Json Pair(string name, object value)
        {
            return this;
        }

        public override string ToString(string tabChar, string lineChar)
        {
            return Constants.EMPTY;
        }

        internal override Json Parse(string jsonText)
        {
            return this;
        }
    }
}
