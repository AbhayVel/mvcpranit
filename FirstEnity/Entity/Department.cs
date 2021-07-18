using System;
using System.Collections.Generic;

#nullable disable

namespace FirstEnity.Entity
{
    public partial class Department
    {
        public Department()
        {
            Employees = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Employees { get; set; }
    }
}
