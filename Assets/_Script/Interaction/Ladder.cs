using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour, IInteractable
{
    public string InteractableText => "Climb Ladder";

    public void Interact(Transform interactor)
    {
        Transform player = GameObject.Find("Player").transform;

        // Calcular la posición final del interactor en función de la posición de la escalera
        Vector3 finalPosition = transform.position + transform.up;

        // Mover gradualmente al interactor hacia la posición final
        float time = 0.5f; // Ajusta este valor para controlar la velocidad de subida
        float elapsedTime = 0f;
        while (elapsedTime < time)
        {
            player.position = Vector3.Lerp(interactor.position, finalPosition, elapsedTime / time);
            elapsedTime += Time.deltaTime;
        }

        // Ajustar la posición final en caso de que se haya superado el tiempo límite
        player.position = finalPosition;
    }

    public Transform Transform()
    {
        return transform;
    }
}
