using System;
using Enums;
using Interfaces;
using UI.PlacePanels;
using UnityEngine;

namespace Place.Core
{
    public class PlaceControllerUI : MonoBehaviour, IInitialize
    {
        [SerializeField] private BuildPanelUI _buildPanel;
        [SerializeField] private UpgradePanelUI _upgradePanel;

        private PlaceController _placeController;

        public bool IsEnable { get; set; }

        public void Initialize(params object[] objects)
        {
            _placeController = objects[0] as PlaceController;

            _buildPanel.AddOnClickEvent(OnBuildButtonClick);
            _upgradePanel.AddOnClickEvent(OnUpgradeButtonClick);

            ClosePanels();
        }

        public void OpenPanel()
        {
            switch (_placeController.CurrentState)
            {
                case PlaceState.Build:
                {
                    _buildPanel.Show();
                }
                    break;
                case PlaceState.Upgrade:
                {
                    _upgradePanel.Show();
                }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void ClosePanels()
        {
            _buildPanel.Hide();
            _upgradePanel.Hide();
        }

        private void OnBuildButtonClick()
        {
            if (!_placeController.BuildPlace())
            {
                return;
            }
            
            _buildPanel.Hide();
            _upgradePanel.Show();
        }

        private void OnUpgradeButtonClick()
        {
            _placeController.UpgradePlace();
        }
    }
}