using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    void OnDrawGizmos()
    {
        foreach (var camera in CameraManager.cameras)
        {
            Gizmos.DrawLine(transform.position, camera.transform.position);
        }
    }
}
