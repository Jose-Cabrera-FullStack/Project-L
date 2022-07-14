using UnityEngine;
using System;

/// <summary> Route manager for spawning and holding the reference of <c>Routes</c> 
/// <value>route: ref to <c>Route</c></value>
/// <value>path: filepath for storing JSON data </value>
/// <summary>
[Serializable]
public class RouteCreator : MonoBehaviour
{
    [SerializeField]
    public Route route;
}
