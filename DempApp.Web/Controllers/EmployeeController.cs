using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DempApp.Web.Data;
using DempApp.Web.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DempApp.Web.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        

        public EmployeeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public IActionResult Index()
        {
            IEnumerable<Employee> _emp = _dbContext.Employees.ToList();
            return View(_emp);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee emp)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Employees.Add(emp);
                _dbContext.SaveChanges();
                TempData["ResultOK"] = "Record Added Successfully";
                return RedirectToAction("Index");
            }
            return View(emp);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id== null || id == 0)
            {
                return NotFound();
            }
            var empObj = _dbContext.Employees.Find(id);
            if (empObj == null)
            {
                return NotFound();
            }
            return View(empObj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee _emp)
        {
            if(ModelState.IsValid)
            {
                _dbContext.Employees.Update(_emp);
                _dbContext.SaveChanges();
                TempData["resultOK"] = "Data Updated Successfully";
                return RedirectToAction("Index");
            }
            return View(_emp);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id== null || id==0)
            {
                return NotFound();
            }
            var empObj = _dbContext.Employees.Find(id);
            if(empObj == null)
            {
                return NotFound();
            }
            return View(empObj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteEmp(int? id)
        {
            var deletedrecord = _dbContext.Employees.Find(id);
            if(deletedrecord == null)
            {
                return NotFound();
            }
            _dbContext.Employees.Remove(deletedrecord);
            _dbContext.SaveChanges();
            TempData["resultOK"] = "Employe Record Deleted Successfully from the PostgreSQL";
            return RedirectToAction("Index");
        }
    }
}

