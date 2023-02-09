using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupInteract : MonoBehaviour, IInteractable
{
    public string InteractableText => "Pick object";

    public Transform Transform()
    {
        return transform;
    }

    public void Interact(Transform transform)
    {
        Debug.Log($"Picking Up object!");
    }
}
