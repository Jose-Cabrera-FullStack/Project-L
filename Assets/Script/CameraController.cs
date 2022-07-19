/* using System.Collections; */
/* using System.Collections.Generic; */
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerController controller;
    public RouteCreator creator;
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
        zooming();
        position();
    }

    private void position()
    {
        transform.LookAt(controller.transform.position);
        transform.position =
            controller.transform.position
            + distance
                * Vector3.Cross(-creator.route.getTanget(controller.t), Vector3.up).normalized;
    }

    private void zooming()
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
