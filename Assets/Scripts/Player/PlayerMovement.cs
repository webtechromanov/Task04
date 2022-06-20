using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private SpriteRenderer _sprite;
    [SerializeField] private float _jumpForce;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private int _amountOfJumps;
    [SerializeField] private float _rayDistance;

    private PlayerInput _input;
    private float _direction;
    private int _jumpsLeft;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();

        _input = new PlayerInput();
        _input.Enable();

        _input.Player.Jump.performed += ctx => TryJump();
    }

    private void Update()
    {
        _direction = _input.Player.Move.ReadValue<float>();
        IsGrounded();
        Move(_direction);
        MoveDirection(_direction);
    }

    private void Move(float direction)
    {
        float normalizedSpeed = direction * _speed * Time.deltaTime;
        transform.Translate(new Vector3(normalizedSpeed, 0, 0));
    }

    private void MoveDirection(float direction)
    {
        if (direction == 1)
            _sprite.flipX = false;
        else if (direction == -1)
            _sprite.flipX = true;
    }

    private void TryJump()
    {
        if (_jumpsLeft > 0)
            Jump();
    }

    private void Jump()
    {
        _jumpsLeft--;
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);

    }

    private void IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _rayDistance);

        if (hit.collider != null)
            _jumpsLeft = _amountOfJumps - 1;
    }
}
