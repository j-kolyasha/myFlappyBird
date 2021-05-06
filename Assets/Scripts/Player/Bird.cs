using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Bird : MonoBehaviour
{
    [SerializeField, Range(0, 10f)] private float _jumpForce; 
    [SerializeField, Range(0, 10f)] private float _gravityModifire;
    [SerializeField] private AudioSource _jumpSound;
    [SerializeField] private AudioSource _hitSound;
    [SerializeField] private AudioSource _wingSound;

    private Rigidbody2D _rigidbody;
    private Animator _animator;

    private void FixedUpdate()
    {
        if (GameController.Instance.GameCondition != GameController.GameState.Game)
        {
            _rigidbody.velocity = Vector2.zero;
            return;
        }

        Vector2 velocity = _rigidbody.velocity;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            velocity = new Vector2(0, _jumpForce);
            _jumpSound.Play();
        }
        else
            velocity.y -= Time.deltaTime * Mathf.Abs(Physics2D.gravity.y) * _gravityModifire;

        _animator.SetFloat("Speed", velocity.y);
        _rigidbody.velocity = velocity;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Pipe pipe))
        {
            GameController.Instance.Loss();
            _hitSound.Play();
        }
        if (collision.gameObject.TryGetComponent(out Coin coin))
        {
            GameController.Instance.SelectCoin();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Chank chank))
        {
            GameController.Instance.ChangeScore();
            _wingSound.Play();
        }
    }
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }
}
