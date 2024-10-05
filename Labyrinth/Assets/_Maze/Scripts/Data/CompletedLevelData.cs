using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class CompletedLevelData
    {
        public bool IsLevelTwoOpen;
        public bool IsLevelThreeOpen;

        public CompletedLevelData(bool isLevelTwoOpen, bool isLevelThreeOpen)
        {
            IsLevelTwoOpen = isLevelTwoOpen;
            IsLevelThreeOpen = isLevelThreeOpen;
        }
    }
}