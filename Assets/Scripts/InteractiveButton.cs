using System;
using Lean.Transition;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class InteractiveButton : MonoBehaviour
{
    private Action _onButtonPressed;
    private Action _onButtonReleased;

    private Animator _animator;

    private bool _isPressed;

    private Transform _target;

    [SerializeField] private AudioClip clickSound;
    
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _isPressed = false;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!_isPressed)
        {
            if (other.gameObject.CompareTag($"Crate") || other.gameObject.CompareTag($"Player") ||
                other.gameObject.CompareTag($"Enemy"))
            {
                _target = other.transform;
                _animator.SetBool("isPressed", true);
                _onButtonPressed?.Invoke();
                _isPressed = true;
                transform.PlaySoundTransition(clickSound, volume: 0.1f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isPressed)
        {
            if ((other.gameObject.CompareTag($"Crate") || other.gameObject.CompareTag($"Player") ||
                 other.gameObject.CompareTag($"Enemy")) && _target.Equals(other.transform))
            {
                _target = null;
                _animator.SetBool("isPressed", false);
                _onButtonReleased?.Invoke();
                _isPressed = false;
                transform.PlaySoundTransition(clickSound, volume: 0.1f);
            }
        }
    }

    public void SubscribeOnButtonPressed(Action listener)
    {
        _onButtonPressed += listener;
    }

    public void SubscribeOnButtonReleased(Action listener)
    {
        _onButtonReleased += listener;
    }

    public void UnsubscribeOnButtonPressed(Action listener)
    {
        _onButtonPressed -= listener;
    }

    public void UnsubscribeOnButtonReleased(Action listener)
    {
        _onButtonReleased -= listener;
    }
}