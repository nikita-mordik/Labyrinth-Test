using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.Input
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => MobileAxis();
    }
}