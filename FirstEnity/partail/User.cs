using FirstEnity.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstEnity
{



    internal interface IUser
    {
        [Display(Name ="User Id")]
        [Key]
        public int Id { get; set; }


        [Display(Name = "First Name")]
        public string FirstName { get; set; }


        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int age { get; set; }

        public  int? DeptId { get; set; }

        public  Department Dept { get; set; }
    }

    [Table(name: "Employee")]
    public partial class User : IUser
    {

       
       
    }
}
