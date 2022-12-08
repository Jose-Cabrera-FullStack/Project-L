using UnityEngine;

public class CameraRegister : MonoBehaviour
{
    void OnEnable()
    {
        CameraManager.Register(GetComponent<Camera>());
    }

    void OnDisable()
    {
        CameraManager.Unregister(GetComponent<Camera>());
    }
}
