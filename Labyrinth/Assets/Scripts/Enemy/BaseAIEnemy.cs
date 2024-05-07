using System.Linq;
using Data;
using Infrastructure.Services.Identifiers;
using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public abstract class BaseAIEnemy : MonoBehaviour, ISavedProgress
    {
        [SerializeField] protected NavMeshAgent agent;
        [SerializeField] protected float fieldOfViewAngle = 60f;

        protected Transform player;
        
        private string uniqueId;
        private int playerLayer;
        
        private IIdentifierService identifierService;

        public void Construct(IIdentifierService identifierService)
        {
            this.identifierService = identifierService;
        }

        private void Awake()
        {
            playerLayer = LayerMask.NameToLayer("Hero");
        }

        public abstract void Initialize(Transform point, GameObject hero);

        protected void GenerateId() => 
            uniqueId = $"{identifierService.Next()}";

        protected bool IsPlayerVisible()
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

        public void LoadProgress(PlayerProgress progress)
        {
            foreach (var enemyPositionOnLevel in progress.WorldData.EnemyData.EnemyPositionOnLevels)
                         //.Where(enemyPositionOnLevel => enemyPositionOnLevel.Id == uniqueId))
            {
                transform.position = enemyPositionOnLevel.Position.AsUnityVector();
                var rotation = transform.rotation;
                rotation.eulerAngles = enemyPositionOnLevel.Rotation.AsUnityVector();
                transform.rotation = rotation;
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.WorldData.EnemyData.EnemyPositionOnLevels.Count <= 0 || 
                progress.WorldData.EnemyData.EnemyPositionOnLevels.All(t => t.Id != uniqueId))
            {
                progress.WorldData.EnemyData.AddEnemy(uniqueId, transform.position.AsVectorData(),
                    transform.rotation.eulerAngles.AsVectorData());
            }
            else
            {
                foreach (var enemyPositionOnLevel in progress.WorldData.EnemyData.EnemyPositionOnLevels
                             .Where(enemyPositionOnLevel => enemyPositionOnLevel.Id == uniqueId))
                {
                    enemyPositionOnLevel.Position = transform.position.AsVectorData();
                    enemyPositionOnLevel.Rotation = transform.rotation.eulerAngles.AsVectorData();
                }   
            }
        }
    }
}