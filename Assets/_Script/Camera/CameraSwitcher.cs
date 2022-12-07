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
        selectedCamera.virtualCam.Priority = 10;
        changeLayout();
    }

    static void changeLayout()
    {
        int counter = 0; // Contador para la division para la position en y (0.25, 0.5, 0.75)
        int unselectedCameras = cameras.Count - 1;
        float split = (float)1 / (unselectedCameras);

        foreach (CameraObject camera in cameras)
        {

            if (camera != selectedCamera && camera.virtualCam.Priority != 0)
            {
                /// <summary>
                /// Aqui esta la camara que pasa de estar seleccionada a no estarla.
                /// </summary>
                camera.virtualCam.Priority = 0;
                camera.cam.rect = new Rect(0, (float)(unselectedCameras - 1) / unselectedCameras, 0.5f, split);

            }
            else if (camera != selectedCamera && camera.virtualCam.Priority == 0)
            {
                /// <summary>
                /// Todas las camaras no selecionadas.
                /// </summary>
                camera.cam.rect = new Rect(0, split * counter, 0.5f, split);
                counter = counter + 1;
            }
            else
            {
                /// <summary>
                /// Aqui esta la camara seleccionada.
                /// </summary>
                camera.cam.rect = new Rect(0.5f, 0, 0.5f, 1);
            }

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
