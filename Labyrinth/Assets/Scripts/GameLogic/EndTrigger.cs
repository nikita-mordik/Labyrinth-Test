using Hero;
using UnityEngine;

namespace GameLogic
{
    public class EndTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<HeroMove>(out var hero))
            {
                Debug.LogError("finished");
            }
        }
    }
}