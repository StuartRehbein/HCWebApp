using System;
using Microsoft.EntityFrameworkCore;
using HCPeopleModel;
public static class ModelBuilderExtensions
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AddrType>()
            .HasData(
            new AddrType { AddrTypeID = 1, Type = "Home" },
            new AddrType { AddrTypeID = 2, Type = "Billing" },
            new AddrType { AddrTypeID = 3, Type = "Shipping" },
            new AddrType { AddrTypeID = 4, Type = "Corporate" });

         modelBuilder.Entity<Person>()
            .HasData(
            new Person { PersonID = 1, FirstName = "Health", LastName = "Catalyst", Birthday = DateTime.Parse("2008-04-12") },
            new Person { PersonID = 2, FirstName = "Trent", LastName = "Wignall", Birthday = DateTime.Parse("1986-03-22") },
            new Person { PersonID = 3, FirstName = "Pete", LastName = "Hess", Birthday = DateTime.Parse("1977-04-15") });

        modelBuilder.Entity<Address>()
            .HasData(
            new Address { PersonID = 1, AddressID = 1, Street1 = "1212 SW First St.", City = "Bolder", State = "CO", Zip = "99898", AddrTypeID = 1 },
            new Address { PersonID = 1, AddressID = 2, Street1 = "1400 Mill Bvld.", Street2 = "Suite 20A", City = "Bolder", State = "CO", Zip = "99898", AddrTypeID = 2 },
            new Address { PersonID = 2, AddressID = 3, Street1 = "342 West First", City = "Salt Lake City", State = "UT", Zip = "97703", AddrTypeID = 4 },
            new Address { PersonID = 3, AddressID = 4, Street1 = "987 Grand Ave", Street2 = "Apt 132", City = "Rexburg", State = "ID", Zip = "55465", AddrTypeID = 1 });

        modelBuilder.Entity<Interest>()
             .HasData(
             new Interest { InterestID = 1, Description = "Golf", PersonID = 2 },
             new Interest { InterestID = 2, Description = "Fishing", PersonID = 3 },
             new Interest { InterestID = 3, Description = "Skiing", PersonID = 3 },
             new Interest { InterestID = 4, Description = "Philanthropy", PersonID = 1 });

    }
}
