using FirstModel;
using FirstRepositoryLayer;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstService
{
  public interface IUserService
    {
        public List<User> GetAll();

        public List<User> GetAll(UserSearch userSearch);
        public List<User> GetAll(UserSearch userSearch, string columnName, string orderBy);
    }
   public class UserService : IUserService
    {
        public IUserRepo UserRepo { get; set; }
      public  UserService(IUserRepo userRepo)
        {
            UserRepo = userRepo;
        }
     public List<User>  GetAll()
        {

            return null;
        }

        public List<User> GetAll(UserSearch userSearch)
        {
            ////var u = userSearch;
            ////var where = "";
            ////for (var i = 0; i < u.UserSearchList.Count; i++)
            ////{
            ////    if (!string.IsNullOrWhiteSpace(u.UserSearchList[i].ColumnValue))
            ////    {
            ////        where = "and " + FilterData(u.UserSearchList[i].ColumnName, u.UserSearchList[i].ColumnValue.Replace("'","''"), u.UserSearchList[i].columnSearchType);
            ////    }
            ////}
        
            return null;
        }

        public List<User> GetAll(UserSearch userSearch, string columnName, string orderBy)
        {
            return null;
        }

        private string FilterData(string columnName, string value, string type)
        {
            if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(value))
            {
                return "";
            }
             if(type == "like")
            {
                return string.Format(" {0}  like '%{1}%'", columnName, value);
            } else if (type == "equals")
            {
                return string.Format(" {0}  = '{1}'", columnName, value);
            }

            return "";
        }

    }
}

