using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class BoostAbility
    {
        private int _boostCount;
        
        public bool HasBoost => _boostCount > 0;
        public int BoostCount => _boostCount;

        public Action<int> OnBoostCountChanged;

        public BoostAbility(int boostCount)
        {
            _boostCount = boostCount;
        }
        
        public void ChangeBoostCount(int value)
        {
            _boostCount += value;
            OnBoostCountChanged?.Invoke(_boostCount);
        }
    }
}