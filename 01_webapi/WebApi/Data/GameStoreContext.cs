using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Data
{
    public class GameStoreContext(DbContextOptions<GameStoreContext> options) : DbContext(options)
    {
        public DbSet<Game> Games => Set<Game>();
        public DbSet<Genre> Genres => Set<Genre>();
        
        override protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Genre>().HasData(
                new Genre { Id = 1, Name = "Action-adventure" },
                new Genre { Id = 2, Name = "RPG" },
                new Genre { Id = 3, Name = "Shooter" }
            );

            modelBuilder.Entity<Game>().HasData(
                new Game { Id = 1, Name = "The Legend of Zelda: Breath of the Wild", GenreId = 1, Price = 59.99m, ReleaseDate = new DateTime(2017, 3, 3) },
                new Game { Id = 2, Name = "God of War", GenreId = 1, Price = 49.99m, ReleaseDate = new DateTime(2018, 4, 20) },
                new Game { Id = 3, Name = "The Witcher 3: Wild Hunt", GenreId = 2, Price = 39.99m, ReleaseDate = new DateTime(2015, 5, 19) },
                new Game { Id = 4, Name = "Halo Infinite", GenreId = 3, Price = 59.99m, ReleaseDate = new DateTime(2021, 12, 8) }
            );
        }
    }
}