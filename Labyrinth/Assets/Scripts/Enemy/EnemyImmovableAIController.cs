using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyImmovableAIController : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent agent;
        [SerializeField] private float fieldOfViewAngle = 45f;

        private int playerLayer;
        private Transform player;

        private void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            playerLayer = LayerMask.NameToLayer("Hero");
        }

        private void Update()
        {
            if (IsPlayerVisible())
            {
                agent.SetDestination(player.position);
            }
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