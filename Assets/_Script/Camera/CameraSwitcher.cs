using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    public static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;

    static CinemachineVirtualCamera selectedCamera = null;
    static int cameraIndex = 0;

    static private void Update()
    {
        // TODO: Need to change to a single call operation.
        selectedCamera = cameras[0];
    }

    public static bool isActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void NextCamera()
    {
        // Select the next camera or the first one in the cameras list.
        selectedCamera = cameraIndex + 1 < cameras.Count ? cameras[cameraIndex + 1] : cameras[0];
        SwitchCamera();
        // CameraLayout.switchLayoutPosition();
    }

    public static void PrevCamera()
    {
        // Select the previus camera or the last one in the cameras list.
        selectedCamera = cameraIndex - 1 >= 0 ? cameras[cameraIndex - 1] : cameras[^1];
        SwitchCamera();
    }

    static void SwitchCamera()
    {
        cameraIndex = cameras.FindIndex(camera => selectedCamera == camera);

        selectedCamera.Priority = 10;
        ActiveCamera = selectedCamera;

        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != selectedCamera && c.Priority != 0)
            {
                c.Priority = 0;
            }
        }
    }

    public static void Register(CinemachineVirtualCamera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera camera)
    {
        cameras.Remove(camera);
    }
}
