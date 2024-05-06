using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Infrastructure.AssetManagement
{
    public class AssetProvider : IAssetProvider
    {
        public AssetProvider()
        {
            
        }
        
        public GameObject Instantiate(string path)
        {
            var gameObject = Resources.Load<GameObject>(path);
            return Object.Instantiate(gameObject);
        }

        public GameObject Instantiate(string path, Vector3 at)
        {
            throw new System.NotImplementedException();
        }

        // public List<WeatherCoord> LoadWeatherCoords() => 
        //     Resources.LoadAll<WeatherCoord>(AssetsPath.AllWeatherData).ToList();
    }
}