using NaughtyAttributes;
using UI.Core;
using UnityEngine;
using UnityEngine.Serialization;

namespace DL.UIRuntime
{
    public class UIPanel : MonoBehaviour, IPanelUI
    {
        [FormerlySerializedAs("_renderer")] [SerializeField] private GameObject _panelRenderer;

        public bool IsEnable { get; set; }
        
        public virtual void Initialize(params object[] objects)
        {
            throw new System.NotImplementedException();
        }

        [Button]
        public virtual void Show() =>
            _panelRenderer.SetActive(true);

        [Button]
        public virtual void Hide() => 
            _panelRenderer.SetActive(false);
    }
}