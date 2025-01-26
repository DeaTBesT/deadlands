using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DL.UIRuntime
{
    public class TimerUIPanel : UIPanel, IPanelUI
    {
        [SerializeField] private Image _imageTimer;

        private float _durationTime;
        
        public bool IsEnable { get; set; }
        public void Initialize(params object[] objects) => 
            _durationTime = (float)objects[0];

        public void SetTime(float time)
        {
            var amount = time / _durationTime;
            _imageTimer.fillAmount = amount;
        }
    }
}