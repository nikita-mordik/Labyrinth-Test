using System;
using System.Collections.Generic;

namespace FreedLOW._Maze.Scripts.Data
{
    [Serializable]
    public class EnemyData
    {
        public List<EnemyPositionOnLevel> EnemyPositionOnLevels = new();

        public void AddEnemy(string id, Vector3Data position, Vector3Data rotation)
        {
            EnemyPositionOnLevel enemyData = new EnemyPositionOnLevel(id, position, rotation);
            EnemyPositionOnLevels.Add(enemyData);
        }
    }
}