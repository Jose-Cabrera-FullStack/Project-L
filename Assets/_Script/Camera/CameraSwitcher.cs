using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using TMPro;


public static class CameraSwitcher
{
    public static List<CinemachineVirtualCamera> vcameras = new List<CinemachineVirtualCamera>();
    public static List<Camera> cameras = new List<Camera>();

    static CinemachineVirtualCamera selectedVCamera = null;
    static Camera selectedCamera = null;
    static int cameraIndex = 0;

    class CameraObject
    {
        public CinemachineVirtualCamera virtualCam;
        public Camera cam;
        public CameraObject(CinemachineVirtualCamera vcam, Camera camera)
        {
            virtualCam = vcam;
            cam = camera;
        }
    }

    // static CameraObject mcs = new CameraObject("FooBar");

    public static void NextCamera()
    {
        // Select the next vcamera or the first one in the vcameras list.
        selectedVCamera = cameraIndex + 1 < vcameras.Count ? vcameras[cameraIndex + 1] : vcameras[0];
        selectedCamera = cameraIndex + 1 < cameras.Count ? cameras[cameraIndex + 1] : cameras[0];
        SwitchCamera();
    }

    public static void PrevCamera()
    {
        // Select the previus vcamera or the last one in the vcameras list.
        selectedVCamera = cameraIndex - 1 >= 0 ? vcameras[cameraIndex - 1] : vcameras[^1];
        selectedCamera = cameraIndex - 1 >= 0 ? cameras[cameraIndex - 1] : cameras[^1];
        SwitchCamera();
    }

    static void SwitchCamera()
    {
        cameraIndex = vcameras.FindIndex(vcamera => selectedVCamera == vcamera);

        selectedVCamera.Priority = 10;

        foreach (CinemachineVirtualCamera c in vcameras)
        {
            if (c != selectedVCamera && c.Priority != 0)
            {
                c.Priority = 0;
                changeLayout(false);
            }
            else
            {
                changeLayout(true);
            }
        }

    }

    static void changeLayout(bool isSelected)
    {
        TextMeshProUGUI screenText = selectedCamera.GetComponentInChildren<TextMeshProUGUI>();
        if (isSelected)
        {
            screenText.SetText("Selected");
            // selectedCamera.rect = new Rect(1.0f, 0.0f, 1.0f - 0.5f * 2.0f, 1.0f);
            Debug.Log($"layout changed");
        }
        else
        {
            screenText.SetText("");
        }
    }

    public static void Register(CinemachineVirtualCamera vcamera, Camera camera)
    {
        vcameras.Add(vcamera);
        cameras.Add(camera);
    }

    public static void Unregister(CinemachineVirtualCamera vcamera, Camera camera)
    {
        vcameras.Remove(vcamera);
        cameras.Remove(camera);
    }
}
