using Employees.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Employees.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeContext _context;

        public EmployeeController(EmployeeContext context)
        {
            _context = context;
        }
        // GET: Product
        public ActionResult Index()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Messages = TempData["SuccessMessage"];
            var dataList = _context.TblEmployees.Where(x => x.IsActive == true);
            return View(dataList);
        }

        public ActionResult EmployeeCreate()
        {
            ViewBag.Message = TempData["Message"];
            ViewBag.Messages = TempData["SuccessMessage"];
            var dataList1 = _context.TblEmployees.Include(x => x.TblDesignation);
            var dataList = dataList1.Where(x => x.IsActive == true);
            return View(dataList);
        }

        [HttpPost]
        public ActionResult EmployeeCreate(TblEmployee tblEmployee)
        {
            var existingCategory = _context.TblEmployees.FirstOrDefault(x => x.Name == tblEmployee.Name);
            if (existingCategory != null)
            {
                if (existingCategory.IsActive == false)
                {
                    TempData["Message"] = $"{tblEmployee.Name} employee has been deleted, please contact admin";
                    return RedirectToAction("EmployeeCreate");
                }
                else
                {
                    TempData["Message"] = $"{tblEmployee.Name} employee already exists";
                    return RedirectToAction("EmployeeCreate");
                }
            }
            else
            {
                tblEmployee.IsActive = true;
                _context.TblEmployees.Add(tblEmployee);
            }

            if (_context.SaveChanges() > 0)
            {
                TempData["SuccessMessage"] = "Save Successfully";
                return RedirectToAction("EmployeeCreate");
            }
            else
            {
                TempData["Message"] = "Failed";
                return RedirectToAction("EmployeeCreate");
            }

        }

        public ActionResult EmployeeEdit(int id)
        {
            {
                ViewBag.Message = TempData["Message"];
                ViewBag.Designation = _context.TblDesignations.ToList();
                ViewBag.Employee = _context.TblEmployees.ToList();
                var pdt = _context.TblEmployees.FirstOrDefault(x => x.Id == id);
                return View(pdt);
            }
        }

        [HttpPost]
        public ActionResult EmployeeEdit(int id, TblEmployee tblEmployee)
        {
            if (tblEmployee.Id > 0)
            {
                var existingCountry1 = _context.TblEmployees.FirstOrDefault(x => x.Name == tblEmployee.Name && x.Id != id);
                if (existingCountry1 != null)
                {
                    if (existingCountry1.IsActive == false)
                    {
                        TempData["Message"] = $"{tblEmployee.Name} employee has been deleted, please contact admin";
                        return RedirectToAction("EmployeeEdit");
                    }
                    else
                    {
                        TempData["Message"] = $"{tblEmployee.Name} employee already exists";
                        return RedirectToAction("EmployeeEdit");
                    }
                }

                else
                {
                    var existingCountry = _context.TblEmployees.FirstOrDefault(x => x.Id == tblEmployee.Id);
                    if (existingCountry != null)
                    {
                        existingCountry.Name = tblEmployee.Name;
                        existingCountry.DesignationId = tblEmployee.DesignationId;
                        existingCountry.Email = tblEmployee.Email;
                        existingCountry.Age = tblEmployee.Age;
                        existingCountry.Doj = tblEmployee.Doj;
                        existingCountry.Gender = tblEmployee.Gender;
                        existingCountry.IsActive = true;
                        _context.Entry(existingCountry).State = EntityState.Modified;
                        if (_context.SaveChanges() > 0)
                        {
                            TempData["SuccessMessage"] = "Edited Successfully";

                            return RedirectToAction("EmployeeCreate");
                        }
                        else
                        {
                            TempData["Message"] = "Failed";
                            return RedirectToAction("EmployeeCreate");
                        }
                    }
                    return RedirectToAction("EmployeeEdit");
                }
            }
            return View(tblEmployee);
        }

        [HttpPost]
        public JsonResult DeleteEmployee(int Id)
        {
            bool result = false;
            TblEmployee s = _context.TblEmployees.Where(x => x.Id == Id).SingleOrDefault();
            if (s != null)
            {
                s.IsActive = false;
                _context.SaveChanges();
                result = true;
            }

            return Json(result);
        }

        [HttpGet]
        public JsonResult GetAllDesignation()
        {
            var dataList = _context.TblDesignations.Where(x => x.IsActive == true).ToList();
            var data = dataList.Select(x => new {
                DesignationId = x.DesignationId,
                Designation = x.Designation,
                IsActive = x.IsActive
            });
            return new JsonResult(data);
        }
    }
}
