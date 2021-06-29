using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;
using giveBloodNewApp.Services;
using Microsoft.Extensions.Options;
using System.Net.Mail;
using System.Net;

namespace giveBloodNewApp.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly MyConfiguration _myConfiguration;

        public ScheduleController(IOptions<MyConfiguration> myConfiguration)
        {
            _myConfiguration = myConfiguration.Value;
        }

        public IActionResult Index() //opcional ?, pode ser Nullable<int>
        {
            SchedulesContext schedulesContext = new SchedulesContext();   //iniciar context 1º

            var scheduleList = schedulesContext.Schedulings.OrderByDescending(x => x.date).Where(x => true);
            var bloodCenterList = schedulesContext.BloodCenters.OrderByDescending(x => x.id_bc).Where(x => true);
            ViewBag.Schedules = schedulesContext.Schedulings.OrderBy(x => x.id).ToList();
            ViewBag.BloodCenters = schedulesContext.BloodCenters.OrderBy(x => x.id_bc).ToList();
            return View(scheduleList.ToList());
        }

      


        public IActionResult Detail(int id)
        {

            List<string> Errors = new List<string>();
            var fromAddress = new MailAddress("doesangue.doe@gmail.com", "Doe Sangue");
            var toAddress = new MailAddress("reckziegel.felipe@gmail.com", "Felipe");
            const string fromPassword = "doesangu3";
            const string subject = "Confirmação de agendamento";
            const string body = "Olá @name, seu agendamento foi realizado com sucesso. <br> Data:<b>@model.name</b> <br> Atenciosamente, Hemocentro X";

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                //DeliveryMethod = SmtpDeliveryMethod.Network,
                //UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };

            try
            {
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = body,
                    IsBodyHtml = true
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception exp)

            { Errors.Add(exp.Message); }


            SchedulesContext SchedulesContext = new SchedulesContext();
            Scheduling schedule = SchedulesContext.Schedulings.Find(id);
            //moviesContext.Movies.Where(x => x.Id == id).FirstOrDefault();

            return View(schedule);
        }

        public IActionResult AboutUs()
        {

            return View();
        }



        public IActionResult Contacts()
        {

            SchedulesContext schedulesContext = new SchedulesContext();
            //ViewBag.BloodCenters = schedulesContext.BloodCenters.OrderBy(x => x.id_bc).ToList();
            var bloodCenterList = schedulesContext.BloodCenters.OrderByDescending(x => x.id_bc).Where(x => true);


            return View(bloodCenterList.ToList());
        }

        public IActionResult New()
        {
            SchedulesContext schedulesContext = new SchedulesContext();
            ViewBag.BloodCenters = schedulesContext.BloodCenters.OrderBy(x => x.id_bc).ToList();
            ViewBag.Donors = schedulesContext.Donors.OrderBy(x => x.id).ToList();
            Scheduling scheduling = new Scheduling();
            return View(scheduling);
        }
        [HttpPost]
        public IActionResult New(Scheduling scheduling)
        {
            SchedulesContext schedulesContext = new SchedulesContext();
            schedulesContext.Schedulings.Add(scheduling);
            schedulesContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
