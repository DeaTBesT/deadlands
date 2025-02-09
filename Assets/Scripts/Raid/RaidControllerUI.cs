using System;
using System.Collections.Generic;
using DL.InterfacesRuntime;
using DL.UIRuntime;
using DL.UtilsRuntime;
using TMPro;
using UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DL.RaidRuntime
{
    public class RaidControllerUI : MonoBehaviour, IInitialize, IDeinitialize
    {
        [Header("Raid")] [SerializeField] private TextMeshProUGUI _textRaidTimer;
        [SerializeField] private UIPanel _raidPanel;

        [Header("Results")] [SerializeField] private UIPanel _generalPanel;
        [SerializeField] private UIPanel _successPanel;
        [SerializeField] private UIPanel _failurePanel;

        [SerializeField] private Button _confirmButton;

        private UIPanelList _panels = new();

        private RaidManager _raidManager;

        private Action _loadAction;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }

            _raidManager = objects[0] as RaidManager;
            _loadAction = (Action)objects[1];

            if (_confirmButton != null)
            {
                _confirmButton.onClick.AddListener(OnConfirmButtonClicked);
            }

            if (_raidManager != null)
            {
                _raidManager.OnStartRaid += OnStartRaid;
                _raidManager.OnStopRaid += OnStopRaid;
                _raidManager.OnFinishRaidSuccess += OnFinishRaidSuccess;
                _raidManager.OnFinishRaidFail += OnFinishRaidFail;
                _raidManager.OnRaidTimeChanged += OnUpdateRaidTimer;
            }

            _panels.Add(_generalPanel);
            _panels.Add(_successPanel);
            _panels.Add(_failurePanel);
            _panels.Add(_raidPanel);

            _panels.ClosePanels();

            IsEnable = true;
        }

        public void Deinitialize(params object[] objects)
        {
            if (!IsEnable)
            {
                return;
            }

            if (_confirmButton != null)
            {
                _confirmButton.onClick.RemoveListener(OnConfirmButtonClicked);
            }

            if (_raidManager != null)
            {
                _raidManager.OnStartRaid -= OnStartRaid;
                _raidManager.OnStopRaid -= OnStopRaid;
                _raidManager.OnFinishRaidSuccess -= OnFinishRaidSuccess;
                _raidManager.OnFinishRaidFail -= OnFinishRaidFail;
                _raidManager.OnRaidTimeChanged -= OnUpdateRaidTimer;
            }

            IsEnable = false;
        }

        private void OnStartRaid()
        {
            _raidPanel.Show();
        }

        private void OnStopRaid()
        {
            _raidPanel.Hide();
            _panels.ClosePanels();
        }

        private void OnFinishRaidSuccess()
        {
            ShowSuccessPanel();
        }

        private void OnFinishRaidFail()
        {
            ShowFailurePanel();
        }

        private void ShowSuccessPanel()
        {
            _panels.ClosePanels();
            _generalPanel.Show();
            _successPanel.Show();

            //Анимации UI
        }

        private void ShowFailurePanel()
        {
            _panels.ClosePanels();
            _generalPanel.Show();
            _failurePanel.Show();

            //Анимации UI
        }

        private void OnUpdateRaidTimer(float time) =>
            _textRaidTimer.text = $"{time:00.00}";

        private void OnConfirmButtonClicked() =>
            _loadAction?.Invoke();
    }
}