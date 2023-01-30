using UnityEngine;
using UnityEngine.InputSystem;

public class InteractorManager : MonoBehaviour
{
    [SerializeField] float interactionRange = 2f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IInteractable interactable = GetInteractableObject();
            if (interactable != null)
            {
                interactable.Interact(transform);
            }
        }
    }

    public IInteractable GetInteractableObject()
    {
        Collider[] collidersArray = Physics.OverlapSphere(transform.position, interactionRange);
        foreach (Collider collider in collidersArray)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                return interactable;
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
