using UnityEngine;

public class CameraActiveTrigger : MonoBehaviour
{
    [SerializeField] string layerSelected = "DarkRoom";
    void OnTriggerEnter(Collider other)
    {
        CameraManager.cameras.Clear();
        int layerIndex = LayerMask.NameToLayer(layerSelected);

        Camera[] cameras = Object.FindObjectsOfType<Camera>();

        foreach (Camera camera in cameras)
        {
            if (camera.gameObject.layer == layerIndex)
            {
                CameraManager.Register(camera);
                camera.enabled = true;
            }
            else
            {
                CameraManager.Unregister(camera);
                camera.enabled = false;
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, transform.localScale);
    }


}
