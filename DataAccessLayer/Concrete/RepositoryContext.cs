using EntityLayer.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Communication>().HasOne(x => x.Person).WithMany(x => x.Communications).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Person> Persons { get; set; }
        public DbSet<Communication> Communications { get; set; }
    }
}
