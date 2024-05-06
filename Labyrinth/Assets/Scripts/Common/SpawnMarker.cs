using Enemy;
using UnityEngine;

namespace Common
{
    public class SpawnMarker : MonoBehaviour
    {
        [SerializeField] private PatrollingType patrollingType;

        public PatrollingType PatrollingType => patrollingType;
    }
}