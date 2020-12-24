using System;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;

public class CheckerCollisionWithEntity : MonoBehaviour
{
    [SerializeField] private string targetTag;
    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponentInParent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            _enemyController.SetTarget(other.transform);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag(targetTag))
        {
            _enemyController.ClearTarget();
        }
    }
}