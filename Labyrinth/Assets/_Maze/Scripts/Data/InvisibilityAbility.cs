using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class InvisibilityAbility
    {
        private int _invisibilityCount;
        
        public bool HasInvisibility => _invisibilityCount > 0;
        public Action OnInvisibilityCountChanged;

        public InvisibilityAbility(int invisibilityCount)
        {
            _invisibilityCount = invisibilityCount;
        }
        
        public void ChangeInvisibilityCount(int value)
        {
            _invisibilityCount += value;
            OnInvisibilityCountChanged?.Invoke();
        }
    }
}