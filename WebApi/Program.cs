using WebApi.Dtos;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndPoint = "GetGame";

List<GameDto> games = [
    new GameDto(1, "The Legend of Zelda: Breath of the Wild", "Action-adventure", 59.99m, new DateTime(2017, 3, 3)),
    new GameDto(2, "God of War", "Action-adventure", 49.99m, new DateTime(2018, 4, 20)),
    new GameDto(3, "Red Dead Redemption 2", "Action-adventure", 39.99m, new DateTime(2018, 10, 26)),
    new GameDto(4, "The Witcher 3: Wild Hunt", "Action RPG", 29.99m, new DateTime(2015, 5, 19)),
    new GameDto(5, "Minecraft", "Sandbox", 26.95m, new DateTime(2011, 11, 18))
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id}", (int id) =>
    games.Find(game => game.Id == id)
).WithName(GetGameEndPoint);

app.MapPost("/games", (CreateGameDto newGame) =>
{
    GameDto game = new(
        games.Count + 1,
        newGame.Name,
        newGame.Genre,
        newGame.Price,
        newGame.ReleaseDate
    );

    games.Add(game);
    return Results.CreatedAtRoute(GetGameEndPoint, new { id = game.Id }, game);
});

app.MapPut("/games/{id}", (int id, UpdateGameDto updatedGame) =>
{
    var indexx = games.FindIndex(game => game.Id == id);
    if (indexx == -1) return Results.NotFound();

    games[indexx] = new GameDto(
        id,
        updatedGame.Name,
        updatedGame.Genre,
        updatedGame.Price,
        updatedGame.ReleaseDate
    );

    return Results.Ok();
});

app.MapDelete("/games/{id}", (int id) =>
{
    var index = games.FindIndex(game => game.Id == id);
    if (index == -1) return Results.NotFound();

    games.RemoveAt(index);
    return Results.Ok();
});

app.Run();