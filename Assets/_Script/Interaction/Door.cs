using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string GetInteractableText => "Open Door";

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform transform)
    {
        Debug.Log($"Opening Door!");
    }
}
