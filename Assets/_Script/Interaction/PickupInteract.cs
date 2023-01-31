using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour, IInteractable
{
    public string GetInteractableText => "Pick object";

    public Transform GetTransform()
    {
        return transform;
    }

    public void Interact(Transform transform)
    {
        Debug.Log($"Picking Up object!");
    }
}
