
using UnityEngine;

namespace ValhalaProject
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "Scriptable Objects/InputManager", order = 1)]
    public class InputManagerSO : ScriptableObject
    {
        private PlayerControls playerControls;

        void OnEnable()
        {
            playerControls = new PlayerControls();
            playerControls.Enable();
        }

        void OnDisable()
        {
            DisablePlayerControls();
        }

        public void DisablePlayerControls()
        {
            playerControls.Disable();
        }

        public Vector2 GetPlayerMovment()
        {
            return playerControls.Player.Movement.ReadValue<Vector2>();
        }
        public Vector2 GetMouseDelta()
        {
            return playerControls.Player.Look.ReadValue<Vector2>();
        }
        public Vector2 GetMousePosition()
        {
            return playerControls.Player.MousePosition.ReadValue<Vector2>();
        }
        public bool PlayerJumped()
        {
            return playerControls.Player.Jump.triggered;
        }
        public bool DrawGesture()
        {
            return playerControls.Player.DrawGesture.IsPressed();
        }
        public bool EnableDisableRecordingMode()
        {
            return playerControls.Player.EnableRecording.triggered;
        }
        public void EnableLookInputAction(bool value)
        {
            if (!value) { playerControls.Player.Look.Disable(); }
            else { playerControls.Player.Look.Enable(); }
        }
        public void EnableMovmentInputAction(bool value)
        {
            if (!value) { playerControls.Player.Movement.Disable(); }
            else { playerControls.Player.Movement.Enable(); }
        }
        public bool Aim()
        {
            return playerControls.Player.Aim.IsPressed();
        }
        public void EnablDraw(bool value)
        {
            if (!value) { playerControls.Player.DrawGesture.Disable(); }
            else { playerControls.Player.DrawGesture.Enable(); }
        }
        public bool Throw()
        {
            return playerControls.Player.Throw.triggered;
        }
        public bool Activate()
        {
            return playerControls.Player.Activate.triggered;
        }
        public bool Crouch()
        {
            return playerControls.Player.Crouch.triggered;
        }
    }

}