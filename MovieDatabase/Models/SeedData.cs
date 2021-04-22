using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//Used to populate the database with some prelimenary data
namespace MovieDatabase.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder application)
        {
            MovieDbContext context = application.ApplicationServices.
                CreateScope().ServiceProvider.GetRequiredService<MovieDbContext>();

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.AddMovie.Any())
            {
                context.AddMovie.AddRange(
                    new AddMovie
                    {
                        Category = "Sci-fi",
                        Title = "Dune",
                        YearReleased = 1984,
                        DirectorName = "David Lynch",
                        MovieRating = "PG-13",
                        IsEdited = false,
                        IsLentTo = "",
                        Notes = "He who controls the spice controls the universe"

                    }
                    );
                context.SaveChanges();
            }
        }
    }
}
