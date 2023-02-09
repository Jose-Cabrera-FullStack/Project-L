using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public string InteractableText => "Open Door";

    public Transform Transform()
    {
        return transform;
    }

    public void Interact(Transform transform)
    {
        Debug.Log($"Opening Door!");
    }
}
