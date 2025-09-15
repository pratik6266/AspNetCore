using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        //* public string? Name { get; set; } another way to make it non-nullable
    }
}