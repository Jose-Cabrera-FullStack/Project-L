using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        foreach (var camera in CameraManager.cameras)
        {
            Gizmos.DrawLine(transform.position, camera.transform.position);
        }
    }
}
