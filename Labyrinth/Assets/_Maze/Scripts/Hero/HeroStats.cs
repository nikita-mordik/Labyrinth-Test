using System.Collections;
using FreedLOW._Maze.Scripts.Infrastructure;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Hero
{
    public class HeroStats
    {
        private readonly int _boostValue;
        private readonly ICoroutineRunner _coroutineRunner;

        public int BoostSpeedValue { get; private set; } = 1;
        public bool HasInvisibility { get; private set; }

        public HeroStats(int boostSpeedValue, ICoroutineRunner coroutineRunner)
        {
            _boostValue = boostSpeedValue;
            _coroutineRunner = coroutineRunner;
        }

        public void ApplyInvisibilityToHero()
        {
            _coroutineRunner.StartCoroutine(InvisibilityRoutine());
        }

        public void ApplyBoostToHero()
        {
            _coroutineRunner.StartCoroutine(BoostRoutine());
        }

        private IEnumerator BoostRoutine()
        {
            BoostSpeedValue = _boostValue;
            var duration = 5f;
            while (duration > 0f)
            {
                duration -= Time.deltaTime;
                yield return null;
            }

            BoostSpeedValue = 1;
        }

        private IEnumerator InvisibilityRoutine()
        {
            HasInvisibility = true;
            var duration = 5f;
            while (duration > 0f)
            {
                duration -= Time.deltaTime;
                yield return null;
            }
            
            HasInvisibility = false;
        }
    }
}