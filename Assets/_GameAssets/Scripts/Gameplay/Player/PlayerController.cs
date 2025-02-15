using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _oriantationTransform;

    [Header("Movement Settings")]
    [SerializeField] private KeyCode _movementKey;
    [SerializeField] private float _movementSpeed;
    [Header("Jump Settings")]
    [SerializeField] private float _jumpforce;
    [SerializeField] private KeyCode _jumpKey;
    [SerializeField] private float _jumpCooldown;
    [SerializeField] private bool _canJump;
    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Ground Check Sttigns")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;
    private Rigidbody _playerRigidbody;
    private float _horizomtalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
    }
    private void Update()
    {
        SetInputs();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }
    private void FixedUpdate() {
        SetPlayerMovement();
    }

    private void SetInputs(){
        _horizomtalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKeyDown(_slideKey)){
            _isSliding = true;
        }
        else if(Input.GetKeyDown(_movementKey)){
            _isSliding = false;
        }
        else if(Input.GetKey(_jumpKey) && _canJump && IsGrounded()){
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJump),_jumpCooldown);
        }
    }

    private void SetPlayerMovement(){
        _movementDirection = _oriantationTransform.forward * _verticalInput 
        + _oriantationTransform.right *_horizomtalInput;

        if(_isSliding)
        {
             _playerRigidbody.AddForce(_movementDirection.normalized *_movementSpeed *_slideMultiplier ,ForceMode.Force);
        }
        else {
             _playerRigidbody.AddForce(_movementDirection.normalized *_movementSpeed ,ForceMode.Force);
        }

    }
    private void SetPlayerDrag()
    {
        if(_isSliding)
        {
            _playerRigidbody.linearDamping = _slideDrag;
        }
        else{
            _playerRigidbody.linearDamping = _groundDrag;
        }
    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x , 0f,_playerRigidbody.linearVelocity.z);
        if(flatVelocity.magnitude > _movementSpeed){

            Vector3 limitedVelocity = flatVelocity.normalized *_movementSpeed;
            _playerRigidbody.linearVelocity = 
            new Vector3(limitedVelocity.x,_playerRigidbody.linearVelocity.y,limitedVelocity.z);
        }
    }

    private void SetPlayerJumping(){
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x,0f,_playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up *_jumpforce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _canJump = true;
    }

    private bool IsGrounded(){
        return Physics.Raycast(transform.position,Vector3.down,_playerHeight *0.5f +0.2f,_groundLayer);
    }
}
