using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Ecommerce.Controllers
{
    public class LoginController : Controller
    {
        private IWebHostEnvironment Environment;
        public LoginController(IWebHostEnvironment _environment)
        {
            Environment = _environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(new Employee());
        }

        [HttpGet]
        public IActionResult Management()
        {
            return View();
        }

            [HttpPost]
        public IActionResult Index(Employee employee)
        {
            DAL.SchedulesContext appContext = new DAL.SchedulesContext();
            Employee customerDatabase = appContext.Customers
                .Where(x => x.email == employee.email).SingleOrDefault();
            if (customerDatabase == null)
            {
                ViewBag.Error = "Usuário ou senha incorretos!";
                return View(employee);
            }
            else
            {
                string hash = employee.Hash + customerDatabase.Salt;
                hash = Util.Cryptography.CreateMD5(hash);
                if (customerDatabase.Hash == hash)
                {
                    HttpContext.Session.SetString("user", customerDatabase.name);
                    HttpContext.Session.SetInt32("userId", customerDatabase.id);
                    return RedirectToAction("Management", "Login");
                }
                else
                {
                    ViewBag.Error = "Usuário ou senha incorretos!";
                    return View(employee);
                }
            }
        }

        [HttpPost]
        public IActionResult Create(Employee employee /*IFormFile postedFile*/)
        {
          
            DAL.SchedulesContext appContext = new DAL.SchedulesContext();
            employee.Salt = Guid.NewGuid().ToString();
            employee.Hash = Util.Cryptography.CreateMD5(employee.Hash + employee.Salt);
            appContext.Customers.Add(employee);
            appContext.SaveChanges();
            return RedirectToAction("Index", "Schedule");
        }
    }
}