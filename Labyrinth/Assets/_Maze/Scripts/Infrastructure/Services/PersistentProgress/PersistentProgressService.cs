using FreedLOW._Maze.Scripts.Data;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public PlayerProgress PlayerProgress { get; set; }
    }
}