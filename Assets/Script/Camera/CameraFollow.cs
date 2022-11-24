using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    public WedgeTrigger trigger;
    public Transform target;
    public Transform cam;
    void Update()
    {
        if (trigger.isContains(target.position))
        {
            Vector3 vecToTarget = target.position - cam.position;
            //TODO: Smooth the rotation
            cam.rotation = Quaternion.LookRotation(vecToTarget, transform.up);
        }
    }
}
