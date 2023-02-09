using UnityEngine;
using TMPro;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] GameObject containerGameObject;
    [SerializeField] InteractorManager playerInteract;
    [SerializeField] TextMeshProUGUI interactTextMeshProGUI;

    void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show(playerInteract.GetInteractableObject());
        }
        else
        {
            Hide();
        }
    }

    void Show(IInteractable interactable)
    {
        containerGameObject.SetActive(true);
        interactTextMeshProGUI.text = interactable.InteractableText;
    }

    void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
