using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    public static List<Camera> cameras = new List<Camera>();
    // TODO: Check if this random camera selected could be a problem in the future
    public static Camera selectedCamera = GameObject.FindGameObjectWithTag("Camera").GetComponent<Camera>();
    static int cameraSelectedIndex = 0;

    public static void Register(Camera camera)
    {
        cameras.Add(camera);
        switchCamera();
    }

    public static void Unregister(Camera camera)
    {
        cameras.Remove(camera);
        switchCamera();
    }

    public static void NextCamera()
    {
        // Select the next camera or the first one in the cameras list.
        selectedCamera = cameraSelectedIndex + 1 < cameras.Count ? cameras[cameraSelectedIndex + 1] : cameras[0];
        switchCamera();
    }

    static void switchCamera()
    {
        // cameras = cameras.FindAll(camera => camera.enabled);
        cameraSelectedIndex = cameras.FindIndex(camera => selectedCamera == camera);
        selectedCamera.depth = 10;
        changeLayout();
    }

    static void changeLayout()
    {
        int cameraPositioned = 0; // It count the number of cameras in the layout to be divided in normalized "y"
        int unselectedCameras = cameras.Count - 1;
        float split = (float)1 / (unselectedCameras);

        foreach (Camera camera in cameras)
        {
            // Select only DarkRoom Cameras

            if (camera != selectedCamera && camera.depth != 0)
            {
                /// <summary>
                /// This is the camera that change state recenly to seleted to unseleted.
                /// Its position is at the begining of the layout.
                /// </summary>
                camera.depth = 0;
                camera.rect = new Rect((float)(unselectedCameras - 1) / unselectedCameras, 0, split, 0.5f);

            }
            else if (camera != selectedCamera && camera.depth == 0)
            {
                /// <summary>
                /// Split other layout positions from unseleted cameras.
                /// </summary>
                camera.rect = new Rect(split * cameraPositioned, 0, split, 0.5f);
                cameraPositioned = cameraPositioned + 1;
            }
            else
            {
                /// <summary>
                /// It the position from the seleted camera.
                /// </summary>
                camera.rect = new Rect(0, 0.25f, 1, 1);
            }

        }
    }
}
