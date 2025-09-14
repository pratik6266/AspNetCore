using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Dtos
{
    public record class UpdateGameDto
    (
        string Name,
        string Genre,
        decimal Price,
        DateTime ReleaseDate
    );
}