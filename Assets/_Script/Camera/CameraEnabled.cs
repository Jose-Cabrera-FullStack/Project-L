using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class CameraEnabled : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    [SerializeField]
    CinemachineVirtualCamera vcam;

    // Start is called before the first frame update
    void Start()
    {
        cam = GetComponentInChildren<Camera>();
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vcam.Priority == 10) cam.enabled = true;
        else cam.enabled = false;
    }
}
