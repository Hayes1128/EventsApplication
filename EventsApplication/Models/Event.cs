using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EventsApplication.Models
{
    public class Event
    {

        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        //if it is a person/band/artist etc.
        public string Owner { get; set; }

        public string filename { get; set; } 


    }
}
