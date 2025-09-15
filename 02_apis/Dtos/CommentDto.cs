using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _02_apis.Model;

namespace _02_apis.Dtos
{
    public class CommentDto
    {
        public int UserId { get; set; }
        public required string Content { get; set; }
    }
}