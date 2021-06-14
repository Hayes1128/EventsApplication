using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EventsApplication.Data;
using System;
using System.Linq;

namespace EventsApplication.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EventsApplicationContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<EventsApplicationContext>>()))
            {
                // Look for any movies.
                if (context.Event.Any())
                {
                    return;   // DB has been seeded
                }

                context.Event.AddRange(
                    new Event
                    {
                        Title = "Music Concert",
                        ReleaseDate = DateTime.Parse("1989-2-12"),
                        Type = "Music",
                        Description = "This is a music concert etc, more information provided soon",
                        Price = 7.99M,
                        Owner = "Some Band",
                        filename = "Queen.jpg"

                    },

                    new Event
                    {
                        Title = "Art Exhibition",
                        ReleaseDate = DateTime.Parse("1996-5-11"),
                        Type = "Art",
                        Description = "This is an art exhibition etc, more information provided soon",
                        Price = 11.99M,
                        Owner = "Some Artist",
                        filename = "Art.jpg"
                    },

                    new Event
                    {
                        Title = "Food Festival",
                        ReleaseDate = DateTime.Parse("2005-11-10"),
                        Type = "Food and Drink",
                        Description = "This is a food festival etc, more information provided soon",
                        Price = 23.99M,
                        Owner = "Some Company or chef",
                        filename = "Food.jpg"
                    },

                    new Event
                    {
                        Title = "Scientific Lecture",
                        ReleaseDate = DateTime.Parse("2020-4-12"),
                        Type = "Academia",
                        Description = "This is an academic lecture etc, more information provided soon",
                        Price = 0.00M,
                        Owner = "Some Scientist",
                        filename = "Lecture.jpg"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}