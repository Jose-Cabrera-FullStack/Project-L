using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    CinemachineVirtualCamera vcamera;
    Transform parent;
    Camera cam;

    void Awake()
    {
        vcamera = GetComponent<CinemachineVirtualCamera>();
        parent = vcamera.transform.parent;
        cam = parent.GetComponentInChildren<Camera>();
    }

    void OnEnable()
    {
        CameraManager.Register(cam);
    }

    void OnDisable()
    {
        CameraManager.Unregister(cam);
    }
}
