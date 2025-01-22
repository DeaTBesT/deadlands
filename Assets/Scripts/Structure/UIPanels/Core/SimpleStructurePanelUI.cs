using DL.InterfacesRuntime;
using DL.StructureRuntime.UIPanels.Interfaces;
using UnityEngine;

namespace DL.StructureRuntime.UIPanels.Core
{
    public abstract class SimpleStructurePanelUI : MonoBehaviour, IStructurePanel
    {
        [SerializeField] protected GameObject _renderer;
        
        public bool IsEnable { get; set; }
        
        public virtual void Show() =>
            _renderer.SetActive(true);

        public virtual void Hide() => 
            _renderer.SetActive(false);
        
        public abstract void Initialize(params object[] objects);
    }
}