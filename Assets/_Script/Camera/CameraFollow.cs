using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    public WedgeTrigger trigger;
    public Transform target;
    public Transform cam;

    /// <summary>
    /// smoothingFactor is velocity to rotate to the target.
    /// </summary>
    public float smoothingFactor = 1f;

    // TODO: Define position to the camera at the start.

    void Update()
    {
        if (trigger.isContains(target.position))
        {
            Vector3 vecToTarget = target.position - cam.position;

            Quaternion targetRotation = Quaternion.LookRotation(vecToTarget, transform.up);
            cam.rotation = Quaternion.Slerp(cam.rotation, targetRotation, smoothingFactor * Time.deltaTime);
        }
    }
}
