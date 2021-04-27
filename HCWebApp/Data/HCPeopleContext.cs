using Microsoft.EntityFrameworkCore;

using HCPeopleModel;

namespace HCPeopleData
{
     public class HCPeopleContext : DbContext
     {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Seed();
        }
        public HCPeopleContext(DbContextOptions<HCPeopleContext> options)
            : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<AddrType> AddrType { get; set; }
        public DbSet<Interest> Interest { get; set; }
    }
}
