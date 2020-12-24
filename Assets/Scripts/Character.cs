using System;
using System.Numerics;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviour
{
    private Collider2D _collider;
    private Animator _animator;
    private Rigidbody2D _rb;
    private SwordController _sword;
    private Rigidbody2D _interactiveObject;
    
    private Vector2 _movement;
    private bool _isGrabInteractive;
    
    [Range(0f, 300f)]
    [SerializeField]
    [InspectorName("Speed")] private float _speed;
    
    [SerializeField]
    [InspectorName("Health")] private float _hp;

    [SerializeField] [InspectorName("Count lives")]
    private int lives;
    
    [SerializeField]
    [InspectorName("Respawn point")] private Transform _respawnPoint;
    
    private void Start()
    {
        _collider = GetComponent<Collider2D>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _rb.MovePosition(_respawnPoint.position);
        _isGrabInteractive = false;
        _sword = GetComponentInChildren<SwordController>();
    }

    private void Update()
    {
        _movement.x = Input.GetAxis("Horizontal");
        _movement.y = Input.GetAxis("Vertical");

        if (_movement.x > 0.01)
        {
            transform.localScale = Vector3.one;
        } 
        else if (_movement.x < -0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }

        if (Input.GetAxis("Fire1") > 0.01)
        {
            _isGrabInteractive = _interactiveObject != null;
        }
        else
        {
            _isGrabInteractive = false;
        }

        if (Input.GetAxis("Fire2") > 0.01)
        {
            _sword.Attack(_movement.y);
        }

        if (Input.GetKey(KeyCode.Space))
        {
            Hit(1f);
        }
        
        _animator.SetFloat("Horizontal", _movement.x);
        _animator.SetFloat("Vertical", _movement.y);
        _animator.SetFloat("Speed", Vector2.SqrMagnitude(_movement));
    }

    private void FixedUpdate()
    {
        Vector3 velocity = _movement * (_speed * Time.fixedDeltaTime);
        
        _rb.velocity = velocity;
        
        if (_isGrabInteractive && _interactiveObject != null)
        {
            _interactiveObject.velocity = velocity;
        }
    }

    public void Hit(float damage)
    {
        
        _hp -= damage;
        Debug.Log("Got damage = " + damage + ". HP = " + _hp);
        _animator.SetTrigger("Damage");
        
        if (_hp <= 0f)
        {
            OnDeath();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag($"Interactive"))
        {
            _interactiveObject = other.GetComponentInParent<Rigidbody2D>();
            _interactiveObject.GetComponentInParent<InteractiveCrate>().Interact(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_interactiveObject != null && _interactiveObject.Equals(other.GetComponentInParent<Rigidbody2D>()))
        {
            _interactiveObject.GetComponentInParent<InteractiveCrate>().InteractEnd(gameObject);
            _interactiveObject = null;
        }
    }

    private void OnDeath()
    {
        Debug.Log("death");
    }
}