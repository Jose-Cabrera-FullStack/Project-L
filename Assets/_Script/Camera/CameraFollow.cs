using UnityEngine;
using UnityEditor;

public class CameraFollow : MonoBehaviour
{
    // Update is called once per frame
    public RectangleTrigger trigger;
    Transform target;
    public Transform cam;

    /// <summary>
    /// smoothingFactor is velocity to rotate to the target.
    /// </summary>
    public float smoothingFactor = 1f;

    // TODO: Define position to the camera at the start.

    void Start()
    {
        target = GameObject.Find("Player").transform;
    }

    void Update()
    {
        if (trigger.isContains && trigger.isDetecting)
        {
            Vector3 vecToTarget = target.position - cam.position;
            Quaternion targetRotation = Quaternion.LookRotation(vecToTarget, transform.up);

            Vector3 eulerAngles = targetRotation.eulerAngles;
            // Target to the player
            eulerAngles.x -= 10;
            // Lock z axis
            eulerAngles.z = 0;
            targetRotation.eulerAngles = eulerAngles;

            cam.rotation = Quaternion.Slerp(cam.rotation, targetRotation, smoothingFactor * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1.0f);
    }
}
