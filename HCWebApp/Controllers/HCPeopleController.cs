using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using HCPeopleModel;
using HCPeopleData;

namespace HCPeopleApi
{
    [Route("api/[controller]")]
    [ApiController]
    public class HCPeopleController : ControllerBase
    {
        private readonly HCPeopleContext _context;

        public HCPeopleController(HCPeopleContext context)
        {
            _context = context;
        }

        // GET: api/People
        //       [HttpGet]
//               public async Task<ActionResult<IEnumerable<Person>>> GetPerson()
//               {
//                   return await _context.Person.ToListAsync();
//               }
        // GET: api/People/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Person>> GetPerson(string name)
        {
            var person = await _context.Person
                    .Where(p => p.LastName == name)
                    .FirstOrDefaultAsync<Person>();

            if (person == null)
            {
                person = await _context.Person
                    .Where(p => p.FirstName == name)
                    .FirstOrDefaultAsync<Person>();
            }
            if (person == null)
            {
                return NotFound();
            }
            else
            {
                //TODO: could be multiples so check for them
                return GetFullPerson(person.PersonID);
            }
        }

        // PUT: api/People/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
 //       [HttpPut("{id}")]
 //       public async Task<IActionResult> PutPerson(int id, Person person)
 //       {
 //           //ToDo Validate Person
 //           if (id != person.PersonID)
 //           {
 //               return BadRequest();
 //           }

 //           _context.Entry(person).State = EntityState.Modified;

 //           try
 //           {
 //               await _context.SaveChangesAsync();
 //           }
 //           catch (DbUpdateConcurrencyException)
 //           {
 //               if (!PersonExists(id))
 //               {
 //                   return NotFound();
 //               }
 //               else
 //               {
 //                   throw;
 //               }
 //           }
 //
 //           return NoContent();
 //       }

        // POST: api/People
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Person>> PostPerson(Person person)
        {
            //ToDo Validate person
            var newperson = new Person();
            //This is add not update - personID must be empty
            if (person.PersonID != 0) return BadRequest();
            else
            {
                //All addresses are new when adding a person
                var addresses = person.Addresses.ToList();
                foreach (var address in addresses)
                {
                    if (address.AddressID != 0) return BadRequest();
                }
                if (!AddressTypeExists(addresses[0].AddrTypeID)) return BadRequest();
                {
                    var interests = person.Interests.ToList();
                    foreach(var interest in interests)
                    {
                        //TODO implement many to many relationship between People and Interests
                        //currently only allowing new interests to be added
                        if (interest.InterestID != 0) return BadRequest();
                    }
                    //if (!InterestTypeExists(interests[0].InterestID)) return BadRequest();
                    //else
                    {
                        //TODO implement transations
                        //ObjectContext objectContext = ((IObjectContextAdapter)_context).ObjectContext;
                        //objectContext.Connection.Open();
                        //using (var transaction = objectContext.Connection.BeginTransaction())
                        {

                            //newperson.PersonID = person.PersonID;
                            newperson.FirstName = person.FirstName;
                            newperson.LastName = person.LastName;
                            newperson.Birthday = person.Birthday;

                            _context.Person.Add(newperson);
                            _context.SaveChanges();
                            
                            foreach (var addr in addresses)
                            {
                                var newaddr = new Address();
                                newaddr.PersonID = newperson.PersonID;
                                newaddr.AddrTypeID = addr.AddrTypeID;
                                newaddr.Street1 = addr.Street1;
                                newaddr.Street2 = addr.Street2;
                                newaddr.City = addr.City;
                                newaddr.State = addr.State;
                                newaddr.Zip = addr.Zip;

                                _context.Address.Add(newaddr);
                                await _context.SaveChangesAsync();
                            }

                            foreach (var intrest in interests)
                            {
                                //TODO implement many to many relationship between People and Interests
                                //currently only allowing new interests to be added
                                var newinterest = new Interest();
                                newinterest.PersonID = newperson.PersonID;
                                newinterest.Description = intrest.Description;

                                _context.Interest.Add(newinterest);
                                await _context.SaveChangesAsync();
                            }
                            //TODO Implement Transactions
                            //objectContext.Connection.Close();
                        }
                    }
                }
            }
            return GetFullPerson(newperson.PersonID);
        }

        // DELETE: api/People/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeletePerson(int id)
//        {
//            return BadRequest(); //should be not found
//            var person = await _context.Person.FindAsync(id);
//            if (person == null)
//            {
//                return NotFound();
//            }

//            _context.Person.Remove(person);
//            await _context.SaveChangesAsync();

//            return NoContent();
//        }

        private bool PersonExists(int id)
        {
            return _context.Person.Any(e => e.PersonID == id);
        }
        private bool AddressExists(int id)
        {
            return _context.Address.Any(e => e.AddressID == id);
        }
        private bool AddressTypeExists(int id)
        {
            return _context.AddrType.Any(e => e.AddrTypeID == id);
        }
        private bool InterestTypeExists(int id)
        {
            return _context.Interest.Any(e => e.InterestID == id);
        }
        private Person GetFullPerson(int id)
        {
            if (PersonExists(id))
            {
                //get the person
                var person = _context.Person
                    .Where(p => p.PersonID == id)
                    .FirstOrDefault<Person>();
                //get all of the person's addresses
                var personaddresses = _context.Address
                    .Where(a => a.PersonID == person.PersonID)
                    .ToList();
                //save the address in the person's address collection
                foreach (var address in personaddresses)
                {
                    person.Addresses.Add(address);
                }
                //get all of the person's interests

                var personinterests = _context.Interest
                    .Where(pi => pi.PersonID == person.PersonID)
                    .ToList();
                
                foreach (var pi in personinterests)
                {
                    //now that we have a list of their interests we need to get the actual interests
                    var interests = _context.Interest
                        .Where(ip => ip.InterestID == pi.InterestID)
                        .ToList();
                    //save the interest in the person's interest collection
                    foreach (var interest in interests)
                    {
                        person.Interests.Add(interest);
                    }
                }
                return person;
            }
            else return null;
        }
    }
}
