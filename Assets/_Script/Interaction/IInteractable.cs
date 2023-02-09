using UnityEngine;

public interface IInteractable
{
    public string InteractableText { get; }
    public void Interact(Transform interactor);

    Transform Transform();
}
