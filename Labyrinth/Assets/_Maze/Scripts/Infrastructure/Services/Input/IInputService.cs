using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
        bool IsAttackButtonUp();
    }
}