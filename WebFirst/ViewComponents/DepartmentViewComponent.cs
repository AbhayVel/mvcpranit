using FirstRepositoryLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebFirst.Views.Shared.Components
{
    public class DepartmentViewComponent : ViewComponent
    {
        public IDeprtmentRepo _deprtmentRepo { get; set; }
        public DepartmentViewComponent(IDeprtmentRepo deprtmentRepo)
        {
             
            _deprtmentRepo = deprtmentRepo;
        }
        public ViewViewComponentResult Invoke(dynamic obj)
        {

            var deptList = _deprtmentRepo.GetAll();

            ViewBag.DeptId = new SelectList(deptList, "Id", "Name", obj.DeptId);
            return View(obj);
        }
    }
}
