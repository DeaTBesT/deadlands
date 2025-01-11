using Interfaces;
using UnityEngine;

namespace InputModule
{
    public sealed class InputHandler : MonoBehaviour, IInitialize
    {
        private IInput _input;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects) =>
            _input = objects[0] as IInput;

        private void UpdateHandler()
        {
            _input.InteractHandler();
            _input.InteractUpHandler();
            _input.EscapeHandler();

            if (IsEnable)
            {
                _input.MouseHandler();
                _input.AttackOnceHandler();
                _input.AttackHandler();
            }
        }

        private void FixedUpdateHandler()
        {
            if (!IsEnable)
            {
                return;
            }

            _input.MoveHandler();
        }

        private void Update()
        {
            if (_input == null)
            {
                return;
            }

            UpdateHandler();
        }

        private void FixedUpdate()
        {
            if (_input == null)
            {
                return;
            }

            FixedUpdateHandler();
        }

        public void SetEnableLocal(bool isEnable) =>
            IsEnable = isEnable;
    }
}