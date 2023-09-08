using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController _charController;
    private Vector3 _playerVelocity;
    private float _moveSpeed = 5f;
    private float gravity = -20f;
    private float _jumpVelocity = 10;
    private float moveDirection;
    private Vector3 defaulRotation = new Vector3(0, 180, 0);

    [Header("Ground Check")]
    [SerializeField] Transform _groundCheck;
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float maxDistance;

    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
        PlayerInput.Instance.OnPlayerJumpInput += Jump;
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        moveDirection = PlayerInput.Instance.GetPlayerMovement();

        if (moveDirection != 0)
        {
            HandleRotation();
            _charController.Move(transform.forward * _moveSpeed * Time.deltaTime);
        }

        _playerVelocity.y += gravity * Time.deltaTime;

        if (IsGrounded() && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -2f;
        }

        _charController.Move(_playerVelocity * Time.deltaTime);
    }

    private void Jump(object sender, System.EventArgs e)
    {
        if (IsGrounded())
        {
            _playerVelocity.y = _jumpVelocity;
        }
    }

    private void HandleRotation()
    {
        transform.rotation = moveDirection > 0 ? Quaternion.Euler(defaulRotation) : Quaternion.Euler(Vector3.zero);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, maxDistance, _groundLayer);
    }
}
