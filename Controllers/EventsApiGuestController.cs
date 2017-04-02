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
        public IEnumerable<EventApi> GetEvents()
        {
            //var applicationDbContext = _context.Events.Include(e => e.Activity);
            return from e in _context.Events.Include(a => a.Activity)
                   select new EventApi                   {
                       ActivityDescription = e.Activity.ActivityDescription,
                       EventFrom = e.EventFrom,
                       EventTo=e.EventTo,
                       IsActive = e.IsActive,
                       EventToFrom =e.EventFrom.ToString("MMM dd, yyyy ") + e.EventFrom.ToString("hh:mm tt- ") + e.EventTo.ToString("hh:mm tt")


                   };
        }

    
    }
}