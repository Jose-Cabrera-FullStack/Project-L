// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class CameraController : MonoBehaviour
// {
//     [SerializeField] Vector3 _offset;
//     public float speed = 0.1f;
//     GameObject _player;
//     // Start is called before the first frame update
//     void Start()
//     {
//         if(!_player)
//         {
//             _player = FindObjectOfType<PlayerInput>().gameObject;
//         }
//     }

//     // Update is called once per frame
//     void Update()
//     {
//         Vector3 position = _player.transform.position + _offset;
//         Vector3 desiredPosition = Vector3.Lerp(transform.position, position, speed);
//         transform.position = desiredPosition;
//     }
// }
