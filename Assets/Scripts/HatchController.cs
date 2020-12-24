using System;
using Lean.Transition;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
public class HatchController : MonoBehaviour
{
    [InspectorName("Button 1")] [SerializeField]
    private InteractiveButton firstButton;

    [InspectorName("Button 2")] [SerializeField]
    private InteractiveButton secondButton;

    private int _countPressedButton;
    private bool _isOpen = false;

    public bool IsOpen => _isOpen;

    private SpriteRenderer _sprite;

    [SerializeField] private Sprite openDoor;
    [SerializeField] private Sprite closeDoor;

    [SerializeField] private LevelController _levelController;

    [SerializeField] private AudioClip openDoorSound;
    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.sprite = closeDoor;
    }

    private void Update()
    {
        if (_countPressedButton >= 2 && !_isOpen)
        {
            OpenHatch();
        }
        else if (_countPressedButton < 2 && _isOpen)
        {
            CloseHatch();
        }
    }

    public void CloseHatch()
    {
        _isOpen = false;
        _sprite.sprite = closeDoor;
        transform.PlaySoundTransition(openDoorSound, volume: 0.1f);
    }

    public void OpenHatch()
    {
        _isOpen = true;
        _sprite.sprite = openDoor;
        transform.PlaySoundTransition(openDoorSound, volume: 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isOpen)
        {
            _levelController.NextLevel();
        }
    }

    private void OnEnable()
    {
        firstButton.SubscribeOnButtonPressed(OnButtonPressed);
        secondButton.SubscribeOnButtonPressed(OnButtonPressed);
        
        firstButton.SubscribeOnButtonReleased(OnButtonReleased);
        secondButton.SubscribeOnButtonReleased(OnButtonReleased);
    }

    private void OnDisable()
    {
        firstButton.UnsubscribeOnButtonPressed(OnButtonPressed);
        secondButton.UnsubscribeOnButtonPressed(OnButtonPressed);
        
        firstButton.UnsubscribeOnButtonReleased(OnButtonReleased);
        secondButton.UnsubscribeOnButtonReleased(OnButtonReleased);
    }

    private void OnButtonPressed()
    {
        _countPressedButton++;
    }

    private void OnButtonReleased()
    {
        _countPressedButton--;
    }
}