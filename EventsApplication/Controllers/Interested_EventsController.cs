using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventsApplication.Data;
using EventsApplication.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using EventsApplication.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNet.Identity;
using System.Data;


namespace EventsApplication.Controllers
{
    public class Interested_EventsController : Controller
    {
        private readonly EventsApplicationContext _context;
        private readonly AuthDbContext _AuthContext;
        private readonly Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> _userManager;


        public Interested_EventsController(EventsApplicationContext context, AuthDbContext context2, Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> userManager)
        {
            _context = context;
            _AuthContext = context2;
            _userManager = userManager;
        }
        protected Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> UserManager { get; set; }

        // GET: Events
        public async Task<IActionResult> Index()
        {

            var user = await _userManager.GetUserAsync(User);
            string ID = user.Id;
            var InterestedEventsIDs = from e in _context.InterestedUserEvents where e.UserId.Equals(user.Id) select e.EventId;
            
            
            List<Event> Events = new List<Event>();
            
            foreach (var item in InterestedEventsIDs)
            {
               var Event = from e in _context.Event where e.Id.Equals(item) select e;
               Events.Add(Event.FirstOrDefault());
            }



            var eventTypeVM = new EventTypeViewModel
            {

                Events = Events
            };

            return View(eventTypeVM);


            
        }
    }
}
