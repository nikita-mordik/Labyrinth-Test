using FreedLOW._Maze.Scripts.Enemy;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Gameplay
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private PatrollingType patrollingType;

        public PatrollingType PatrollingType => patrollingType;
    }
}