using System.ComponentModel.DataAnnotations;
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
    }
}