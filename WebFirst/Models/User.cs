using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebFirst.Models
{
    public class User2
    {


        [Required]
        [Range(3, 78)]
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Salary { get; set; }

        public float Tax { get; set; }

        public Address Address { get; set; }
    }
}
