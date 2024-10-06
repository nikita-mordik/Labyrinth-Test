using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class InvisibilityAbility
    {
        public int InvisibilityCount;
        public bool HasInvisibility => InvisibilityCount > 0;

        public Action<int> OnInvisibilityCountChanged;

        public InvisibilityAbility(int invisibilityCount)
        {
            InvisibilityCount = invisibilityCount;
        }
        
        public void ChangeInvisibilityCount(int value)
        {
            InvisibilityCount += value;
            OnInvisibilityCountChanged?.Invoke(InvisibilityCount);
        }
    }
}