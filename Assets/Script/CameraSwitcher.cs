using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public static class CameraSwitcher
{
    public static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;

    // static CinemachineVirtualCamera nextCamera = null;
    // static CinemachineVirtualCamera prevCamera = null;

    public static bool isActiveCamera(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(bool isNext)
    {
        int cameraIndex = cameras.FindIndex(camera => camera.Priority == 10);
        CinemachineVirtualCamera camera;
        if (cameraIndex == 0) camera = cameras[0];

        if (isNext)
        {
            camera = cameraIndex + 1 < cameras.Count ? cameras[cameraIndex + 1] : cameras[0];
        }
        else
        {
            camera = cameraIndex - 1 >= 0 ? cameras[cameraIndex - 1] : cameras[^1];
        }

        camera.Priority = 10;
        ActiveCamera = camera;

        foreach (CinemachineVirtualCamera c in cameras)
        {
            if (c != camera && c.Priority != 0)
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
