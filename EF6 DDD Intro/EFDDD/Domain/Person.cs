using System;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace EFDDD.Domain
{
    [DebuggerDisplay("Id = {Id}, PersonalName = {PersonalName.Value}")]
    public class Person
    {
        protected Person()
        {
            /* intentionally left blank */
        }

        public Person(PersonalName personalName, Address address)
        {
            PersonalName = personalName;
            Address = address;
        }

        [Key]
        public int Id { get; set; }

        public PersonalName PersonalName { get; private set; }
        public Address Address { get; private set; }

        public void Relocate(Address relocateToAddress)
        {
            if (Address == relocateToAddress)
                throw new ArgumentException("The new address must not be equal to the current address","relocateToAddress");

            Address = relocateToAddress;
        }

        public void Rename(PersonalName newName)
        {
            if (PersonalName == newName)
                throw new ArgumentException("The new name must not be equal to the current name","newName");

            PersonalName = newName;
        }
    }
}