using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ZenithCore.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;

namespace ZenithCore.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            var firstDay = "";
            var lastDay = "";



            if ((int)System.DateTime.Now.DayOfWeek == 0)
            {
                firstDay = DateTime.Now.ToString() + " 12:00 AM";
            }
            else
            {
                firstDay = DateTime.Now.AddDays(1 - (int)System.DateTime.Now.DayOfWeek).ToString("MM/dd/yy") + " 12:00 AM";
            }


            if ((int)System.DateTime.Now.DayOfWeek == 0)
            {
                lastDay = System.DateTime.Now.ToString() + " 23:59";
            }
            else
            {
                lastDay = (DateTime.Now.AddDays(7 - (int)System.DateTime.Now.DayOfWeek)).ToString("MM/dd/yy") + " 23:59";
            }

            DateTime maxDate = DateTime.Parse(lastDay);

            ViewBag.DateUntil = maxDate;
            DateTime minDate = Convert.ToDateTime(firstDay);

           // var events = _context.Events.Include(e => e.Activity);

            var events = from e in _context.Events.Include(a => a.Activity)
                        where e.EventFrom >= minDate && e.EventTo <= maxDate && e.IsActive
                         orderby e.EventFrom ascending
                         select e;
            return View(events.ToList());
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
