using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    [SerializeField] string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;

    public void Interact(Transform transform)
    {
        Debug.Log($"Opening Door!");
    }
}
