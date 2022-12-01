using UnityEngine;
using Cinemachine;
using TMPro;


public class CameraLayout : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    Rect _initialCamState;
    [SerializeField]
    CinemachineVirtualCamera vcam;
    [Range(-1, 1), SerializeField]
    float margin = 0f;
    bool _isChanged = false;
    public TextMeshProUGUI textComponent;

    void Awake()
    {
        transform.position = vcam.transform.position;
        _initialCamState = cam.rect;
    }

    // public void switchLayoutPosition() Arreglar para que no se updatee todo el tiempo

    void Update()
    {
        // cam.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);

        // TODO: Needs to inverse behavior
        if (vcam.Priority == 10 && !_isChanged)
        {
            textComponent.SetText("Camera Selected");
            Debug.Log($"Initial rect: {cam.rect}");
            cam.rect = new Rect(margin, 0.0f, 1.0f - margin * 2.0f, 1.0f);
            Debug.Log($"New rect: {cam.rect}");
            _isChanged = true;
            // cam.enabled = false;
        }
        else
        {
            textComponent.SetText("");
            cam.rect = _initialCamState;
            _isChanged = false;

            // cam.enabled = true;
        }
    }
}
