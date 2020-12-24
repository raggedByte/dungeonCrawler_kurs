using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemySimpleAttack : MonoBehaviour
{
    private EnemyController _enemyController;
    private bool _isCooldown;

    [SerializeField] private float hitCooldown;
    [SerializeField] private float damage;


    private void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
        _isCooldown = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isCooldown)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                StartCoroutine(StartHitCooldown());
                Attack(other.GetComponent<Character>());
            }
        }
    }

    protected virtual void Attack(Character target)
    {
        target.Hit(damage);
    }
    
    IEnumerator StartHitCooldown()
    {
        _isCooldown = true;
        yield return new WaitForSeconds(1f);
        _isCooldown = false;
    }
}