using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace HCPeopleModel
{
    public class Person
    {
        public Person()
        {
            this.Addresses = new HashSet<Address>();
            this.Interests = new HashSet<Interest>();
        }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Interest> Interests { get; set; }

    }
    public class Address
    {
        public int AddressID { get; set; }
        public int AddrTypeID { get; set; }
        [Required] 
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public int? PersonID { get; set; } //Foreign-Key property

        [ForeignKey("PersonID")]
        public virtual Person Person { get; set; } // Navigational Property 
    }
    public class AddrType
    {
        public int AddrTypeID { get; set; }
        public string Type { get; set; }
    }
    public class Interest
    {
        public int InterestID { get; set; }
        [Required] 
        public string Description { get; set; }
        public int? PersonID { get; set; } //Foreign-Key property
        public virtual Person Person { get; set; } // Navigational Property }

    }
}