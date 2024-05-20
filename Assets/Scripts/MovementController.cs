using UnityEngine;
using UnityEngine.InputSystem;

public class MovementController : MonoBehaviour
{
    [Tooltip("Controller or Keyboard control toggle")]
    public bool isKeyboardControlled;

    [Header("Movement")] 
    public float moveSpeed;

    [HideInInspector]
    public float moveSpeedOffset;
    [HideInInspector]
    public bool movementEnabled = true;

    private Rigidbody _rb;
    private PlayerInput _playerInput;
    private InputAction _moveAction;
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _playerInput = GetComponent<PlayerInput>();
        _moveAction = _playerInput.actions.FindAction("Directional");

        moveSpeedOffset = 0;

        if (isKeyboardControlled)
        {
            _playerInput.defaultControlScheme = "Keyboard";
        }
        else
        {
            _playerInput.defaultControlScheme = "Controller";
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovePlayer();
    }

    void MovePlayer()
    {
        if (movementEnabled)
        {
            var direction = _moveAction.ReadValue<Vector2>();
            var moveDirection = new Vector3(direction.x, transform.position.y, direction.y);
        
            // Translate the character in the move direction with the magnitude of designer-controlled move speed + move speed offset (from other scripts)
            // Move speed / 3 was calculated from:
            // ( default speed we wanted to start with (10) divided by factor that gets us to the Unity Engine force we want (3) )
            _rb.AddForce(transform.InverseTransformDirection(moveDirection) * (moveSpeed / 3 + moveSpeedOffset), ForceMode.VelocityChange);
        }
    }
}
