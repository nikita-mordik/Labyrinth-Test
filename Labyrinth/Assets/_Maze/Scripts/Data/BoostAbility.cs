using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class BoostAbility
    {
        public int BoostCount;
        public bool HasBoost => BoostCount > 0;

        public Action<int> OnBoostCountChanged;

        public BoostAbility(int boostCount)
        {
            BoostCount = boostCount;
        }
        
        public void ChangeBoostCount(int value)
        {
            BoostCount += value;
            OnBoostCountChanged?.Invoke(BoostCount);
        }
    }
}