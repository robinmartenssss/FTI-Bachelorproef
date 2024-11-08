using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Users;

public class GamePadScript : MonoBehaviour
{
    [SerializeField]
   private PlayerInput playerInput;

    [SerializeField]
   private RectTransform cursorTransform;
     [SerializeField]
     private RectTransform canvasRectTransform;

     [SerializeField]
    private Canvas canvas;
   [SerializeField]
   private float cursorSpeed = 20f;

   private bool previousMouseState;


   private Mouse virtualMouse;
   private Camera mainCamera;

   private void OnEnable(){
    mainCamera = Camera.main;

    if(virtualMouse == null) {
        virtualMouse = (Mouse) InputSystem.AddDevice("VirtualMouse");
    }
    else if (!virtualMouse.added) {
        InputSystem.AddDevice(virtualMouse);
    }

    InputUser.PerformPairingWithDevice(virtualMouse, playerInput.user);

    if (cursorTransform != null) {
        Vector2 position = cursorTransform.anchoredPosition;
        InputState.Change(virtualMouse.position, position);
        }

    InputSystem.onAfterUpdate += UpdateMotion;
   }


private void OnDisable() {
    InputSystem.RemoveDevice(virtualMouse);
    InputSystem.onAfterUpdate -= UpdateMotion;
}
   private void UpdateMotion(){
        if (virtualMouse == null || Gamepad.current == null) {
            return;
        }

        Vector2 deltaValue = Gamepad.current.leftStick.ReadValue();
        deltaValue *= cursorSpeed * Time.deltaTime;

        Vector2 currentPosition = virtualMouse.position.ReadValue();
        Vector2 newPosition = currentPosition + deltaValue;

        newPosition.x = Mathf.Clamp(newPosition.x, 0, Screen.width);
        newPosition.y = Mathf.Clamp(newPosition.y, 0, Screen.height);

        InputState.Change(virtualMouse.position, newPosition);
        InputState.Change(virtualMouse.delta, deltaValue);

        bool aButtonIsPressed = Gamepad.current.aButton.isPressed;
        if(previousMouseState != aButtonIsPressed) {
            virtualMouse.CopyState<MouseState>(out var mouseState);
            mouseState.WithButton(MouseButton.Left, aButtonIsPressed);
            InputState.Change(virtualMouse, mouseState);
            previousMouseState = aButtonIsPressed;
        }

        AnchorCursos(newPosition);

   }

   private void AnchorCursos(Vector2 position) {
        Vector2 anchoredPosition;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, position, canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : mainCamera, out anchoredPosition);
        cursorTransform.anchoredPosition = anchoredPosition;
   }
}
