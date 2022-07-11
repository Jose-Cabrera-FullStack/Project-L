/* using System.Collections; */
/* using System.Collections.Generic; */
using UnityEngine;

public class Camera : MonoBehaviour
{
    public PlayerController controller;
    public RouteCreator creator;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(controller.transform.position);
        transform.position = controller.transform.position + distance * Vector3.Cross(-creator.route.getTanget(controller.t), Vector3.up).normalized;
    }
}
