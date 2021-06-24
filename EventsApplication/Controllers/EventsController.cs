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

namespace EventsApplication.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly EventsApplicationContext _context;
        private readonly AuthDbContext _AuthContext;
        private readonly Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> _userManager;


        public EventsController(EventsApplicationContext context, AuthDbContext context2, Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> userManager)
        {
            _context = context;
            _AuthContext = context2;
            _userManager = userManager;
        }
        protected Microsoft.AspNetCore.Identity.UserManager<EventsApplicationUser> UserManager { get; set; }

        // GET: Events
        public async Task<IActionResult> Index(string eventType, string searchString)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> typeQuery = from e in _context.Event
                                            orderby e.Type
                                            select e.Type;

            var Events = from e in _context.Event select e;

            if (!String.IsNullOrEmpty(searchString))
            {
                Events = Events.Where(s => s.Owner.Contains(searchString));
                
               
            }

            if (!string.IsNullOrEmpty(eventType))
            {
                Events = Events.Where(x => x.Type == eventType);
            }

            var eventTypeVM = new EventTypeViewModel
            {
                Types = new SelectList(await typeQuery.Distinct().ToListAsync()),
                Events = await Events.ToListAsync()
            };

            return View(eventTypeVM);
        }

        // GET: Events/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            

            var @event = await _context.Event
                .FirstOrDefaultAsync(m => m.Id == id);

            
            
            var user = await _userManager.GetUserAsync(User);
            //we have got event and user so need to add entry in interested event database
            InterestedUserEvents IntUseEvent = new InterestedUserEvents
            {
                EventId = @event.Id,
                UserId = user.Id

            };

            _context.InterestedUserEvents.Add(IntUseEvent);

            
            

            await _context.SaveChangesAsync();

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // GET: Events/Create

        public async Task<IActionResult> CreateAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            string Type = user.UserType;
            if (Type == "Admin")
            {
                return View();
            }
            else
            {
                
                return Redirect("/Events");
            }
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ReleaseDate,Type,Description,Price,Owner,filename")] Event @event)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            string Type = user.UserType;
            if (Type == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var @event = await _context.Event.FindAsync(id);
                if (@event == null)
                {
                    return NotFound();
                }
                return View(@event);
            }
            else
            {

                return Redirect("/Events");
            }
           
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ReleaseDate,Type,Description,Price,Owner,filename")] Event @event)
        {
            if (id != @event.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            var user = await _userManager.GetUserAsync(User);
            string Type = user.UserType;
            if (Type == "Admin")
            {
                if (id == null)
                {
                    return NotFound();
                }

                var @event = await _context.Event
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (@event == null)
                {
                    return NotFound();
                }

                return View(@event);
            }
            else
            {

                return Redirect("/Events");
            }
            
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Event.Any(e => e.Id == id);
        }
    }
}
