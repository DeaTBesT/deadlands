using System;
using Interfaces;
using UnityEngine;

namespace PlayerInputModule
{
    public class PlayerInputMobile : IInput
    {
        private Joystick _joystick;
        
        private bool _onToggleEsc = false;

        public Action<Vector2> OnMove { get; set; }
        public Action<Ray> OnMousePosition { get; set; }
        public Action OnAttackOnce { get; set; }
        public Action OnAttack { get; set; }
        public Action OnInteract { get; set; }
        public Action OnInteractUp { get; set; }
        public Action<bool> OnEscapeToggle { get; set; }

        public PlayerInputMobile(Joystick joystick)
        {
            _joystick = joystick;

            if (_joystick == null)
            {
#if UNITY_EDITOR
                Debug.LogError("Joystick is null");
#endif
            }
        }

        public void MoveHandler()
        {
            var horizontalInput = _joystick.Horizontal;
            var verticalInput = _joystick.Vertical;
            var moveInput = new Vector2(horizontalInput, verticalInput);
            OnMove?.Invoke(moveInput);
        }

        public void MouseHandler()
        {
            
        }

        public void AttackOnceHandler()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                OnAttackOnce?.Invoke();
            }
        }
        
        public void AttackHandler()
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                OnAttack?.Invoke();
            }
        }

        public void InteractHandler()
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                OnInteract?.Invoke();
            }
        }
        
        public void InteractUpHandler()
        {
            if (Input.GetKeyUp(KeyCode.E))
            {
                OnInteractUp?.Invoke();
            }
        }
        
        public void EscapeHandler()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _onToggleEsc = !_onToggleEsc;
                OnEscapeToggle?.Invoke(_onToggleEsc);
            }
        }
    }
}