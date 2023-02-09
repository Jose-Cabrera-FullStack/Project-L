using UnityEngine;
using System.Collections;
public class Push : MonoBehaviour, IInteractable
{
    [SerializeField] Rigidbody rb;
    [SerializeField] float force = 10f;
    bool isPushing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public string InteractableText => isPushing ? "Stop Push" : "Push Object";

    public Transform Transform() => transform;


    public void Interact(Transform interactor)
    {
        Debug.Log($"Move");
    }

    IEnumerator ApplyForce(Transform interactor)
    {
        while (isPushing)
        {
            Vector3 direction = transform.position - interactor.position;
            rb.AddForce(direction.normalized * force, ForceMode.Impulse);

            if (rb.velocity.magnitude >= force) yield break;
            Debug.Log($"rb.velocity.magnitude: {rb.velocity.magnitude}");

            yield return new WaitForFixedUpdate();
        }
    }
}
