using UnityEngine;
//By adding this script you will get all these scripts...
//...and components automatically assgined to the characther.
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(p_AudioManager))]
[RequireComponent(typeof(p_checkpoint))]
[RequireComponent(typeof(p_lives))]
public class p_movement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _pointerT; //Temporary code, will be removed.
    [SerializeField] private LayerMask whatIsGround;

    [Header("States")]
    public MovementState state;

    [Header("Movement")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float crouchYScale;

    private Transform _playerT;
    private Rigidbody2D _rb;
    private PlayerControls _ctrl;
    private p_AudioManager _AudioManager;
    private p_grab _playerGrab;  
    private float currentSpeed;
    private float crouchSpeed;
    private float startYScale;
    private float currentYScale;
    private float movement;
    private bool isGrounded => Physics2D.Raycast(transform.position, Vector2.down, 2f, whatIsGround);
    private bool noInput => movement == 0;

    //Other
    public Vector2 globalTransform { get; private set; }

    private void Awake()
    {
        _ctrl = new PlayerControls();
        _playerT = FindObjectOfType<p_movement>().transform;
        _playerGrab = FindObjectOfType<p_grab>();
        _AudioManager = FindObjectOfType<p_AudioManager>();
        _rb = GetComponent<Rigidbody2D>();
        //Movement
        _ctrl.Player.Movement.performed += ctx => movement = ctx.ReadValue<float>();
        _ctrl.Player.Movement.canceled += _ => movement = 0;
        _ctrl.Player.Jump.started += _ => Jump();
        //Crouch
        startYScale = transform.localScale.y;
        currentYScale = startYScale;
        _ctrl.Player.Crouch.started += _ => Crouch();
        _ctrl.Player.Crouch.canceled += _ => StopCrouch();

    }
    private void Start()
    {
        currentSpeed = playerSpeed;
        crouchSpeed = playerSpeed / 2f;
        _rb.freezeRotation = true;
    }
    //These can be used to display different sprites in the screen.
    public enum MovementState
    {
        IDLE,
        WALKING,
        CROUCHING,
        CARRYING,
        AIR
    }
    private void Update()
    {
        stateMachine();
        playerFacing();
        _AudioManager.HandleFootsteps();
    }
    private void FixedUpdate()
    {
        handleMovement();
    }
    private void OnEnable() => _ctrl.Enable();
    private void OnDisable() => _ctrl.Disable();
    private void playerFacing()
    {
        switch (movement)
        {
            case < 0:
                _pointerT.localScale = new Vector3(-1, currentYScale, 1);
                globalTransform = -transform.right; //Don't let the name fool you, as it is negative so it is left.
                break;
            case > 0:
                _pointerT.localScale = new Vector3(1, currentYScale, 1);
                globalTransform = transform.right;
                break;
        }
    }
    private void handleMovement()
    {
        _rb.velocity = new Vector2(movement * currentSpeed, _rb.velocity.y);
    }
    public void stopMovement()
    {
        _rb.velocity = Vector2.zero;
    }
    private void Jump()
    {
        if (isGrounded)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
            _AudioManager.HandleJumpSounds();
        }
    }
    private void Crouch()
    {
        currentYScale = crouchYScale;
        _playerT.localScale = new Vector3(transform.localScale.x, currentYScale, 1);
        if (isGrounded) { _rb.AddForce(Vector2.down * 5f, ForceMode2D.Impulse); }
        else return;
    }
    private void StopCrouch()
    {
        currentYScale = startYScale;
        _playerT.localScale = new Vector3(transform.localScale.x, currentYScale, 1);
    }
    //State machine can be used to determine...
    //...animations,behaviours and etc. Such as idle animation,..
    //...automatic pause or more.
    private void stateMachine()
    {
        if (noInput && isGrounded) state = MovementState.IDLE;
        else if (_playerGrab.isActive && isGrounded) state = MovementState.CARRYING;
        else if (!noInput && currentYScale == crouchYScale) { state = MovementState.CROUCHING; currentSpeed = crouchSpeed; }
        else if (!noInput) { state = MovementState.WALKING; currentSpeed = playerSpeed; }
        else state = MovementState.AIR;
    }
    public bool getBoolData(string indentifier)
    {
        switch (indentifier)
        {
            case "noInput":
                return noInput;
            case "isGrounded":
                return isGrounded;
            default:
                Debug.LogWarning("Critical error,insert an identifier. Returning false.");
                return false;
        }
    }
}
