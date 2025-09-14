using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int GenreId { get; set; }
        public Genre? Genre { get; set; }

        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}