using System.Linq;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Hero;
using FreedLOW._Maze.Scripts.Infrastructure.Services;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Identifiers;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.AI;

namespace FreedLOW._Maze.Scripts.Enemy
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
                        var heroMove = hit.collider.GetComponentInParent<HeroMove>();
                        if (heroMove != null && heroMove.HasInvisibility)
                        {
                            return false;
                        }
                        
                        return true;
                    }
                }
            }
            
            return false;
        }

        protected void UpdateEnemyData()
        {
            var progress = AllServices.Container.Single<IPersistentProgressService>().PlayerProgress;
            if (progress.WorldData.EnemyData.EnemyPositionOnLevels.Count == 0 || 
                !progress.WorldData.EnemyData.EnemyPositionOnLevels.Exists(t=> t.Id == uniqueId))
            {
                progress.WorldData.EnemyData.AddEnemy(uniqueId,
                    transform.position.AsVectorData(),
                    transform.rotation.eulerAngles.AsVectorData());
            }
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (progress.WorldData.EnemyData.EnemyPositionOnLevels.Count == 0)
                return;
            
            agent.ResetPath();
            agent.enabled = false;
            
            var enemyData = progress.WorldData.EnemyData.EnemyPositionOnLevels
                .First(t => t.Id == uniqueId);
            transform.position = enemyData.Position.AsUnityVector();
            var rotation = transform.rotation;
            rotation.eulerAngles = enemyData.Rotation.AsUnityVector();
            transform.rotation = rotation;
            
            agent.enabled = true;
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            if (progress.WorldData.GameData.IsRestart)
                return;
                
            var enemyData = progress.WorldData.EnemyData.EnemyPositionOnLevels
                .First(t => t.Id == uniqueId);
            enemyData.Position = transform.position.AsVectorData();
            enemyData.Rotation = transform.rotation.eulerAngles.AsVectorData();
        }
    }
}