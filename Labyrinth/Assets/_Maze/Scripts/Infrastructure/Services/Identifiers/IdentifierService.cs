namespace FreedLOW._Maze.Scripts.Infrastructure.Services.Identifiers
{
  public class IdentifierService : IIdentifierService
  {
    private int lastId = 1;

    public int Next() =>
      ++lastId;
  }
}