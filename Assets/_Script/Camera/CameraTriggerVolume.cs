using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraTriggerVolume : MonoBehaviour
{
    [SerializeField] Vector3 boxSize;
    BoxCollider box;
    Rigidbody rb;

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

    void OnDrawGizmos()
    {
        Gizmos.color = isContains ? Color.white : Color.red;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

}
