using Interfaces;
using UnityEngine;

namespace UI.PlacePanels.Core
{
    public abstract class SimplePlacePanelUI : MonoBehaviour, IInitialize, IPlacePanel
    {
        [SerializeField] protected GameObject _renderer;
        
        public virtual void Show() =>
            _renderer.SetActive(true);

        public virtual void Hide() => 
            _renderer.SetActive(false);

        public bool IsEnable { get; set; }
        
        public abstract void Initialize(params object[] objects);
    }
}