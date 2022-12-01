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

        CameraSwitcher.Register(vcamera, cam);
    }

    void OnDisable()
    {
        CameraSwitcher.Unregister(vcamera, cam);
    }
}
