using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SwordController : MonoBehaviour
{
    private bool _isAttackCooldown;

    private Animator _animator;

    [SerializeField] private float strength;
    
    [SerializeField] private float attackTime;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (!_isAttackCooldown)
        {
            StartCoroutine(StartCooldown());
            _animator.SetTrigger("Attack");
        }
    }

    public void Attack(float verticalVelocity)
    {
        if (!_isAttackCooldown)
        {
            _animator.SetFloat("Vertical", verticalVelocity);
            Attack();
        }
    }
    
    private IEnumerator StartCooldown()
    {
        if (!_isAttackCooldown)
        {
            _isAttackCooldown = true;
            yield return new WaitForSeconds(attackTime);
            _isAttackCooldown = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyController>().HitEnemy(strength);
        }
    }
}