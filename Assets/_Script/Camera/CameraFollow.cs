using UnityEngine;
using UnityEditor;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] RectangleTrigger trigger;
    [SerializeField] Transform target;
    [SerializeField] Transform cam;

    [Range(0f, 1f)][SerializeField] float smoothingFactor = 1f;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        Handles.color = Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y, transform.position.z), 1.0f);
    }
#endif
}