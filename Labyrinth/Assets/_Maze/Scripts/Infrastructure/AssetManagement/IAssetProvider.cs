using FreedLOW._Maze.Scripts.Infrastructure.Services;
using UnityEngine;

namespace FreedLOW._Maze.Scripts.Infrastructure.AssetManagement
{
    public interface IAssetProvider : IService
    {
        GameObject Instantiate(string path);
        GameObject Instantiate(string path, Vector3 at);
        //List<WeatherCoord> LoadWeatherCoords();
    }
}