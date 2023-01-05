using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RectangleTrigger : MonoBehaviour
{
    [SerializeField] Vector3 boxSize;
    BoxCollider box;
    Rigidbody rb;
    public bool isDetecting = true;
    public bool isContains = false;

    void Awake()
    {
        box = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        box.isTrigger = true;
        box.size = boxSize;

        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        isContains = true;
    }

    void OnTriggerExit(Collider other)
    {
        isContains = false;
    }

    void OnDrawGizmosSelected()
    {
        if (isDetecting)
        {
            Gizmos.color = isContains ? Color.white : Color.red;
            Gizmos.DrawWireCube(transform.position, boxSize);
        }
    }

}
