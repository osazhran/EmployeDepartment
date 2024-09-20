using AspNetCoreHero.ToastNotification.Abstractions;
using BLL.Interfaces;
using BLL.Repositories;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;

namespace MvcProject.Controllers
{
    /// here there is two relashin ship 
    /// 1. inheritance  : employeeController is a controller 
    /// 2. association compostion(required) :  employeeController has a employeeRepository

    public class DepartmentController : Controller
    {
        #region Constructor and read only properity
        private readonly IDepartmentRepository _departmentloyeeRepo;
        private readonly INotyfService _notyf;

        public DepartmentController(IDepartmentRepository departmentloyeeRepo, INotyfService notyf)
        {
            _departmentloyeeRepo = departmentloyeeRepo;
            _notyf = notyf;
        }
        #endregion

        #region Index Action
        // / employee/Index
        public IActionResult Index()
        {
            var department = _departmentloyeeRepo.GetAll();
            return View(department);
        } 
        #endregion

        #region Create employee
        // Get: employee/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Department department)
        {
            if (ModelState.IsValid) // server side validation
            {
                var Count = _departmentloyeeRepo.Add(department);
                if (Count > 0)
                {
                    _notyf.Success("Department has been added successfully", 3);
                    //TempData["message"] = "Department has been occured, Department not added Please try again in another time";
                    return RedirectToAction(nameof(Index));
                }

                else
                {
                    _notyf.Error("An error has been occured , Department not added, please try again ");
                    return RedirectToAction(nameof(Index));
                }

            }
            return View(department);
        }
        #endregion

        #region Details of department
        [HttpGet]
        public IActionResult Details(int? id , string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();// 400
            var department = _departmentloyeeRepo.GetById(id.Value);
            if(department is null)
                return NotFound();
            return View(ViewName, department);
        }
        #endregion

        #region Edit department
        [HttpGet]
        public IActionResult Edit(int? id)
        {
  
            return Details(id, "Edit");

        }
        [ValidateAntiForgeryToken] /*it prevent third party like postman  to make any action*/

        [HttpPost]
        public IActionResult Edit([FromRoute] int id , Department department)
        {
            if (id != department.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
               try
                {
                    _departmentloyeeRepo.Update(department);
                    _notyf.Success("Department has been Updated successfully", 3);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _notyf.Error("An error has been occured , Department not Updated, please try again ");

                }
            }
          return View(department);
        }
        #endregion

        #region Delete employee
        [HttpGet]
        public IActionResult Delete(int?id)
        {
            return Details(id, "Delete");
        }

        [ValidateAntiForgeryToken] 
        [HttpPost]
        public IActionResult Delete([FromRoute] int id,Department department)
        {
            if (id != department.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _departmentloyeeRepo.Delete(department);
                    _notyf.Success("Department has been Deleted successfully", 3);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _notyf.Error("An error has been occured , Department not Deleted, please try again ");

                }
            }

            return View(department);
        }
        #endregion
    }
}
