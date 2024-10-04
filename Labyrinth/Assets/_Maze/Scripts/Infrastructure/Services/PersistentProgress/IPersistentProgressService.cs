using FreedLOW._Maze.Scripts.Data;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface IPersistentProgressService : IService
    {
        PlayerProgress PlayerProgress { get; set; }
    }
}