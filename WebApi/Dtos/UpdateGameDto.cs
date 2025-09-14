using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Dtos
{
    public class UpdateGameDto
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public int GenreId { get; set; }

        [Range(1, 100)]
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }
    }
}