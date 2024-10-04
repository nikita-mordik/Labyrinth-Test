using FreedLOW._Maze.Scripts.Data;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        /// <summary>
        /// Load saved progress
        /// </summary>
        /// <param name="progress">Progress data</param>
        void LoadProgress(PlayerProgress progress);
    }

    public interface ISavedProgress : ISavedProgressReader
    {
        /// <summary>
        /// Save progress
        /// </summary>
        /// <param name="progress">Progress data</param>
        void UpdateProgress(PlayerProgress progress);
    }
}