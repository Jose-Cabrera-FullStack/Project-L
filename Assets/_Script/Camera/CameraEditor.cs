using UnityEngine;

public class CameraEditor : MonoBehaviour
{
    void OnDrawGizmosSelected()
    {
        foreach (Camera camera in CameraManager.cameras)
        {
            Gizmos.DrawLine(transform.position, camera.transform.position);
        }
    }
}
