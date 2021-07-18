using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using Newtonsoft.Json;

namespace FirstEnity
{

    [Table("LoginUser")]
    public class LoginUserIdentity :  IIdentity
    {
        [NotMapped]
        public string AuthenticationType { get; set; }


        [NotMapped]
        public bool IsAuthenticated { get; set; }

        [NotMapped]
        public string Name
        {
            get { return UserName; }
            set { UserName = value; }
        }
         
        [Key]
        public int Id { get; set; }

        public string  UserName { get; set; }


        [JsonIgnore]
        public string Password { get; set; }


        public string Role { get; set; }

        public string Address { get; set; }
    }
}
