using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZenithCore.Data;
using ZenithCore.Models.ZenithModels;
using Microsoft.AspNetCore.Authorization;
using System.Collections;

namespace ZenithCore.Controllers
{
    [Produces("application/json")]
    [Route("api/EventsApiGuest")]
    //[Authorize]
    public class EventsApiGuestController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventsApiGuestController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/EventsApi
        [HttpGet]
        public IEnumerable GetEvents()
        {
            //var applicationDbContext = _context.Events.Include(e => e.Activity);

            var firstDay = "";
            var lastDay = "";



            if ((int)System.DateTime.Now.DayOfWeek == 0)
            {
                firstDay = DateTime.Now.AddDays(-6).ToString("MM/dd/yy") + " 12:00 AM";
            }
            else
            {
                firstDay = DateTime.Now.AddDays(1 - (int)System.DateTime.Now.DayOfWeek).ToString("MM/dd/yy") + " 12:00 AM";
            }


            if ((int)System.DateTime.Now.DayOfWeek == 0)
            {
                lastDay = System.DateTime.Now.ToString("MM/dd/yy") + " 23:59";
            }
            else
            {
                lastDay = (DateTime.Now.AddDays(7 - (int)System.DateTime.Now.DayOfWeek)).ToString("MM/dd/yy") + " 23:59";
            }

            DateTime maxDate = DateTime.Parse(lastDay);

            ViewBag.DateUntil = maxDate;
            DateTime minDate = Convert.ToDateTime(firstDay);

            // var events = _context.Events.Include(e => e.Activity);

            //var events = from e in _context.Events.Include(a => a.Activity)
            //             where e.EventFrom >= minDate && e.EventTo <= maxDate && e.IsActive
            //             orderby e.EventFrom ascending
            //             select e;
            

            return from e in _context.Events.Include(a => a.Activity)
                   where e.EventFrom >= minDate && e.EventTo <= maxDate && e.IsActive
                   orderby e.EventFrom ascending
                   select new                   {
                       ActivityDescription = e.Activity.ActivityDescription,
                       EventFrom = e.EventFrom,
                       EventTo=e.EventTo,
                       IsActive = e.IsActive,
                       EventToFrom =e.EventFrom.ToString("MMM dd, yyyy ") + e.EventFrom.ToString("hh:mm tt - ") + e.EventTo.ToString("hh:mm tt")


                   };
        }

    
    }
}