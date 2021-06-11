using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EventsApplication.Models;

namespace EventsApplication.Data
{
    public class EventsApplicationContext : DbContext
    {
        public EventsApplicationContext (DbContextOptions<EventsApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<EventsApplication.Models.Event> Event { get; set; }
    }
}
