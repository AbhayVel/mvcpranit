using FirstEnity.Entity;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FirstEnity
{

   
    public partial class User
    {

       

        [Display(Name ="User Id")]
        [Key]
        public int Id { get; set; }


        [MinLength(5)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }



        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        public int age { get; set; }

        public virtual int? DeptId { get; set; }

        public virtual Department Dept { get; set; }
    }
}
