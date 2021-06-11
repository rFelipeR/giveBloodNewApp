using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL;

namespace giveBloodNewApp.Controllers
{
    public class ScheduleController : Controller
    {
        IHostEnvironment hostingEnvironment;

        public ScheduleController(IHostEnvironment env)
        {
            hostingEnvironment = env;
        }
        public IActionResult Index() //opcional ?, pode ser Nullable<int>
        {
            SchedulesContext schedulesContext = new SchedulesContext();   //iniciar context 1º

            var scheduleList = schedulesContext.Schedules.OrderByDescending(x => x.date).Where(x => true);        


            ViewBag.Schedules = schedulesContext.Schedules.OrderBy(x => x.id_schedule).ToList();
            return View(scheduleList.ToList());
        }

        public IActionResult Detail(int id)
        {

            SchedulesContext SchedulesContext = new SchedulesContext();
            Scheduling schedule = SchedulesContext.Schedules.Find(id);
            //moviesContext.Movies.Where(x => x.Id == id).FirstOrDefault();

            return View(schedule);
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
            schedulesContext.Schedules.Add(scheduling);
            schedulesContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
