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
            new AddrType { AddrTypeID = 3, Type = "Mailing" },
            new AddrType { AddrTypeID = 4, Type = "Corporate" });

         modelBuilder.Entity<Person>()
            .HasData(
            new Person { PersonID = 1, FirstName = "Health", LastName = "Catalyst", Birthday = DateTime.Parse("2008-04-12") },
            new Person { PersonID = 2, FirstName = "John", LastName = "Browning", Birthday = DateTime.Parse("1855-01-23") },
            new Person { PersonID = 3, FirstName = "Gary", LastName = "Coleman", Birthday = DateTime.Parse("1968-02-08") },
            new Person { PersonID = 4, FirstName = "Sarah", LastName = "Palin", Birthday = DateTime.Parse("1964-02-11") },
            new Person { PersonID = 5, FirstName = "Frank", LastName = "Church", Birthday = DateTime.Parse("1924-07-25") });

        modelBuilder.Entity<Address>()
            .HasData(
            new Address { PersonID = 1, AddressID = 1, Street1 = "10897 South River Front Parkway", Street2 = "#300 South", City = "Jordon", State = "UT", Zip = "84095", AddrTypeID = 4 },
            new Address { PersonID = 2, AddressID = 2, Street1 = "358 South 700 East", Street2 = "Suite A", City = "Salt Lake City", State = "UT", Zip = "84102", AddrTypeID = 1 },
            new Address { PersonID = 2, AddressID = 3, Street1 = "62 W Bulldog Blvd", City = "Provo", State = "UT", Zip = "84604", AddrTypeID = 3 },
            new Address { PersonID = 3, AddressID = 4, Street1 = "4245 S. Riverdale Rd", Street2 = "Ste D", City = "Ogden", State = "UT", Zip = "84405", AddrTypeID = 1 },
            new Address { PersonID = 4, AddressID = 5, Street1 = "Po Box 674", City = "Ashton", State = "ID", Zip = "83420", AddrTypeID = 3 },
            new Address { PersonID = 5, AddressID = 6, Street1 = "170 Plummer Rd", City = "Chalis", State = "ID", Zip = "83226", AddrTypeID = 3 },
            new Address { PersonID = 5, AddressID = 7, Street1 = " 685 Rod and Gun Club Loop", City = "Chalis", State = "ID", Zip = "83226", AddrTypeID = 2 });

        modelBuilder.Entity<Interest>()
             .HasData(
             new Interest { InterestID = 1, Description = "Golf", PersonID = 2 },
             new Interest { InterestID = 2, Description = "Fishing", PersonID = 2 },
             new Interest { InterestID = 3, Description = "Acting", PersonID = 3 },
             new Interest { InterestID = 4, Description = "Philanthropy", PersonID = 1 },
             new Interest { InterestID = 5, Description = "Politics", PersonID = 4 },
             new Interest { InterestID = 6, Description = "River Rafting", PersonID = 5 });

    }
}
