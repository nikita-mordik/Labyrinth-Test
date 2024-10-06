using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Enemy
{
    public class EnemyImmovableAIController : BaseAIEnemy
    {
        private Vector3 initialPosition;
        private Quaternion initialRotation;

        private void Update()
        {
            if (IsPlayerVisible())
            {
                agent.SetDestination(player.position);
            }
            else if (agent.enabled && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                agent.SetDestination(initialPosition);
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    transform.rotation = initialRotation;
                }
            }
        }

        public override void Initialize(Transform point, GameObject hero)
        {
            player = hero.transform;

            transform.position = point.position;
            transform.rotation = point.rotation;
            
            initialPosition = transform.position;
            initialRotation = transform.rotation;
            agent.enabled = true;
            
            GenerateId();
            UpdateEnemyData();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color=Color.magenta;
            
            Vector3 directionToPlayer = player.position - transform.position;
            float playerDetectionRange = directionToPlayer.magnitude;
            
            float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

            if (angleToPlayer <= fieldOfViewAngle * 0.5f)
            { 
                Gizmos.DrawRay(transform.position, directionToPlayer);
            }
        }
    }
}