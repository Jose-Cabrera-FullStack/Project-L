using UnityEngine;
using UnityEditor;


public class CameraFieldOfView : MonoBehaviour
{

    void OnDrawGizmos()
    {
        Camera cam = GetComponent<Camera>();
        // Set the Handles color to red
        Handles.color = Color.red;

        // Calculate the distance between the camera and the near and far clip planes
        float nearDist = cam.nearClipPlane;
        float farDist = cam.farClipPlane;

        // Calculate the aspect ratio of the cam
        float aspect = cam.aspect;

        // Calculate the field of view in radians
        float fovRad = cam.fieldOfView * Mathf.Deg2Rad;

        // Calculate the height and width of the frustum at the near and far clip planes
        float nearHeight = 2.0f * Mathf.Tan(fovRad / 2.0f) * nearDist;
        float nearWidth = nearHeight * aspect;
        float farHeight = 2.0f * Mathf.Tan(fovRad / 2.0f) * farDist;
        float farWidth = farHeight * aspect;

        // Calculate the positions of the frustum corners at the near and far clip planes
        Vector3 topLeftNear = cam.transform.position + cam.transform.forward * nearDist - cam.transform.up * nearHeight / 2.0f - cam.transform.right * nearWidth / 2.0f;
        Vector3 topRightNear = cam.transform.position + cam.transform.forward * nearDist - cam.transform.up * nearHeight / 2.0f + cam.transform.right * nearWidth / 2.0f;
        Vector3 bottomLeftNear = cam.transform.position + cam.transform.forward * nearDist + cam.transform.up * nearHeight / 2.0f - cam.transform.right * nearWidth / 2.0f;
        Vector3 bottomRightNear = cam.transform.position + cam.transform.forward * nearDist + cam.transform.up * nearHeight / 2.0f + cam.transform.right * nearWidth / 2.0f;
        Handles.DrawLine(topLeftNear, topRightNear);
        Handles.DrawLine(topRightNear, bottomRightNear);
        Handles.DrawLine(bottomRightNear, bottomLeftNear);
        Handles.DrawLine(bottomLeftNear, topLeftNear);

        // Calculate the positions of the frustum corners at the far clip plane
        Vector3 topLeftFar = cam.transform.position + cam.transform.forward * farDist - cam.transform.up * farHeight / 2.0f - cam.transform.right * farWidth / 2.0f;
        Vector3 topRightFar = cam.transform.position + cam.transform.forward * farDist - cam.transform.up * farHeight / 2.0f + cam.transform.right * farWidth / 2.0f;
        Vector3 bottomLeftFar = cam.transform.position + cam.transform.forward * farDist + cam.transform.up * farHeight / 2.0f - cam.transform.right * farWidth / 2.0f;
        Vector3 bottomRightFar = cam.transform.position + cam.transform.forward * farDist + cam.transform.up * farHeight / 2.0f + cam.transform.right * farWidth / 2.0f;

        // Draw the far clip plane of the frustum
        Handles.DrawLine(topLeftFar, topRightFar);
        Handles.DrawLine(topRightFar, bottomRightFar);
        Handles.DrawLine(bottomRightFar, bottomLeftFar);
        Handles.DrawLine(bottomLeftFar, topLeftFar);

        // Draw the vertical lines connecting the frustum corners at the near and far clip planes
        Handles.DrawLine(topLeftNear, topLeftFar);
        Handles.DrawLine(topRightNear, topRightFar);
        Handles.DrawLine(bottomLeftNear, bottomLeftFar);
        Handles.DrawLine(bottomRightNear, bottomRightFar);
    }
}
