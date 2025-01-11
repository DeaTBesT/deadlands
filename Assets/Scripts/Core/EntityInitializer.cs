using Interfaces;
using UnityEngine;

namespace Core
{
    public abstract class EntityInitializer : MonoBehaviour, IInitialize, IDeinitialize
    {
        [SerializeField] private bool _selfInitialize = true;
        [SerializeField] private bool _selfDeinitialize = true;
        
        public bool IsInitialized { get; protected set; }

        public bool IsEnable { get; set; } = true;

        private void Start()
        {
            if (!_selfInitialize)
            {
                return;
            }
            
            Initialize();
        }

        private void OnDestroy()
        {
            if (!_selfDeinitialize)
            {
                return;
            }
            
            Deinitialize();
        }

        public abstract void Initialize(params object[] objects);

        public virtual void Deinitialize(params object[] objects)
        {
          
        }

        public virtual void Quit()
        {
            
        }
    }
}