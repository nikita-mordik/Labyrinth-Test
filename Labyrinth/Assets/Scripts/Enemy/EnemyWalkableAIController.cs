using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyWalkableAIController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private Transform[] waypoints;
        [SerializeField] private float fieldOfViewAngle = 45f;

        private int playerLayer;
        private Transform player;
        private int currentWaypointIndex;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerLayer = LayerMask.NameToLayer("Hero");
            SetNextWaypoint();
        }

        private void Update()
        {
            if (IsPlayerVisible())
            {
                agent.SetDestination(player.position);
            }
            else if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetNextWaypoint();
            }
        }

        private void SetNextWaypoint()
        {
            if (waypoints.Length == 0) return;

            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }

        private bool IsPlayerVisible()
        {
            Vector3 directionToPlayer = player.position - transform.position;
            float playerDetectionRange = directionToPlayer.magnitude;
            
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle * 0.5f)
            {
                if (Physics.Raycast(transform.position, directionToPlayer, out var hit, playerDetectionRange))
                {
                    if (hit.collider.gameObject.layer == playerLayer)
                    {
                        return true;
                    }
                }
            }
            
            return false;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.magenta;
            
            /*ector3 directionToPlayer = player.position - transform.position;
            float playerDetectionRange = directionToPlayer.magnitude;
            
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle * 0.5f)
            { 
                Gizmos.DrawRay(transform.position, directionToPlayer);
            }*/
        }
    }
}