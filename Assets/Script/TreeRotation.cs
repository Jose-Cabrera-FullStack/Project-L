using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRotation : MonoBehaviour
{
    public float speed = 1;

    void FixedUpdate () {
        
        float moveHorizontal = Input.GetAxis("Horizontal");

        transform.Rotate(0, moveHorizontal * speed, 0);
    }

}
