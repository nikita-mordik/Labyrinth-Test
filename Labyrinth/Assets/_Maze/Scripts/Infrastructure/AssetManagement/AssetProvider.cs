using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public GameObject Instantiate(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject, at, Quaternion.identity);
        }

        public T Load<T>(string path) where T : Object
        {
            return Resources.Load<T>(path);
        }
    }
}