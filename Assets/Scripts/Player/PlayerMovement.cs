using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private int _amountOfJumps;
    [SerializeField] private float _radius;
    [SerializeField] private ContactFilter2D _filter;

    private PlayerInput _input;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private SpriteRenderer _sprite;
    private readonly Collider2D[] _results = new Collider2D[1];
    private float _direction;
    private int _jumpsLeft;

    private void Awake()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();

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
        _animator.SetInteger("Run", (int)direction);
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
        _rigidbody.AddForce(new Vector2(0, _jumpForce), ForceMode2D.Impulse);
        _animator.SetTrigger("Jump");
    }

    private void IsGrounded()
    {
        var hit = Physics2D.OverlapCircle(transform.position, _radius, _filter, _results);

        if (hit != 0)
            _jumpsLeft = _amountOfJumps - 1;
    }
}