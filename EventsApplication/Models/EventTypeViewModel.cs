using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class EventTypeViewModel
    {
        public List<Event> Events { get; set; }
        public SelectList Types { get; set; }

        public string EventType { get; set; }

        public string SearchString { get; set; }

    }
}
