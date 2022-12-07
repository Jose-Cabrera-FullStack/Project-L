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

// #TODO: rename to a CameraManager
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
        Debug.Log($"cameraIndex:{cameraIndex}");

        selectedCamera.virtualCam.Priority = 10;
        int counter = 1; // Contador para la division para la position en y (0.25, 0.5, 0.75)
        int counter2 = 0; // contador para inicializar en la position 0

        foreach (CameraObject camera in cameras)
        {
            float split = (float)1 / ((cameras.Count - 1) * counter);

            if (camera != selectedCamera && camera.virtualCam.Priority != 0)
            {

                camera.virtualCam.Priority = 0;
                TextMeshProUGUI screenText = camera.cam.GetComponentInChildren<TextMeshProUGUI>();
                // screenText.SetText($"{cameras.Count}"); Falta arreglar cosas Camera 2

                // Debug.Log($"split:{split}");
                // Debug.Log($"counter:{counter}");
                // Debug.Log($"counter2:{counter2}");
                camera.cam.rect = new Rect(0, 0.5f, split * counter, split * counter);
                // counter = counter + 1;
            }
            else if (camera != selectedCamera && camera.virtualCam.Priority == 0)
            {
                camera.cam.rect = new Rect(0, split * counter2, split, split);
                // counter2 = counter2 + 1;
                counter = counter + 1;

            }
            else
            {
                camSelectedLayout(camera);
            }

        }

    }

    static void camSelectedLayout(CameraObject cameraObj)
    {
        TextMeshProUGUI screenText = cameraObj.cam.GetComponentInChildren<TextMeshProUGUI>();
        cameraObj.cam.rect = new Rect(0.5f, 0, 0.5f, 1);
        if (cameraObj.virtualCam.Priority == 10)
        {
            // screenText.SetText($"Soy la seleccionada");
            // if (cameraObj.cam.rect.x == -0.5f)

            // Left side
            // Rect(0, aqui, aqui, 0);

            // Right side (selected camera)
            // Rect(0.5, 0, 0.5, 1);
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
