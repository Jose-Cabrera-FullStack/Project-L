using UnityEngine;
[RequireComponent(typeof(PlayerInput))]
[RequireComponent(typeof(UnityEngine.AI.NavMeshAgent))]
public class PlayerController : MonoBehaviour
{
    UnityEngine.AI.NavMeshAgent _navMeshAgent;
    PlayerInput _playerInput;
    Vector3 _destinationPoint;

    public Vector3 destinationPoint
    {
        get
        {
            return _destinationPoint;
        }
        set
        {
            _destinationPoint = value;
        }
    }

    Animator _animator;
    Vector3 _velocity;
    Vector3 _localVelocity;
    float _speed;
    readonly int hashHorizontalSpeed = Animator.StringToHash("HorizontalSpeed");

    void Start() {
        if(!TryGetComponent(out _playerInput))
        {
            Debug.LogWarning($"ERROR FALTA DE PLAYER INPUT");
        }
        if(!TryGetComponent(out _navMeshAgent))
        {
            Debug.LogWarning($"ERROR FALTA DE NAVMESH AGENT");
        }
        _destinationPoint = transform.position;
    }

    void Update()
    {
        _navMeshAgent.SetDestination(_destinationPoint);
    }

    void OnAnimatorMove()
    {
        _velocity = _navMeshAgent.velocity;
        _localVelocity = transform.InverseTransformDirection(_velocity);
        _speed = _localVelocity.z;
        _animator.SetFloat(hashHorizontalSpeed, _speed);
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rigidbody = hit.collider.attachedRigidbody;
        float forceMagnitude = 10f;
        if(rigidbody != null)
        {
            Vector3 forceDirection = hit.gameObject.transform.position - transform.position;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidbody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);
        }
    }
}