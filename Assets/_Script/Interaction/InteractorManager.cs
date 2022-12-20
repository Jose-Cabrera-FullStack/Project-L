using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorManager : MonoBehaviour
{
    [SerializeField] Transform interationPoint;
    [SerializeField] float interationPointRadius = 0.5f;
    [SerializeField] LayerMask interatableMask;

    readonly Collider[] colliders = new Collider[3];
    [SerializeField] int _numFound;

    void Update()
    {
        _numFound = Physics.OverlapSphereNonAlloc(interationPoint.position, interationPointRadius, colliders, interatableMask);
        if (_numFound > 0)
        {
            var interactable = colliders[0].GetComponent<IInteractable>();

            if (interactable != null && Keyboard.current.rKey.wasPressedThisFrame)
            {
                interactable.Interact(this);
            }
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interationPoint.position, interationPointRadius);
    }
}
