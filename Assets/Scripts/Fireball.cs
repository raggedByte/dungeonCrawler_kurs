using System;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float damage;
    
    private Vector3 _targetPosition;
    private Vector3 _velocity;
    
    [SerializeField] private float speed;
    
    public void SetTarget(Vector3 target)
    {
        _targetPosition = target;
        _velocity = _targetPosition - transform.position;
    }

    private void Update()
    {
        if (_targetPosition != null)
        {
            transform.position += _velocity * (speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Character>().Hit(damage);
        }
        else if (other.gameObject.CompareTag("Enemy") || other.gameObject.CompareTag("Enemies colliders"))
        {
            return;
        }
        
        Destroy(gameObject);
    }
}