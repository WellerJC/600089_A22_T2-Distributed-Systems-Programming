using Microsoft.Owin.BuilderProperties;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkLab
{
    public class Person
    {
        public string First_Name { get; set; }
        public string Middle_Name { get; set; }
        public string Last_Name { get; set;}
        public DateTime Date_Of_Birth { get; set; }
        public int Age { get; set; }
        public Address Address { get; set; }
        public int PersonID { get; set; }
        public Person() { }
    }
}
