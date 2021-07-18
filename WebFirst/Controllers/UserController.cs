using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using DBContextProject;
using FirstModel;
using FirstRepositoryLayer;
using FirstService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
 
using WebFirst.Models;
using WebFirst.Filters;
using WebFirst.Utility;

namespace WebFirst.Controllers
{


    [Authorize(Roles ="Admin,Dept")] //-->ISINROle 
    [AuthorizationMyFilter(RoleList ="Admin")]//--> ANy Custom Logic

    [MyActionFIlter(MinValue =30)]
    public class UserController : Base 
    {

        [NonAction]
        public string CheckMeCall()
        {
            return "I am called";
        }
        IUserRepo _userService;
        IDeprtmentRepo _deprtmentRepo;
        public UserController(IUserRepo userService, IDeprtmentRepo  deprtmentRepo)
        {
            _userService = userService;
            _deprtmentRepo = deprtmentRepo;
        }
        
        //////public IActionResult Index(string columnName, string orderBy)
        //////{
        //////    var lstData = _userService.GetAll();
        //////    lstData = (List<User>)SortData<User>(lstData, columnName, orderBy);
        //////    return View(lstData);
        //////}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>


        
        [HttpPost]
        [HttpGet]
        public ActionResult Index(string columnName, string orderBy)
        {

        ////    EmployeeContext ec = new EmployeeContext();

            //var dataEmployee = ec.UserDbSet.ToList();

            //var dataEmployeeDepartMent = ec.Departments.ToList();
            if (columnName != null)
            {
                ViewBag.columnName = columnName;
            }
            else
            {
                ViewBag.columnName = "Id"; ;
            }

            if (orderBy != null)
            {
                ViewBag.orderBy = orderBy;
            }
            else
            {
                ViewBag.orderBy = ""; 
            }
           
            UserSearch u = new UserSearch();
            ViewBag.search = u;
            if (Request.Method == "GET")
            {

              
                if(TempData["search"] !=null)
                {
                    
                    u = JsonSerializer.Deserialize<UserSearch>(TempData["search"].ToString());
                    ViewBag.search = u;
                    TempData.Keep();
                }
                
            }
            else
            {
                u.SetValue(Request);
                TempData["search"] = JsonSerializer.Serialize(u);
                TempData.Keep();
                ViewBag.search = u;
            }
           
            IEnumerable<FirstEnity.User> data = _userService.GetAllUser();


            //for (var i = 0; i < u.UserSearchList.Count; i++)
            //{
            //    if (!string.IsNullOrWhiteSpace(u.UserSearchList[i].ColumnValue))
            //    {
            //        data = FilterData(data, u.UserSearchList[i].ColumnName, u.UserSearchList[i].ColumnValue, u.UserSearchList[i].ColumnType);
            //    }
            //}

            var calData = data.ToList();

            calData = (List<FirstEnity.User>)SortData<FirstEnity.User>(calData, columnName, orderBy);
            return View("Index",calData);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var data = _userService.DeleteById(id);
            return RedirectToActionPermanent("Index"); 
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            var data = _userService.GetById(id);
            var deptList = _deprtmentRepo.GetAll();

            ViewBag.DeptId = new SelectList(deptList, "Id", "Name", data.DeptId);

            var employeDepartMentObj = new EmployeDepartMent();
            employeDepartMentObj.User = data;
            employeDepartMentObj.DepartmentList = deptList;

            return View(employeDepartMentObj);
        }


        [HttpGet]
        public ActionResult Create()
        {
            var data = new FirstEnity.User();
            var deptList = _deprtmentRepo.GetAll();
            deptList.Insert(0, new FirstEnity.Entity.Department() { Id = 0, Name = "--Select--" });
            ViewBag.DeptId = new SelectList(deptList, "Id", "Name", 0);

            var employeDepartMentObj = new EmployeDepartMent();
            employeDepartMentObj.User = data;
            employeDepartMentObj.DepartmentList = deptList;

            return View("Edit" , employeDepartMentObj);
        }


        [HttpPost]
        public ActionResult Save(EmployeDepartMent eobj)
        {

            if (ModelState.IsValid)
            {
                if (eobj.User.Id == 0)
                {
                                      
                    _userService.Save(eobj.User);
                }
                else
                {
                    var data = _userService.GetById(eobj.User.Id);
                    data.FirstName = eobj.User.FirstName;
                    data.LastName = eobj.User.LastName;
                    data.age = eobj.User.age;
                    data.DeptId = eobj.User.DeptId;

                    _userService.Save(data);
                }
               
                return RedirectToAction("Index");
            } else
            {
                var deptList = _deprtmentRepo.GetAll();

                eobj.DepartmentList = deptList;
                //var deptList = _deprtmentRepo.GetAll();


               
                //ViewBag.DeptId = new SelectList(deptList, "Id", "Name", eobj.User.Id);
                return View("Edit", eobj);
            }
            
        }



        private IEnumerable<T> SortData<T>(IEnumerable<T> lstData, string columnName, string orderBy)
        {
            if (string.IsNullOrEmpty(columnName))
            {
                return lstData;
            }

            if (string.IsNullOrEmpty(orderBy))
            {
                orderBy = "asc";
            }
            var propertyInfo = typeof(T).GetProperty(columnName);
            if (orderBy.Equals("asc"))
            {
              lstData=  lstData.OrderBy(x => propertyInfo.GetValue(x, null));
            } else
            {
              lstData=  lstData.OrderByDescending(x => propertyInfo.GetValue(x, null));
            }
            return lstData.ToList();
        }

       

   

    private IEnumerable<T> FilterData<T>(IEnumerable<T> lstData, string columnName, string value, string type)
        {
            if (string.IsNullOrEmpty(columnName)  || string.IsNullOrEmpty(value))
            {
                return lstData;
            }

           
            var propertyInfo = typeof(T).GetProperty(columnName);

            if (type.Equals("number"))
            {
                var convertedValue = MyModelBinder.Converts(value.ToString(), Type.GetType(propertyInfo.PropertyType.ToString()));
                lstData = lstData.Where(x => MyModelBinder.Converts(propertyInfo.GetValue(x, null).ToString(), Type.GetType(propertyInfo.PropertyType.ToString())) == convertedValue);

            }
            else if (type.Equals("string"))
            {
                var convertedValue = value; // MyModelBinder.Converts(value.ToString(), Type.GetType(propertyInfo.PropertyType.ToString()));
                lstData = lstData.Where(x => propertyInfo.GetValue(x, null).ToString().Contains(convertedValue));

            }

            return lstData;
        }

    }
}



//public IActionResult Index(string columnName, string orderBy)
//{
//    // FirstName , Last Name 
//    //  EmployeeContext ec = new EmployeeContext();

//    //    ec.UserDbSet.Add(new FirstModel.User { FirstName = "test" + DateTime.Now.Ticks.ToString(), LastName = "test", age = 21 });

//    //var user=    ec.UserDbSet.FirstOrDefault(X => X.Id == 1);
//    //    if (user != null)
//    //    {
//    //        ec.UserDbSet.Remove(user);
//    //    }

//    //    var user2 = ec.UserDbSet.FirstOrDefault(X => X.Id == 2);
//    //    if (user2 != null)
//    //    {
//    //        user2.LastName = "test" + DateTime.Now.Ticks.ToString();
//    //    }



//    //    ec.SaveChanges();
//    //    var queryList = ec.UserDbSet;
//    //    var lstD = ec.UserDbSet.FromSqlRaw("Select * from Employee").ToList();
//    var lstData = _userService.GetAll(); ;

//    return View(lstData);

//    //if (string.IsNullOrWhiteSpace(columnName))
//    //{
//    //    var orderByString = "FirstName asc,LastName desc";
//    //    IEnumerable<User> data = _userService.GetAll();
//    //    var orderByStringArray= orderByString.Split(  ',' );

//    //    for (var i = 0; i < orderByStringArray.Length; i++){
//    //        var columnNameOrderBy = orderByStringArray[i].Split(' ');
//    //        data = SortData(data, columnNameOrderBy[0], columnNameOrderBy[1]);
//    //    }


//    //    return View(data.ToList());
//    //}
//    //else
//    //{
//    //    var lstData = SortData(_userService.GetAll(), columnName, orderBy).ToList();
//    //    return View(lstData);
//    //}




//}

//[HttpPost]
//public IActionResult Index()
//{
//    IEnumerable<User> data = _userService.GetAll();

//    UserSearch u = new UserSearch();
//    u.SetValue(Request);


//    for (var i = 0; i < u.UserSearchList.Count; i++)
//    {
//        if (!string.IsNullOrWhiteSpace(u.UserSearchList[i].ColumnValue))
//        {
//            data = FilterData(data, u.UserSearchList[i].ColumnName, u.UserSearchList[i].ColumnValue, u.UserSearchList[i].ColumnType);
//        }
//    }

//    var calData = data.ToList();
//    return View(calData);
//}
