using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public event Action OnPlayerJump;

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
    [SerializeField] private float _airMultiplier;
    [SerializeField] private float _airDrag;
    
    [Header("Sliding Settings")]
    [SerializeField] private KeyCode _slideKey;
    [SerializeField] private float _slideMultiplier;
    [SerializeField] private float _slideDrag;

    [Header("Ground Check Sttigns")]
    [SerializeField] private float _playerHeight;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _groundDrag;

    private float _startingMovementSpeed, _startingJumpForce;
    private StateController _stateController;
    private Rigidbody _playerRigidbody;
    private float _horizomtalInput, _verticalInput;
    private Vector3 _movementDirection;
    private bool _isSliding;

    private void Awake()
    {
        _stateController = GetComponent<StateController>();
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;

        _startingMovementSpeed = _movementSpeed;
        _startingJumpForce = _jumpforce;
    }
    private void Update()
    {
        SetInputs();
        SetStates();
        SetPlayerDrag();
        LimitPlayerSpeed();
    }
    private void FixedUpdate()
    {
        SetPlayerMovement();
    }

    private void SetInputs()
    {
        _horizomtalInput = Input.GetAxisRaw("Horizontal");
        _verticalInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(_slideKey))
        {
            _isSliding = true;
        }
        else if (Input.GetKeyDown(_movementKey))
        {
            _isSliding = false;
        }
        else if (Input.GetKey(_jumpKey) && _canJump && IsGrounded())
        {
            _canJump = false;
            SetPlayerJumping();
            Invoke(nameof(ResetJump), _jumpCooldown);
        }
    }

    private void SetStates()
    {
        var movementDirection = GetMovementDirection();
        var isGrounded = IsGrounded();
        var isSliding = IsSliding();
        var currentState = _stateController.GetCurrentState();

        var newState = currentState switch
        {
            _ when movementDirection == Vector3.zero && isGrounded && !isSliding => PlayerState.Idle,
            _ when movementDirection != Vector3.zero && isGrounded && !isSliding => PlayerState.Move,
            _ when movementDirection != Vector3.zero && isGrounded && isSliding => PlayerState.Slide,
            _ when movementDirection == Vector3.zero && isGrounded && isSliding => PlayerState.SlideIdle,
            _ when !_canJump && !isGrounded => PlayerState.Jump,
            _ => currentState
        };

        if (newState != currentState)
        {
            _stateController.ChangeState(newState);
        }
    }

    private void SetPlayerMovement()
    {
        _movementDirection = _oriantationTransform.forward * _verticalInput + _oriantationTransform.right * _horizomtalInput;

        float forceMultiplier = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => 1f,
            PlayerState.Slide => _slideMultiplier,
            PlayerState.Jump => _airMultiplier,
            _ => 1f
        };       
         _playerRigidbody.AddForce(_movementDirection.normalized * _movementSpeed * forceMultiplier, ForceMode.Force);
    }
    private void SetPlayerDrag()
    {

        _playerRigidbody.linearDamping = _stateController.GetCurrentState() switch
        {
            PlayerState.Move => _groundDrag,
            PlayerState.Slide => _slideDrag,
            PlayerState.Jump => _airDrag,
            _ => _playerRigidbody.linearDamping
        };
    }
    private void LimitPlayerSpeed()
    {
        Vector3 flatVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        if (flatVelocity.magnitude > _movementSpeed)
        {

            Vector3 limitedVelocity = flatVelocity.normalized * _movementSpeed;
            _playerRigidbody.linearVelocity =
            new Vector3(limitedVelocity.x, _playerRigidbody.linearVelocity.y, limitedVelocity.z);
        }
    }

    private void SetPlayerJumping()
    {
        OnPlayerJump?.Invoke();
        _playerRigidbody.linearVelocity = new Vector3(_playerRigidbody.linearVelocity.x, 0f, _playerRigidbody.linearVelocity.z);
        _playerRigidbody.AddForce(transform.up * _jumpforce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        _canJump = true;
    }

    #region Helper Functions
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, _playerHeight * 0.5f + 0.2f, _groundLayer);
    }

    private Vector3 GetMovementDirection()
    {
        return _movementDirection.normalized;
    }

    private bool IsSliding()
    {
        return _isSliding;
    }

    public void SetMovementSpeed(float speed, float duration){
        _movementSpeed += speed;
        Invoke(nameof(ResetMovementSpeed),duration);
    }

    private void ResetMovementSpeed()
    {
        _movementSpeed = _startingMovementSpeed;
    }

    public void SetJumpForce(float jumpForce, float duration){
        _jumpforce += jumpForce;
        Invoke(nameof(ResetJumpForce),duration);
    }

    private void ResetJumpForce(){
        _jumpforce = _startingJumpForce;
    }
    #endregion
}
