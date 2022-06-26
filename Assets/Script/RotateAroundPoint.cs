// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class PlayerController : MonoBehaviour
// {
//     public CharacterController controller;
//     private Vector3 direction;

//     public GameObject angle1;


//     public float speed = 8;
//     public float radius;
//     public float angle;

//     public float jumpForce = 10;
//     public float gravity = -20;

//     void Start() {
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         GameObject lighthouse = GameObject.Find("Lighthouse");
//         GameObject player = GameObject.Find("Player");
//         float hInput = Input.GetAxis("Horizontal");
//         direction.x = hInput * speed;
//         //z y y para calcular el radio del cylinder, radius -> Vector3.Module = Es la distancia que se debe calcular entre el cilindro y el player
//         //angle += speed * Time.deltaTime;
//         //transform.position = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;

//         // float distance = Vector3.Distance(lighthouse.transform.position, player.transform.position);
//         // Debug.Log("lighthouse.transform.position:",lighthouse.transform.position);
//         // Debug.Log("player.transform.position:",player.transform.position);
//         direction.y += gravity * Time.deltaTime;
//         if (Input.GetButtonDown("Jump"))
//         {
//             direction.y = jumpForce;
//         }
//         controller.Move((new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius) * Time.deltaTime);
//     }
// }
