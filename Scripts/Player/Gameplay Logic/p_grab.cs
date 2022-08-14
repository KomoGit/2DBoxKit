using UnityEngine;
/// <summary>
/// This script gives the player the ability of grabbing interactive..
/// ..Rigidbodies. To ensure player can interact with the objects, add..
/// ..Tag -> 'Interactives/Rigidbody' to the tag.
/// </summary>
public class p_grab : MonoBehaviour
{
    [Header("Values")]
    [SerializeField] private float rayDistance;
    [SerializeField] private float throwForce;
    [Header("References")]
    [SerializeField] private Transform objectHolder;
    [SerializeField] private Transform rayPoint;

    public bool isActive { get; private set; }
    public Rigidbody2D _heldObject;
    private Rigidbody2D _grabbedRB;
    private PlayerControls _ctrl;
    private p_movement _plr;    
    private RaycastHit2D hitInfo;

    private void Awake()
    {
        _ctrl = new PlayerControls();
        _grabbedRB = GetComponent<Rigidbody2D>();
        _plr = FindObjectOfType<p_movement>();
    }
    private void Update()
    {
        MyInput();
        moveObject();
    }
    private void OnEnable() => _ctrl.Enable();
    private void OnDisable() => _ctrl.Disable();
    private void MyInput()
    {
        if (_ctrl.Player.Interact.WasPressedThisFrame() && _grabbedRB == null)
        {
            grabObject();
        }
        else if (_ctrl.Player.Interact.WasPressedThisFrame() && _heldObject != null)
        {
            DropObject();
        }
        if (_ctrl.Player.Throw.WasPerformedThisFrame() && _grabbedRB != null)
        {
            throwObject();
        }
    }
    private void grabObject()
    {
        hitInfo = Physics2D.Raycast(rayPoint.position, _plr.globalTransform, rayDistance);
        if (hitInfo.collider == null) return;//NaN protection
        var tag = hitInfo.transform.CompareTag("Interactives/Rigidbody");
        if (tag && _grabbedRB == null)
        {
            _grabbedRB = hitInfo.collider.gameObject.GetComponent<Rigidbody2D>();
            _grabbedRB.isKinematic = true;
            _grabbedRB.freezeRotation = true;
            _heldObject = _grabbedRB;
            isActive = true;
        }
    }
    private void moveObject()
    {
        while (isActive)
        {
            _grabbedRB.transform.position = objectHolder.position;
            break;
        }
    }
    private void DropObject()
    {
        isActive = false;
        _heldObject.isKinematic = false;
        _heldObject.freezeRotation = false;
        _heldObject = null;
        _grabbedRB = null;
    }
    private void throwObject()
    {
        isActive = false;
        _heldObject.isKinematic = false;
        _heldObject.velocity = new Vector2(_plr.globalTransform.x,_plr.globalTransform.y)*throwForce;
        _heldObject = null;
        _grabbedRB = null;
    }
    public void DestroyHeldObject()
    {
        if (_heldObject != null)
        {
            Object.Destroy(_heldObject.gameObject);
        }
    }
}