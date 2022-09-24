/* using System.Collections; */
/* using System.Collections.Generic; */
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController controller;
    public float distance;
    public float zoomChangeAmount = 80f;
    public float minumunDistance = 10f;
    public float maximumDistance = 70f;

    // Start is called before the first frame update
    void Start()
    {
        distance = 15f;
    }

    // Update is called once per frame
    void Update()
    {
        Zooming();
        Position();
    }

    private void Position()
    {
        transform.LookAt(controller.transform.position);
    }

    private void Zooming()
    {
        if (Input.GetKey("i"))
        {
            this.distance -= zoomChangeAmount * Time.deltaTime;
        }

        if (Input.GetKey("k"))
        {
            this.distance += zoomChangeAmount * Time.deltaTime;
        }

        /// <summary> The float result between the minimum and maximum values
        /// <value> Clamp(float value, float min, float max) </value>
        /// <summary>
        this.distance = Mathf.Clamp(this.distance, minumunDistance, maximumDistance);
    }
}
