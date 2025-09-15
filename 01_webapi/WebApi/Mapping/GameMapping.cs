using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Dtos;
using WebApi.Entities;

namespace WebApi.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto dto)
        {
            return new Game
            {
                Name = dto.Name,
                GenreId = dto.GenreId,
                Price = dto.Price,
                ReleaseDate = dto.ReleaseDate
            };
        }

        public static GameDto ToDto(this Game entity)
        {
            return new GameDto(
                entity.Id,
                entity.Name,
                entity.Genre?.Name ?? "Unknown",
                entity.Price,
                entity.ReleaseDate
            );
        }
    };    
}