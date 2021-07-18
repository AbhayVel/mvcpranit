using DBContextProject;
using FirstEntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using FirstEnity.Entity;

namespace FirstRepositoryLayer
{

    public interface IDeprtmentRepo
    {

        public List<Department> GetAll();

      //  public List<Department> GetAll(string whereCriteria);

       // public Department GetById(int id);

      //  public  FirstEnity.User Save(FirstEnity.User user);
    }
    public class DepartmentRepo : IDeprtmentRepo
    {

        string userDbQuery = "Select top (10) * from Employee Where 1=1 ";
        private EmployeeContext EmployeeContext { get; set; }
        public DepartmentRepo(EmployeeContext employeeContext)
        {
            EmployeeContext = employeeContext;
        }

        public List<Department> GetAll()
        {
            var deptList = EmployeeContext.Departments
                  .ToList();
            return deptList;
        }
        public List<FirstEnity.User> GetAllUser()
        {
            var userList = EmployeeContext.UserDbSet.Include(x=>x.Dept)
                  .ToList();
            return userList;
        }


        public FirstEnity.User GetById(int id)
        {
            var user = EmployeeContext.UserDbSet.Include(x => x.Dept)
                  .FirstOrDefault(x=>x.Id==id);
            return user;
        }

        public FirstEnity.User Save(FirstEnity.User user)
        {
            EmployeeContext.UserDbSet.Attach(user);
            EmployeeContext.SaveChanges();
            return user;
        }

        public List<FirstEnity.User> GetAllUser(string whereCriteria)
        {
            var userList = EmployeeContext.UserDbSet.FromSqlRaw(userDbQuery + whereCriteria)                  
                  .AsNoTracking()
                  .ToList();
            return userList;
        }

       
    }
}
