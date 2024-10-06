using UnityEngine;

namespace FreedLOW._Maze.Scripts.Gameplay
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private float rotationAngle = 2f;
        
        private void LateUpdate()
        {
            transform.Rotate(transform.up, rotationAngle * Time.deltaTime, Space.World);
        }
    }
}