using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace EFDDD.Domain
{
    [ComplexType]
    [DebuggerDisplay("{Value}")]
    public class PersonalName : IEquatable<PersonalName>
    {
        protected PersonalName()
        {
            /* intentionally left blank */
        }

        public PersonalName(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(value);

            Value = value;
        }

        public string Value { get; private set; }

        public static PersonalName Of(string value)
        {
            return new PersonalName(value);
        }

        #region equals
        /* All the madness below is due to C#'s inabillity to create structural equallity */

        public bool Equals(PersonalName other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Value, other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PersonalName) obj);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static bool operator ==(PersonalName left, PersonalName right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(PersonalName left, PersonalName right)
        {
            return !Equals(left, right);
        }
        #endregion
    }
}