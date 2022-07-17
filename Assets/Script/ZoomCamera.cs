using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{

    public HandlerCamera mainCamera;
    public float zoomChangeAmount = 80f;
    public float MinumunDistance = 10f;
    public float MaximumDistance = 70f;

    private float initialDistance;
    // Start is called before the first frame update
    void Start()
    {
        initialDistance = mainCamera.distance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("i"))
        {
            mainCamera.distance -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.GetKey("k"))
        {
            mainCamera.distance += zoomChangeAmount * Time.deltaTime;
        }

        mainCamera.distance = Mathf.Clamp(mainCamera.distance, MinumunDistance, MaximumDistance);
    }
}
