using System;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class AbilityData
    {
        public BoostAbility BoostAbility;
        public InvisibilityAbility InvisibilityAbility;

        public AbilityData(BoostAbility boostAbility, InvisibilityAbility invisibilityAbility)
        {
            BoostAbility = boostAbility;
            InvisibilityAbility = invisibilityAbility;
        }
    }
}