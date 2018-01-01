// IronMeta Copyright © Gordon Tisher 2018

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronMeta.UnitTests.Matcher
{
    public class AnonInputObject
    {
        public string Name { get; set; }
        public string Value { get; set; }

        public override bool Equals(object obj)
        {
            var other = obj as AnonInputObject;
            if (other == null)
                return false;

            return Name.Equals(other.Name) && Value.Equals(other.Value);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode() ^ Value.GetHashCode();
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", Name, Value);
        }
    }
}
