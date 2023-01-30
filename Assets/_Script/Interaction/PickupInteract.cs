using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour, IInteractable
{
    [SerializeField] string _interactionPrompt;
    public string InteractionPrompt => _interactionPrompt;

    public void Interact(Transform transform)
    {
        Debug.Log($"Picking Up object!");
    }
}
