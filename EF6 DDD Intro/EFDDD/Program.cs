using System.Data.Entity;
using System.Linq;
using EFDDD.Domain;

namespace EFDDD
{
    public class MyContext : DbContext
    {
        public DbSet<Person> Persons { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            using (var ctx = new MyContext())
            {
                ctx.Database.Delete();

                var roger = new Person(
                    PersonalName.Of("Roger"),
                    Address.Of("Stora mellösa", "Linfrövägen 12", "T", "Sverige")
                    );

                ctx.Persons.Add(roger);

                roger.Relocate(Address.Of("Foo","Bar","X","Sverige"));
                roger.Rename(PersonalName.Of("FooBar"));

                ctx.SaveChanges();
            }
            using (var ctx = new MyContext())
            {
                Person p = ctx.Persons.First();
            }
        }
    }
}