using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkLab
{
    public class Address
    {
        public string House_Name_or_Number { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Postcode { get; set; }
        public string Country { get; set; }
        public ICollection<Person> People { get; set; }
        [Key]
        public int AddressIdentifier { get; set; }
        public Address() { }    
    }
}
