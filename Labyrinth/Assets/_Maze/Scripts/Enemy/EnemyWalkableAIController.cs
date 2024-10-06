using System.Collections.Generic;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using FreedLOW._Maze.Scripts.Infrastructure.Services.SaveLoad;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Enemy
{
    public class EnemyWalkableAIController : BaseAIEnemy
    {
        private readonly List<Transform> waypoints = new();
        
        private int currentWaypointIndex;

        private void Update()
        {
            if (IsPlayerVisible())
            {
                agent.SetDestination(player.position);
            }
            else if (agent.enabled && !agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetNextWaypoint();
            }
        }

        public override void Initialize(Transform point, GameObject hero)
        {
            player = hero.transform;
            
            transform.position = point.position;
            transform.rotation = point.rotation;

            for (int i = 0; i < point.childCount; i++)
            {
                var wayPoint = point.GetChild(i);
                waypoints.Add(wayPoint);
            }
            
            agent.enabled = true;
            
            SetNextWaypoint();
            GenerateId();
            UpdateEnemyData();
        }

        private void SetNextWaypoint()
        {
            if (waypoints.Count == 0) return;

            agent.SetDestination(waypoints[currentWaypointIndex].position);
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Count;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            
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