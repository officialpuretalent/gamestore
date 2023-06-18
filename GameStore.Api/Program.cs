using GameStore.Api.Entities;

const string GetGameEndpointName = "GetGame";

List<Game> games = new()
{
  new Game()
  {
    Id = 1,
    Name = "Street Fighter II",
    Genre = "Fighting",
    Price = 19.99M,
    ReleaseDate = new DateTime(1991, 2, 1),
    ImageUri = "https://placehold.co/150"
  },
  new Game()
  {
    Id = 2,
    Name = "Final Fantasy XIV",
    Genre = "Roleplaying",
    Price = 59.99M,
    ReleaseDate = new DateTime(2010, 9, 30),
    ImageUri = "https://placehold.co/150"
  },
  new Game()
  {
    Id = 3,
    Name = "FIFA 23",
    Genre = "Sport",
    Price = 69.99M,
    ReleaseDate = new DateTime(2022, 9, 27),
    ImageUri = "https://placehold.co/150"
  }
};

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

// GET /games
app.MapGet("/games", () => games);


// GET /games/{id}
app.MapGet("/games/{id}", (int id) =>
{
  Game? game = games.Find(game => game.Id == id);

  if (game is null)
    return Results.NotFound();

  return Results.Ok(game);
})
.WithName(GetGameEndpointName);


// POST /games
app.MapPost("/games", (Game game) =>
{
  game.Id = games.Max(game => game.Id) + 1;
  games.Add(game);

  return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
});


// PUT /games/{id}
app.MapPut("/games/{id}", (int id, Game updatedGame) =>
{
  Game? existingGame = games.Find(game => game.Id == id);

  if (existingGame is null)
    return Results.NotFound();

  existingGame.Name = string.IsNullOrEmpty(updatedGame.Name) ? existingGame.Name : updatedGame.Name;
  existingGame.Genre = string.IsNullOrEmpty(updatedGame.Genre) ? existingGame.Genre : updatedGame.Genre;
  existingGame.Price = existingGame.Price != updatedGame.Price ? updatedGame.Price : existingGame.Price;
  existingGame.ReleaseDate = existingGame.ReleaseDate;
  existingGame.ImageUri = string.IsNullOrEmpty(updatedGame.ImageUri) ? existingGame.ImageUri : updatedGame.ImageUri;

  return Results.NoContent();
});


// DELETE /games/{id}
app.MapDelete("/games/{id}", (int id) =>
{
  Game? game = games.Find(game => game.Id == id);

  if (game is not null)
    games.Remove(game);

  return Results.NoContent();
});

app.Run();
