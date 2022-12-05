using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;

public class CameraObject
{
    public CinemachineVirtualCamera virtualCam;
    public Camera cam;
    public CameraObject(CinemachineVirtualCamera vcam, Camera camera)
    {
        virtualCam = vcam;
        cam = camera;
    }
}

public static class CameraSwitcher
{
    public static List<CameraObject> cameras = new List<CameraObject>();
    static CameraObject selectedCamera = null;
    static int cameraIndex = 0;

    public static void NextCamera()
    {
        // Select the next vcamera or the first one in the cameras list.
        selectedCamera = cameraIndex + 1 < cameras.Count ? cameras[cameraIndex + 1] : cameras[0];
        SwitchCamera();
    }

    public static void PrevCamera()
    {
        // Select the previus vcamera or the last one in the cameras list.
        selectedCamera = cameraIndex - 1 >= 0 ? cameras[cameraIndex - 1] : cameras[^1];
        SwitchCamera();
    }

    static void SwitchCamera()
    {
        cameraIndex = cameras.FindIndex(vcamera => selectedCamera == vcamera);

        selectedCamera.virtualCam.Priority = 10;
        changeLayout();

        foreach (CameraObject c in cameras)
        {
            var index = cameras.IndexOf(c);

            Debug.Log($"Camera {index}:{c.virtualCam.Priority}");
            if (c != selectedCamera && c.virtualCam.Priority != 0)
            {
                c.virtualCam.Priority = 0;
                TextMeshProUGUI screenText = c.cam.GetComponentInChildren<TextMeshProUGUI>();
                screenText.SetText("");
            }
        }

    }

    static void changeLayout()
    {
        TextMeshProUGUI screenText = selectedCamera.cam.GetComponentInChildren<TextMeshProUGUI>();
        if (selectedCamera.virtualCam.Priority == 10)
        {
            screenText.SetText("Selected");
            // selectedCamera.rect = new Rect(1.0f, 0.0f, 1.0f - 0.5f * 2.0f, 1.0f);
            //  Camera.main.rect = new Rect (0, 0, 1, 1);

            Debug.Log($"layout changed");
        }
    }

    public static void Register(CameraObject camObj)
    {
        cameras.Add(camObj);
    }

    public static void Unregister(CameraObject camObj)
    {
        cameras.Remove(camObj);
    }
}
