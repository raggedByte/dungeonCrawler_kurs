using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(SpriteRenderer))]
public class InteractiveCrate : MonoBehaviour, IInteractive
{
    private SpriteRenderer _sprite;
    private Transform _targetTransform;
    
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = new Color(0.7f, 0.7f, 0.7f);
    }
    
    private void Update()
    {
        if (_targetTransform != null)
        {
            if (_targetTransform.position.y > transform.position.y)
            {
                _sprite.sortingOrder = 1;
            }
            else
            {
                _sprite.sortingOrder = -1;
            }
        }
    }
    
    public void Interact(GameObject sender)
    {
        _targetTransform = sender.GetComponent<Transform>();
        _sprite.color = new Color(1f, 1f, 1f);
    }

    public void InteractEnd(GameObject sender)
    {
        _targetTransform = null;
        _sprite.color = new Color(0.7f, 0.7f, 0.7f);
    }
}