using AspNetCoreHero.ToastNotification.Abstractions;
using BLL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;

namespace MvcProject.Controllers
{
    public class EmployeeController : Controller
    {
        #region Constructor and read only properity
        private readonly IEmployeeRepository _employeeRepo;
        private readonly INotyfService _notyf;

        public EmployeeController(IEmployeeRepository employeeRepo , INotyfService notyf)
        {
            _employeeRepo = employeeRepo;
            _notyf = notyf;
        }
        #endregion

        #region Index Action
        // / employee/Index
        public IActionResult Index(string searchInp)
        {
            var employee = Enumerable.Empty<Employee>();
            if (string.IsNullOrEmpty(searchInp))
                employee = _employeeRepo.GetAll();

            else
                employee = _employeeRepo.SearchByName(searchInp.ToLower());
                return View(employee);

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
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid) // server side validation
            {
                var Count = _employeeRepo.Add(employee);
                if (Count > 0)
                {
                    return RedirectToAction(nameof(Index));
                    _notyf.Success("Employee has been added successfully", 3);

                }
                else
                {
                    return RedirectToAction(nameof(Index));
                    _notyf.Error("An error has been occured , Employee not added, please try again ");
                }
            }
            return View(employee);
        }
        #endregion

        #region Details of employee
        [HttpGet]
        public IActionResult Details(int? id, string ViewName = "Details")
        {
            if (!id.HasValue)
                return BadRequest();// 400
            var employee = _employeeRepo.GetById(id.Value);
            if (employee is null)
                return NotFound();
            return View(ViewName, employee);
        }
        #endregion

        #region Edit employee
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ///if (!id.HasValue)
            ///    return BadRequest();
            ///var employee = _employeeRepo.GetById(id.Value);
            ///if(employee is null)
            ///    return NotFound();  
            ///    
            return Details(id, "Edit");

        }
        [ValidateAntiForgeryToken] /*it prevent third party like postman  to make any action*/

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();
            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Update(employee);
                    _notyf.Success("Employee has been Updated successfully", 3);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _notyf.Error("An error has been occured , Employee not Updated, please try again ");
                }
            }
            return View(employee);
        }
        #endregion

        #region Delete employee
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            return Details(id, "Delete");
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Delete([FromRoute] int id, Employee employee)
        {
            if (id != employee.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _employeeRepo.Delete(employee);
                    _notyf.Success("Employee has been Deleted successfully", 3);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, ex.Message);
                    _notyf.Error("An error has been occured , Employee not Deleted, please try again ");
                }
            }

            return View(employee);
        }
        #endregion
    }
}
