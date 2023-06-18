using GameStore.Api.Entities;

namespace GameStore.Api.Repositories;

public class InMemGamesRepository : IGamesRepository
{
  private readonly List<Game> _games = new()
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

  public IEnumerable<Game> GetAll()
  {
    return _games;
  }

  public Game? Get(int id)
  {
    return _games.Find(g => g.Id == id);
  }

  public void Create(Game game)
  {
    game.Id = _games.Max(g => g.Id) + 1;
    _games.Add(game);
  }

  public void Update(Game updatedGame)
  {
    int index = _games.FindIndex(g => g.Id == updatedGame.Id);
    _games[index] = updatedGame;
  }

  public void Delete(int id)
  {
    int index = _games.FindIndex(g => g.Id == id);
    _games.RemoveAt(index);
  }
}
