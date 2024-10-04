using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public GameData GameData;
        public EnemyData EnemyData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            GameData = new GameData();
            EnemyData = new EnemyData();
        }
    }
}