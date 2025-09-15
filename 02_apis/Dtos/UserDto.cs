using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Model;

namespace _02_apis.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<User>? Comments { get; set; }
    }
}