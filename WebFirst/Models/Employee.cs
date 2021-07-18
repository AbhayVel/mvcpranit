using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFirst.Models
{
    public class Employee
    {

    
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public float Tax { get; set; }

        public Address Address { get; set; }

        public List<Address> OtherAddress { get; set; }
    }

    public class Address
    {
        public string HomeAddress { get; set; }

        public string OfficeAddress { get; set; }

        public Country Country { get; set; }
    }

    public class Country
    {
        public int Id { get; set; }

        public string  Name { get; set; }
    }
}
