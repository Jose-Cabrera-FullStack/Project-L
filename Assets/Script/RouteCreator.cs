using UnityEngine;
using System;
using System.IO;
using System.Threading.Tasks;

/// <summary> Route manager for spawning and holding the reference of <c>Routes</c> 
/// <value>route: ref to <c>Route</c></value>
/// <value>path: filepath for storing JSON data </value>
/// <summary>
[Serializable]
public class RouteCreator : MonoBehaviour
{
    [SerializeField]
    public Route route;
    [SerializeField]
    private string path;

    public void CreateRoute()
    {
        // Load Route from or create default
        Debug.Log("creation");
        route = new Route(transform.position);
    }

    // Initialize the path for storing the routes
    public void Awake()
    {
        path = string.Format("{0}/routes.json", Application.dataPath);
    }

    /// <summary> set the midpoint y value in all the points of the Route</summary>
    public void alignHorizontal()
    {
        var sum = 0f;
        for (int i = 0; i < route.len - 1; i++)
        {
            sum += route[i].y;
        }

        sum = sum / route.len;

        for (int i = 0; i < route.len - 1; i++)
        {
            var current = route[i];
            route.movePoint(i, new Vector3(current.x, sum, current.z));
        }
    }

    public async void Save()
    {
        string data = JsonUtility.ToJson(route);
        await File.WriteAllTextAsync(path, data);
    }

    public async Task Load()
    {
        if (File.Exists(path))
        {
            Debug.Log("Data loading");

            var data = await File.ReadAllTextAsync(path);
            route = JsonUtility.FromJson<Route>(data);
        }

    }

}
