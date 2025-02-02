using System;
using Data;
using DL.CoreRuntime;
using DL.InterfacesRuntime;
using DL.UtilsRuntime.TimerSystemRuntime.Common;
using DL.UtilsRuntime.TimerSystemRuntime.Core;
using NaughtyAttributes;
using UnityEngine;

namespace DL.RaidRuntime
{
    [DisallowMultipleComponent]
    public class EscapeZone : MonoBehaviour, IEscapeZone
    {
        private const float EscapeTime = 2f;//В секундах
     
        [ShowNonSerializedField] private float _leftEscapeTime;
        
        private Timer _timer;
        
        public Action OnEscaped { get; set; }

        private void Start() =>
            RaidManager.Instance.RegisterEscapeZones(this);

        // private void OnDestroy() => 
        //     RaidManager.Instance?.UnRegisterEscapeZones(this);
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out EntityStats entityStats))
            {
                return;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
            {
                return;
            }

            StartEscapeTimer();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out EntityStats entityStats))
            {
                return;
            }

            if (entityStats.TeamId != Teams.PlayerTeamId)
            {
                return;
            }

            StopEscapeTimer();
        }

        private void StartEscapeTimer()
        {
            _timer = new SimpleTimer(this, EscapeTime)
            {
                OnTimerStop = OnEscaped,
                OnChangedTime = x => _leftEscapeTime = x,
            };
            
            _timer.Start();
        }

        private void StopEscapeTimer() => 
            _timer?.Stop();

        [Button("Escape")]
        private void OnEscapedButton() => 
            OnEscaped?.Invoke();
    }
}