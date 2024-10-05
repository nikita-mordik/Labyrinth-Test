using FreedLOW._Maze.Scripts.Common;
using FreedLOW._Maze.Scripts.Data;
using FreedLOW._Maze.Scripts.Infrastructure.Services.Input;
using FreedLOW._Maze.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FreedLOW._Maze.Scripts.Hero
{
    public class HeroMove : MonoBehaviour, ISavedProgress
    {
        [SerializeField] private CharacterController characterController;
        [SerializeField] private float movementSpeed;

        private Camera _mainCamera;

        public bool HasInvisibility => _heroStats.HasInvisibility;
        
        private IInputService _inputService;
        private HeroStats _heroStats;

        public void Construct(IInputService input, HeroStats heroStats)
        {
            _inputService = input;
            _heroStats = heroStats;
        }
        
        private void Awake()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _mainCamera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();
                transform.forward = movementVector;
            }

            movementVector += Physics.gravity;
            
            characterController.Move(movementVector * movementSpeed * _heroStats.BoostSpeedValue * Time.deltaTime);
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel(CurrentLevel(), transform.position.AsVectorData());
        }

        public void LoadProgress(PlayerProgress progress)
        {
            if (CurrentLevel() == progress.WorldData.PositionOnLevel.LevelName)
            {
                var savedPosition = progress.WorldData.PositionOnLevel.Position;
                if (savedPosition != null) 
                    Warp(to: savedPosition);
            }
        }

        private void Warp(Vector3Data to)
        {
            characterController.enabled = false;
            transform.position = to.AsUnityVector()
                .ToUpY(y: characterController.height);
            characterController.enabled = true;
        }

        private static string CurrentLevel() => 
            SceneManager.GetActiveScene().name;
    }
}