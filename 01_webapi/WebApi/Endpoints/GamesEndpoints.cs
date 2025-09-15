using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Dtos;
using WebApi.Entities;
using WebApi.Mapping;

namespace WebApi.Endpoints
{
    public static class GamesEndpoints
    {
        public static RouteGroupBuilder MapGamesEndPoints(this WebApplication app)
        {
            const string GetGameEndPoint = "GetGame";

            var group = app.MapGroup("/games").WithParameterValidation();

            group.MapGet("/", async (GameStoreContext dbContext) =>
            {
                var games = await dbContext.Games
                    .Include(g => g.Genre)
                    .Select(game => game.ToDto())
                    .AsNoTracking()
                    .ToListAsync();
                return Results.Ok(games);
            });

            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? game = await dbContext.Games.FindAsync(id);
                if (game is null) return Results.NotFound();
                return Results.Ok(game.ToDto());
            }).WithName(GetGameEndPoint);

            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
            {
                Game game = newGame.ToEntity();
                game.Genre = dbContext.Genres.Find(newGame.GenreId);

                dbContext.Games.Add(game);
                await dbContext.SaveChangesAsync();

                GameDto gameDto = GameMapping.ToDto(game);
                return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, gameDto);
            });

            group.MapPut("/{id}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                Game? existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame is null) return Results.NotFound();
                existingGame.Name = updatedGame.Name;
                existingGame.Price = updatedGame.Price; 
                existingGame.ReleaseDate = updatedGame.ReleaseDate;
                existingGame.GenreId = updatedGame.GenreId;
                existingGame.Genre = dbContext.Genres.Find(updatedGame.GenreId);
                await dbContext.SaveChangesAsync();

                return Results.Ok();
            });

            group.MapDelete("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                Game? existingGame = await dbContext.Games.FindAsync(id);
                if (existingGame is null) return Results.NoContent();

                dbContext.Games.Remove(existingGame);
                await dbContext.SaveChangesAsync();
                return Results.Ok();
            });

            return group;
        }
    }
}