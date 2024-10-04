using System.Collections;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}