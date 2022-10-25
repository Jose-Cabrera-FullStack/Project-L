using UnityEngine;
using Cinemachine;

public class CameraRegister : MonoBehaviour
{
    void OnEnable()
    {
        CameraSwitcher.Register(GetComponent<CinemachineVirtualCamera>());
    }

    void OnDisable()
    {
        CameraSwitcher.Unregister(GetComponent<CinemachineVirtualCamera>());
    }
}
