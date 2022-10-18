using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    bool _clicked;
    public bool clicked{
        get
        {
            return _clicked;
        }
    }
    bool _pause;
    public bool pause{
        get
        {
            return _pause;
        }
    }
    bool _showInteractables;
    public bool ShowInteractable
    {
        get
        {
            return _showInteractables;
        }
    }

    [SerializeField] bool _inputEnabled;
    public bool inputEnabled
    {
        get {return _inputEnabled;}
        set {_inputEnabled = value;}
    }

    Vector3 _mousePosition;
    public Vector3 mousePosition
    {
        get
        {
            return _mousePosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(_inputEnabled)
        {
            _clicked = Input.GetButtonDown("Fire1");
            _pause = Input.GetKeyDown(KeyCode.P);
            _showInteractables = Input.GetKeyDown(KeyCode.E);
            _mousePosition = Input.mousePosition;
        }
        else
        {
            _clicked = false;
            _pause = false;
            _showInteractables = false;
            _mousePosition = Vector3.zero;
        }
    }
}
