using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FirstService
{

    public class Search
    {

        public string ColumnSearchKey { get; set; }
        public string ColumnName { get; set; }

        public string ColumnType { get; set; }

        public string ColumnValue { get; set; }

        public string columnSearchType { get; set; }


    }
    public class UserSearch
    {


        public List<Search> UserSearchList { get; set; }

        public UserSearch()
        {
            UserSearchList = new List<Search>();
            UserSearchList.Add(new Search { ColumnSearchKey = "Id", ColumnName = "Id", ColumnType = "number", ColumnValue = string.Empty, columnSearchType="equals" });
            UserSearchList.Add(new Search { ColumnSearchKey = "FirstName", ColumnName = "FirstName", ColumnType = "string", ColumnValue = string.Empty, columnSearchType = "like" });
            UserSearchList.Add(new Search { ColumnSearchKey = "LastName", ColumnName = "LastName", ColumnType = "string", ColumnValue = string.Empty, columnSearchType = "like" });
            UserSearchList.Add(new Search { ColumnSearchKey = "AgeGte", ColumnName = "age", ColumnType = "numberGte", ColumnValue = string.Empty, columnSearchType = "equals" });


        }


        public void SetValue(HttpRequest httpRequest)
        {

            if (httpRequest.Method == "GET")
            {
                return;
            }
            foreach (var key in httpRequest.Form.Keys)
            {
                var search = UserSearchList.Where(x => x.ColumnSearchKey.Equals(key)).FirstOrDefault();

                if (search != null)
                {
                    search.ColumnValue = httpRequest.Form[key].First().ToString();
                }

            }
        }
    }
}
