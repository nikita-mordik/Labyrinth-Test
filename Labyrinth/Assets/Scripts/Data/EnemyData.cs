using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class EnemyData
    {
        public List<EnemyPositionOnLevel> EnemyPositionOnLevels = new List<EnemyPositionOnLevel>();

        public void AddEnemy(string id, Vector3Data position, Vector3Data rotation)
        {
            EnemyPositionOnLevel enemyData = new EnemyPositionOnLevel(id, position, rotation);
            EnemyPositionOnLevels.Add(enemyData);
        }
    }
}