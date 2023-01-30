using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] GameObject containerGameObject;
    [SerializeField] InteractorManager playerInteract;

    void Update()
    {
        if (playerInteract.GetInteractableObject() != null)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    void Show()
    {
        containerGameObject.SetActive(true);
    }

    void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
