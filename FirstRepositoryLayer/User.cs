using DBContextProject;
using FirstEntityModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstRepositoryLayer
{

    public interface IUserRepo
    {

        public List<FirstEnity.User> GetAllUser();

        public List<FirstEnity.User> GetAllUser(string whereCriteria);

        public FirstEnity.User GetById(int id);

        public bool DeleteById(int id);

        public  FirstEnity.User Save(FirstEnity.User user);
    }
    public class UserRepo : IUserRepo
    {

        string userDbQuery = "Select top (10) * from Employee Where 1=1 ";
        private EmployeeContext EmployeeContext { get; set; }
        public UserRepo(EmployeeContext employeeContext)
        {
            EmployeeContext = employeeContext;
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

        public bool DeleteById(int id)
        {

            var user = EmployeeContext.UserDbSet
                 .FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                EmployeeContext.UserDbSet.Remove(user);
                EmployeeContext.SaveChanges();
                return true;
            }
           
           
            return false;
        }
    }
}
