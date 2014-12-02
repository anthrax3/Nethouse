using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace EFDDD.Domain
{
    [ComplexType]
    [DebuggerDisplay("City = {City}, Street = {Street}, State or Province = {StateOrProvince}, Country = {Country}")]
    public class Address : IEquatable<Address>
    {
        protected Address()
        {
            // this is needed for EF to materialize the object from the database
        }

        public Address(string city, string street, string stateOrProvince = null, string country = null)
        {
            if (string.IsNullOrWhiteSpace(city))
                throw new ArgumentException("city");

            if (string.IsNullOrWhiteSpace(street))
                throw new ArgumentException("street");
            
            // we leave stateOrProvince and country as optional

            City = city;
            Street = street;
            StateOrProvince = stateOrProvince;
            Country = country;
        }

        // This has potential for even more refined types
        // Eg Country, however, refining types comes at a cost of lines of code in C#
        // In this example, we stop here
        public string City { get; private set; }
        public string Street { get; private set; }
        public string StateOrProvince { get; private set; }
        public string Country { get; private set; }

        public static Address Of(string city, string street, string stateOrProvince = null, string country = null)
        {
            return new Address(city, street, stateOrProvince, country);
        }

        #region equals
        /* All the madness below is due to C#'s inabillity to create structural equallity */
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public bool Equals(Address other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(City, other.City) && string.Equals(Street, other.Street) && string.Equals(StateOrProvince, other.StateOrProvince) && string.Equals(Country, other.Country);
        }

        public static bool operator ==(Address left, Address right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !Equals(left, right);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = City.GetHashCode();
                hashCode = (hashCode * 397) ^ Street.GetHashCode();
                hashCode = (hashCode * 397) ^ StateOrProvince.GetHashCode();
                hashCode = (hashCode * 397) ^ Country.GetHashCode();
                return hashCode;
            }
        }
        #endregion
    }
}