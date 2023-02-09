using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class RectangleTrigger : MonoBehaviour
{
    [SerializeField] Vector3 boxSize;
    BoxCollider boxCollider;
    Rigidbody rb;
    public bool isDetecting = true;
    public bool isContains = false;

    void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        boxCollider.isTrigger = true;
        boxCollider.size = boxSize;
        rb.isKinematic = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            isContains = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
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
