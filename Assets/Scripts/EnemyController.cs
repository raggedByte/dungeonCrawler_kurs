using System;
using System.Collections;
using System.Collections.Generic;
using Lean.Transition;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(AIDestinationSetter))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private AudioClip soundDeath;
    
    private Animator _animator;
    private Rigidbody2D _rb;
    private AIPath _aiPath;
    private AIDestinationSetter _destinationSetter;

    private Transform _currentTransform;
    public Transform CurrentTarget => _currentTransform;

    //private bool _isDeath = false;
    
    //private Vector3 _lastPosition;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _aiPath = GetComponent<AIPath>();
        _destinationSetter = GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        var position = transform.position;
        
        Vector3 velocity = _aiPath.velocity;
        float speed = Vector2.SqrMagnitude(velocity);
        
        _animator.SetFloat("Speed", speed);
        _animator.SetFloat("Horizontal", velocity.x);
        _animator.SetFloat("Vertical", velocity.y);

        if (velocity.x > 0.01)
        {
            transform.localScale = Vector3.one;
        }
        else if (velocity.x < -0.01)
        {
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
    }

    public void HitEnemy(float damageCount)
    {
        _aiPath.enabled = false;
        tag = "Untagged";
        transform.PlaySoundTransition(soundDeath, volume: 0.1f);
        _animator.SetTrigger("Death");
        StartCoroutine(DeferredDeath());
    }

    public void SetTarget(Transform targetTransform)
    {
        _currentTransform = targetTransform;
        _destinationSetter.target = targetTransform;
    }

    public void ClearTarget()
    {
        _currentTransform = null;
        _destinationSetter.target = null;
    }
    
    private void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            
        }
    }

    IEnumerator DeferredDeath()
    {
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}