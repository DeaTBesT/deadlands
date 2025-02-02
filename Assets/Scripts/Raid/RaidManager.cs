using System;
using System.Collections.Generic;
using DL.CoreRuntime;
using DL.Data.Scene;
using DL.InterfacesRuntime;
using DL.RaidRuntime.Spawners;
using DL.SceneTransitionRuntime;
using DL.UtilsRuntime;
using DL.UtilsRuntime.TimerSystemRuntime.Common;
using UnityEngine;

namespace DL.RaidRuntime
{
    public class RaidManager : Singleton<RaidManager>, IInitialize, IDeinitialize
    {
        private const float StartTimerTime = 300; //В секундах

        [SerializeField] private EnemySpawnController _enemySpawnController;
        
        [SerializeField] private SceneConfig _baseSceneConfig;
        
        private RaidControllerUI _raidControllerUI;
        private EntityStats _playerStats;
        private SimpleTimer _timer;

        public Action OnStartRaid { get; set; }
        public Action OnStopRaid { get; set; }
        public Action OnFinishRaidSuccess { get; set; }
        public Action OnFinishRaidFail { get; set; }

        public Action<float> OnRaidTimeChanged { get; set; }

        public bool IsEnable { get; set; }

        private static readonly List<IEscapeZone> EscapeZones = new();

        private static void ResetStatics(SceneConfig sceneConfig)
        {
            // reset all statics
            EscapeZones.Clear();
        }

        public void Initialize(params object[] objects)
        {  
            _raidControllerUI = objects[0] as RaidControllerUI;
            _raidControllerUI.Initialize(this, (Action)LoadBaseScene);

            SceneLoader.OnStartLoadScene += ResetStatics;
        }
        
        public void Deinitialize(params object[] objects)
        {
            _raidControllerUI.Deinitialize();
            
            SceneLoader.OnStartLoadScene -= ResetStatics;
        }
        
        public void StartRaid(Transform player, Camera playerCamera)
        {
            if (IsEnable)
            {
                return;
            }

            if (player.TryGetComponent(out _playerStats))
            {
                _playerStats.OnDeath += OnPlayerEscapedFail;
            }

            StartTimer();

            _enemySpawnController.Initialize(player, playerCamera);

            IsEnable = true;
            OnStartRaid?.Invoke();
        }

        public void StopRaid()
        {
            if (!IsEnable)
            {
                return;
            }

            if (_playerStats != null)
            {
                _playerStats.OnDeath -= OnPlayerEscapedFail;
            }
            
            StopTimer();

            _enemySpawnController.Deinitialize();
            
            IsEnable = false;
            OnStopRaid?.Invoke();
        }

        public void RegisterEscapeZones(IEscapeZone escapeZone)
        {
            if (EscapeZones.Contains(escapeZone))
            {
                Debug.LogWarning("Zone already registered");
                return;
            }
            
            EscapeZones.Add(escapeZone);
            escapeZone.OnEscaped += OnPlayerEscapedSuccess;
        }

        public void UnRegisterEscapeZones(IEscapeZone escapeZone)
        {
            EscapeZones.Remove(escapeZone);
            escapeZone.OnEscaped -= OnPlayerEscapedSuccess;
        }

        private void StartTimer()
        {
            _timer = new SimpleTimer(this, StartTimerTime)
            {
                OnChangedTime = x => OnRaidTimeChanged?.Invoke(x),
                OnTimerStop = OnPlayerEscapedFail //TODO: заменить на большую волну зомби
            };

            _timer.Start();
        }

        private void StopTimer() =>
            _timer?.Stop();

        private void OnPlayerEscapedSuccess()
        {
            //Тут какие-нибудь манипуляции с камерой
            OnFinishRaidSuccess?.Invoke();
        }

        private void OnPlayerEscapedFail()
        {
            //Тут какие-нибудь манипуляции с камерой
            OnFinishRaidFail?.Invoke();
        }

        private void LoadBaseScene() => 
            SceneLoader.Instance.LoadScene(_baseSceneConfig);
    }
}