using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Bingzer.Bison.Commons
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class Entity : Attribute
    {

        [DefaultValueAttribute(null)]
        public string Name
        {
            get;
            set;
        }

        [DefaultValueAttribute(false)]
        public bool Browsable
        {
            get;
            set;
        }
    }
}
