using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Net.Mail;
using System.Net;

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


        public IActionResult Confirmation(int id)
        {
            SchedulesContext SchedulesContext = new SchedulesContext();
            Scheduling schedule = SchedulesContext.Schedulings.Find(id);
            var fromAddress = new MailAddress("doesangue.doe@gmail.com", "Doe Sangue");
            var toAddress = new MailAddress(schedule.email, schedule.name);
            string fromPassword = "doesangu3";
            string subject = "Confirmação de agendamento";
            string body = @$"Olá {schedule.name}, seu agendamento foi 
realizado com sucesso. <br> Data:<strong>{schedule.date.ToString()}</strong> <br> Atenciosamente, Hemocentro {schedule.bloodCenter.name_bc}";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };


            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                smtp.Send(message);
            }

            return RedirectToAction("Management");

        }


        [HttpGet]
        public IActionResult Management()
        {
                SchedulesContext schedulesContext = new SchedulesContext();   
                var scheduleList = schedulesContext.Schedulings.OrderByDescending(x => x.date).Where(x => true);
                //var bloodCenterList = schedulesContext.BloodCenters.OrderByDescending(x => x.id_bc).Where(x => true);
                ViewBag.Schedulings = schedulesContext.Schedulings.OrderBy(x => x.id).ToList();
                return View(scheduleList.ToList());
        }

            [HttpPost]
        public IActionResult Index(Employee employee)
        {
            DAL.SchedulesContext appContext = new DAL.SchedulesContext();
            Employee customerDatabase = appContext.Employees
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
            appContext.Employees.Add(employee);
            appContext.SaveChanges();
            return RedirectToAction("Index", "Schedule");
        }
    }
}