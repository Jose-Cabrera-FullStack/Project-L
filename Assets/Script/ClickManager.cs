using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickManager : MonoBehaviour
{
    PlayerInput _playerInput;
    PlayerController _playerController;
    Vector3 _destinationPoint;
    Camera _camera;
    // Start is called before the first frame update
    void Start()
    {
        if(!TryGetComponent(out _playerController))
        {
            Debug.LogWarning($"ERROR NO HAY PLAYER CONTROLLER");
        }

        if(!TryGetComponent(out _playerInput))
        {
            Debug.LogWarning($"ERROR NO HAY PLAYER INPUT");
        }

        _camera = _camera ?? Camera.main ?? FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerInput.clicked)
        {
            var ray = _camera.ScreenPointToRay(_playerInput.mousePosition);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit))
            {
                _playerController.destinationPoint = hit.point;
            }
        }
    }
}
