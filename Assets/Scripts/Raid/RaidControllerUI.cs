using System;
using System.Collections.Generic;
using DL.InterfacesRuntime;
using DL.UIRuntime;
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

        private List<IPanelUI> _panels = new();

        private RaidManager _raidManager;

        private Action _loadAction;

        public bool IsEnable { get; set; } = true;

        public void Initialize(params object[] objects)
        {
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

            _panels = new List<IPanelUI>
            {
                _generalPanel,
                _successPanel,
                _failurePanel,
                _raidPanel
            };

            ClosePanels();
        }

        public void Deinitialize(params object[] objects)
        {
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
        }

        private void OnStartRaid()
        {
            _raidPanel.Show();
        }

        private void OnStopRaid()
        {
            _raidPanel.Hide();
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
            ClosePanels();
            _generalPanel.Show();
            _successPanel.Show();

            //Анимации UI
        }

        private void ShowFailurePanel()
        {
            ClosePanels();
            _generalPanel.Show();
            _failurePanel.Show();

            //Анимации UI
        }

        private void OnUpdateRaidTimer(float time) =>
            _textRaidTimer.text = $"{time:00.00}";

        private void ClosePanels() =>
            _panels.ForEach(panel => panel.Hide());

        private void OnConfirmButtonClicked() =>
            _loadAction?.Invoke();
    }
}