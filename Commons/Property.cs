using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace Bingzer.Bison.Commons
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class Property : Attribute
    {
        [DefaultValueAttribute("")]
        public string Name
        {
            get;
            set;
        }

        [DefaultValueAttribute(true)]
        public bool Browsable
        {
            get;
            set;
        }

        [DefaultValueAttribute(null)]
        public Type Type
        {
            get;
            set;
        }

        [DefaultValueAttribute(null)]
        public string Format
        {
            get;
            set;
        }
    }
}
