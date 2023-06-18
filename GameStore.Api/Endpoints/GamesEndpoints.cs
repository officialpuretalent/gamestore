using GameStore.Api.Entities;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
  const string GetGameEndpointName = "GetGame";

  private static List<Game> _games = new()
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

  public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
  {
    var group = routes.MapGroup("/games").WithParameterValidation();

    group.MapGet("/", () => _games);

    group.MapGet("/{id}", (int id) =>
    {
      Game? game = _games.Find(game => game.Id == id);

      if (game is null)
        return Results.NotFound();

      return Results.Ok(game);
    })
    .WithName(GetGameEndpointName);

    group.MapPost("/", (Game game) =>
    {
      game.Id = _games.Max(game => game.Id) + 1;
      _games.Add(game);

      return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
    });

    group.MapPut("/{id}", (int id, Game updatedGame) =>
    {
      Game? existingGame = _games.Find(game => game.Id == id);

      if (existingGame is null)
        return Results.NotFound();

      existingGame.Name = updatedGame.Name;
      existingGame.Genre = updatedGame.Genre;
      existingGame.Price = existingGame.Price;
      existingGame.ReleaseDate = existingGame.ReleaseDate;
      existingGame.ImageUri = updatedGame.ImageUri;

      return Results.NoContent();
    });

    group.MapDelete("/{id}", (int id) =>
    {
      Game? game = _games.Find(game => game.Id == id);

      if (game is not null)
        _games.Remove(game);

      return Results.NoContent();
    });
    
    return group;
  }
}
