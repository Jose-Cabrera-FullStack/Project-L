using UnityEngine;

// Use with precaution "ExecuteAlways"
[ExecuteAlways]
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
