using FreedLOW._Maze.Scripts.Data;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad
{
    public interface ISaveLoadService : IService
    {
        void SaveProgress();
        PlayerProgress LoadProgress();
    }
}