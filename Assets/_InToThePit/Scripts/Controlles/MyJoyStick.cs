using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch = UnityEngine.InputSystem.EnhancedTouch;

public class MyJoyStick : MonoBehaviour
{
    [SerializeField] private Vector2 joystickSize;
    [SerializeField] private FloatingJoyStick joystick;
    [SerializeField] private bool canMovePlayer;

    private Finger _movementFinger;
    private Vector2 _movementAmount;

    public Vector2 GetMovementAmount()
    {
        return _movementAmount;
    }

    public void SetCanMovePlayer(bool b)
    {
        canMovePlayer = b;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable(); // starting with Unity 2022 this does not work! You need to attach a TouchSimulation.cs script to your player
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleLoseFinger;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleLoseFinger;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable(); // You need to attach a TouchSimulation.cs script to your player
    }

    private void HandleFingerMove(Finger movedFinger)
    {
        if(!canMovePlayer)
            return;
        
        if (movedFinger == _movementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = joystickSize.x / 2f;
            ETouch.Touch currentTouch = movedFinger.currentTouch;

            if (Vector2.Distance( currentTouch.screenPosition  - (joystickSize / 2f), joystick.RectTransform.anchoredPosition) > maxMovement)
            {
                knobPosition = ( (currentTouch.screenPosition - (joystickSize / 2f)) - joystick.RectTransform.anchoredPosition).normalized * maxMovement;
            }
            else
            {
                knobPosition = (currentTouch.screenPosition - (joystickSize / 2f)) - joystick.RectTransform.anchoredPosition;
            }

            joystick.Knob.anchoredPosition = knobPosition;
            _movementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleLoseFinger(Finger lostFinger)
    {
        if(!canMovePlayer)
            return;
        
        if (lostFinger == _movementFinger)
        {
            _movementFinger = null;
            joystick.Knob.anchoredPosition = Vector2.zero;
            joystick.gameObject.SetActive(false);
            _movementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger touchedFinger)
    {
        if(!canMovePlayer)
            return;
        
        if (_movementFinger == null && (touchedFinger.screenPosition.y <= Screen.height / 2.0f))
        {
            _movementFinger = touchedFinger;
            _movementAmount = Vector2.zero;
            joystick.gameObject.SetActive(true);
            joystick.RectTransform.sizeDelta = joystickSize;
            joystick.RectTransform.anchoredPosition = ClampStartPosition(touchedFinger.screenPosition - (joystickSize / 2));
        }
    }

    private Vector2 ClampStartPosition(Vector2 startPosition)
    {
        if (startPosition.x < joystickSize.x / 2)
        {
            startPosition.x = joystickSize.x / 2;
            //Debug.Log("Clamped X to: " + startPosition.x);
        }
        else if (startPosition.x > Screen.width - joystickSize.x / 2)
        {
            startPosition.x = Screen.width - joystickSize.x / 2;
            //Debug.Log("Clamped X to: " + startPosition.x);
        }

        if (startPosition.y < joystickSize.y / 2)
        {
            startPosition.y = joystickSize.y / 2;
            //Debug.Log("Clamped Y to: " + startPosition.y);
        }
        else if (startPosition.y > Screen.height - joystickSize.y / 2)
        {
            startPosition.y = Screen.height - joystickSize.y / 2;
            //Debug.Log("Clamped Y to: " + startPosition.y);
        }

        return startPosition;
    }
}
