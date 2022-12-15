using System.Collections.Generic;
using UnityEngine;

public static class CameraManager
{
    public static List<Camera> cameras = new List<Camera>();
    static Camera selectedCamera = null;
    static int cameraSelectedIndex = 0;

    public static void Register(Camera camera)
    {
        cameras.Add(camera);
    }

    public static void Unregister(Camera camera)
    {
        cameras.Remove(camera);
    }

    public static void NextCamera()
    {
        // Select the next camera or the first one in the cameras list.
        selectedCamera = cameraSelectedIndex + 1 < cameras.Count ? cameras[cameraSelectedIndex + 1] : cameras[0];
        switchCamera();
    }

    public static void PrevCamera()
    {
        // Select the previus camera or the last one in the cameras list.
        selectedCamera = cameraSelectedIndex - 1 >= 0 ? cameras[cameraSelectedIndex - 1] : cameras[^1];
        switchCamera();
    }

    static void switchCamera()
    {
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

            if (camera != selectedCamera && camera.depth != 0)
            {
                /// <summary>
                /// This is the camera that change state recenly to seleted to unseleted.
                /// Its position is at the begining of the layout .
                /// </summary>
                camera.depth = 0;
                camera.rect = new Rect(0, (float)(unselectedCameras - 1) / unselectedCameras, 0.5f, split);

            }
            else if (camera != selectedCamera && camera.depth == 0)
            {
                /// <summary>
                /// Split other layout positions from unseleted cameras.
                /// </summary>
                camera.rect = new Rect(0, split * cameraPositioned, 0.5f, split);
                cameraPositioned = cameraPositioned + 1;
            }
            else
            {
                /// <summary>
                /// It the position from the seleted camera.
                /// </summary>
                camera.rect = new Rect(0.5f, 0, 0.5f, 1);
            }

        }
    }
}

// TODO: Refactorizar esta clase

// La función CameraManager.NextCamera y CameraManager.PrevCamera pueden fusionarse en una sola función que reciba un parámetro booleano para determinar si se debe avanzar o retroceder en la lista de cámaras.

// En lugar de tener una variable cameraSelectedIndex y otra selectedCamera, se puede usar una única variable que represente el índice de la cámara seleccionada en la lista.

// La función CameraManager.switchCamera no es necesaria, ya que su lógica puede incluirse directamente en las funciones CameraManager.NextCamera y CameraManager.PrevCamera.

// La función CameraManager.changeLayout se puede mejorar para hacerla más legible y fácil de entender. Por ejemplo, se pueden usar nombres de variables más descriptivos y se pueden agregar comentarios que expliquen lo que hace el código.

// using System.Collections.Generic;
// using UnityEngine;

// public static class CameraManager
// {
//     // Lista de cámaras registradas.
//     public static List<Camera> cameras = new List<Camera>();

//     // Índice de la cámara seleccionada en la lista de cámaras.
//     static int selectedCameraIndex = 0;

//     // Regístra una nueva cámara.
//     public static void Register(Camera camera)
//     {
//         cameras.Add(camera);
//     }

//     // Desregistra una cámara.
//     public static void Unregister(Camera camera)
//     {
//         cameras.Remove(camera);
//     }

//     // Cambia la cámara seleccionada a la siguiente o a la anterior de la lista de cámaras.
//     public static void ChangeSelectedCamera(bool next)
//     {
//         // Calcula el índice de la cámara seleccionada en la lista.
//         selectedCameraIndex = next ? 
//             (selectedCameraIndex + 1) % cameras.Count : 
//             (selectedCameraIndex - 1 + cameras.Count) % cameras.Count;

//         // Cambia el orden de profundidad de la cámara seleccionada y las demás.
//         cameras[selectedCameraIndex].depth = 10;
//         for (int i = 0; i < cameras.Count; i++)
//         {
//             if (i != selectedCameraIndex)
//             {
//                 cameras[i].depth = 0;
//             }
//         }

//         // Cambia el diseño de las cámaras.
//         ChangeLayout();
//     }

//     // Cambia el diseño de las cámaras.
//     static void ChangeLayout()
//     {
//           // Número de cámaras que no son la cámara seleccionada.
//     int unselectedCameras = cameras.Count - 1;

//     // Porcentaje de la pantalla que ocupa cada cámara no seleccionada.
//     float split = (float)1 / unselectedCameras;

//     // Recorre todas las cámaras.
//     for (int i = 0; i < cameras.Count; i++)
//     {
//         // Obtiene la cámara actual.
//         Camera camera = cameras[i];

//         // Si la cámara actual no es la seleccionada...
//         if (i != selectedCameraIndex)
//         {
//             // Si la cámara tenía el orden de profundidad 10...
//             if (camera.depth == 10)
//             {
//                 // La coloca al comienzo del diseño.
//                 camera.rect = new Rect(0, (float)(unselectedCameras - 1) / unselectedCameras, 0.5f, split);
//             }
//             else
//             {
//                 // De lo contrario, la divide entre el resto de cámaras no seleccionadas.
//                 camera.rect = new Rect(0, split * i, 0.5f, split);
//             }
//         }
//         // Si la cámara actual es la seleccionada...
//         else
//         {
//             // La coloca en la mitad derecha de la pantalla.
//             camera.rect = new Rect(0.5f, 0, 0.5f, 1);
//         }
//     }
//     }}

// Para llegar a esta conclusión, primero analicé el código y busqué formas de simplificarlo y mejorar su legibilidad. Esto implicó identificar repetidos patrones de código y buscar formas de reutilizarlos en lugar de repetirlos varias veces. También implicó buscar formas de eliminar variables y funciones innecesarias y agregar comentarios que ayuden a entender el propósito del código.

// Por último, es importante tener en cuenta que este es solo un ejemplo de cómo podría mejorarse el código y que hay muchas otras formas posibles de hacerlo. La idea es buscar formas de simplificar y mejorar el código, pero siempre teniendo en cuenta las necesidades específicas de cada proyecto y de cada equipo de desarrollo.