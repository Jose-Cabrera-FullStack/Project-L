using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    public WedgeTrigger trigger;
    public Transform target;
    public Transform cam;
    public float smoothingFactor = 1f;

    // void Start()
    // {

    // TODO: Define position to the camera at the start.

    //     transform.position = new Vector3(transform.position.x, transform.position.y + (trigger.height / 2), transform.position.z);
    //     transform.rotation = trigger.transform.rotation;
    // }
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
