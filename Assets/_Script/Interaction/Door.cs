using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] string _prompt;
    public string InteractionPrompt => _prompt;

    public bool Interact(InteractorManager interactor)
    {
        Debug.Log($"Opening Door!");
        return true;
    }
}
