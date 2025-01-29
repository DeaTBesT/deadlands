using UI.Core;
using UnityEngine;

namespace DL.UIRuntime
{
    public class UIPanel : MonoBehaviour, IPanelUI
    {
        [SerializeField] private Canvas _panelCanvas;

        public bool IsEnable { get; set; }
        
        public virtual void Initialize(params object[] objects)
        {
            throw new System.NotImplementedException();
        }
        
        public void Show() => 
            _panelCanvas.enabled = true;

        public void Hide() => 
            _panelCanvas.enabled = false;
    }
}