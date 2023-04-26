using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem.EnhancedTouch;
using ETouch  = UnityEngine.InputSystem.EnhancedTouch; //this is because Unity has an homonymy on the Touch class(old
//and new system)
using Touch = UnityEngine.Touch;


//this class is used as a manager for the joystick logic and player movement around the level.
//I decided to use Unity's new input sistem
public class PlayerMovementController : MonoBehaviour
{
    
    [SerializeField] private Vector2 JoystickSize;//this just serves as a way to speed things up and not access the component every time
    [SerializeField] private FloatingJoystick Joystick;//our variable for the joystick
    [SerializeField] private Rigidbody2D Player;//our variable for the player

    private Finger MovementFinger = null;
    private Vector2 MovementAmount;
    private Vector2 speed = new Vector2(7, 7);

    private void Awake()
    {
        JoystickSize = new Vector2(200, 200);
        
    }

    //since every time the player lifts the finger, and since the joystick starts disabled, we have to
    //work around the onenable and ondisable
    
    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
        //upon enabling, we add three callback methods for handling the logic of the finger movement, to be subtracted on 
        //disable
        ETouch.Touch.onFingerDown += HandleFingerDown;
        ETouch.Touch.onFingerUp += HandleFingerUp;
        ETouch.Touch.onFingerMove += HandleFingerMove;
    }

    private void OnDisable()
    {
        ETouch.Touch.onFingerDown -= HandleFingerDown;
        ETouch.Touch.onFingerUp -= HandleFingerUp;
        ETouch.Touch.onFingerMove -= HandleFingerMove;
        EnhancedTouchSupport.Disable();
    }

    private void HandleFingerMove(Finger MovedFinger)
    {
        //logic for when the user moves the finger
        if (MovedFinger == MovementFinger)
        {
            Vector2 knobPosition;
            float maxMovement = JoystickSize.x / 2f; //max movement for the knob (since it's a square)
            ETouch.Touch currentTouch = MovedFinger.currentTouch;

            if (Vector2.Distance(
                    currentTouch.screenPosition,
                    Joystick.RectTransform.anchoredPosition
                ) > maxMovement) 
            {
                //if the movement is outside of the outer circle, then the position of the knob is 
                //equal to the normalization of the difference between the touch of the user and the current position
                //of the joystick times the max movement
                
                knobPosition = (
                                   currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition
                               ).normalized
                               * maxMovement;
            }
            else
            {
                //outcome if it's inside the boundary
                knobPosition = currentTouch.screenPosition - Joystick.RectTransform.anchoredPosition;
            }

            Joystick.Knob.anchoredPosition = knobPosition;
            MovementAmount = knobPosition / maxMovement;
        }
    }

    private void HandleFingerUp(Finger LostFinger)
    {
        //if the user lifts up the finger, then disable the joystick
        if (LostFinger == MovementFinger)
        {
            MovementFinger = null;
            Joystick.RectTransform.anchoredPosition = Vector2.zero;
            Joystick.gameObject.SetActive(false);
            MovementAmount = Vector2.zero;
        }
    }

    private void HandleFingerDown(Finger TouchedFinger)
    {
        if (MovementFinger == null)
        {
            //spawn the joystick active
            MovementFinger = TouchedFinger;
            MovementAmount = Vector2.zero;
            Joystick.gameObject.SetActive(true);
            Joystick.RectTransform.sizeDelta = JoystickSize;
            
            if (TouchedFinger.screenPosition.x < Screen.width && TouchedFinger.screenPosition.y < Screen.height)
            {
                //if the player presses in a region where the joystick would be partially hidden, fix the position
                //of the joystick accordingly
                Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
                
            }else if (TouchedFinger.screenPosition.x > Screen.width)
            {
                Joystick.RectTransform.anchoredPosition = ClampStartPosition(TouchedFinger.screenPosition);
            }
        }
    }

    private Vector2 ClampStartPosition(Vector2 StartPosition)
    {
        //this method simply has the logic to handle edge cases of the user's inputs
        if (StartPosition.x < JoystickSize.x/2)
        {
            StartPosition.x = JoystickSize.x/2;
        }
        if (StartPosition.y < JoystickSize.y/2)
        {
            StartPosition.y = JoystickSize.y/2;
        }
        if (StartPosition.x > Screen.width - JoystickSize.x/2)
        {
            StartPosition.x = Screen.width - JoystickSize.x/2;
        }
        if (StartPosition.y > Screen.height - JoystickSize.y/2)
        {
            StartPosition.y = Screen.height - JoystickSize.y/2;
        }

        return StartPosition;
    }

    private void Update()
    {
        Vector2 scaledMovement = speed * Time.deltaTime * new Vector2(MovementAmount.x,MovementAmount.y);
        Player.transform.Translate(scaledMovement);
    }
}
