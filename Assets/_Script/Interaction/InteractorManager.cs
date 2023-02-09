using System.Collections.Generic;
using UnityEngine;

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
        Collider[] colliders = Physics.OverlapSphere(transform.position, interactionRange);
        List<IInteractable> interactables = new List<IInteractable>();

        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out IInteractable interactable))
            {
                interactables.Add(interactable);
            }
        }

        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        foreach (IInteractable interactable in interactables)
        {
            float distance = Vector3.Distance(transform.position, interactable.Transform().position);
            if (distance < closestDistance)
            {
                closestInteractable = interactable;
                closestDistance = distance;
            }
        }

        return closestInteractable;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
}
