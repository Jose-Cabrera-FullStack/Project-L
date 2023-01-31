using UnityEngine;

public interface IInteractable
{
    public string GetInteractableText { get; }
    public void Interact(Transform interactor);

    Transform GetTransform();
}
