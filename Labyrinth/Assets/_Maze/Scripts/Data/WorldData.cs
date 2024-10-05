using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        public GameData GameData;
        public EnemyData EnemyData;
        public CompletedLevelData CompletedLevelData;
        public MoneyData MoneyData;
        public AbilityData AbilityData;

        public WorldData(string initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
            GameData = new GameData();
            EnemyData = new EnemyData();
            CompletedLevelData = new CompletedLevelData(isLevelTwoOpen: false, isLevelThreeOpen: false);
            MoneyData = new MoneyData(100);
            AbilityData = new AbilityData(new BoostAbility(1), new InvisibilityAbility(0));
        }
    }
}