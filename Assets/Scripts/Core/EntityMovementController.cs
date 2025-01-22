using DL.InputModuleRuntime.Interfaces;
using DL.InterfacesRuntime;
using UnityEngine;

namespace DL.CoreRuntime
{
    public abstract class EntityMovementController : MonoBehaviour, IInitialize
    {
        private IInput _inputModule;
        
        protected Rigidbody _rigidbody;
        
        [field: SerializeField]  public virtual bool IsEnable { get; set; } = true;
        
        public virtual void Initialize(params object[] objects)
        {
            _inputModule = objects[0] as IInput;
            _rigidbody = objects[1] as Rigidbody;
            
            if (_inputModule != null)
            {
                _inputModule.OnMove += OnMove;
                _inputModule.OnMousePosition += OnMousePosition;
            }
        }

        public virtual void Deinitialize()
        {
            if (_inputModule != null)
            {
                _inputModule.OnMove -= OnMove;
                _inputModule.OnMousePosition -= OnMousePosition;
            }
        }

        protected abstract void OnMove(Vector2 moveInput);
        
        protected virtual void OnMousePosition(Ray ray)
        {
            
        }
    }
}