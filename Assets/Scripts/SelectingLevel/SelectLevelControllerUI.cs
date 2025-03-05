using System.Collections.Generic;
using DL.InterfacesRuntime;
using DL.SceneTransitionRuntime;
using DL.SelectingLevel.Models;
using DL.UIRuntime;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.SelectingLevel
{
    public class SelectLevelControllerUI : MonoBehaviour, IInitialize, IDeinitialize
    {
        [SerializeField] private Button _buttonSelectLevelPrefab;
        [SerializeField] private Transform _buttonsPivot;

        [SerializeField] private UIPanel _selectLevelsPanel;
        [SerializeField] private Button _closePanelButton;

        private SelectLevelManager _selectLevelManager;

        public bool IsEnable { get; set; } = true;

        public void Initialize(params object[] objects)
        {
            _selectLevelManager = objects[0] as SelectLevelManager;

            if (_selectLevelManager != null)
            {
                _selectLevelManager.OnShowPanel += OnShowPanel;
                _selectLevelManager.OnLevelsInfoInitialize += OnGenerateButtons;
            }

            if (_closePanelButton != null)
            {
                _closePanelButton.onClick.AddListener(OnHidePanel);
            }
            
            _selectLevelsPanel.Hide();
        }

        public void Deinitialize(params object[] objects)
        {
            if (_selectLevelManager != null)
            {
                _selectLevelManager.OnShowPanel -= OnShowPanel;
                _selectLevelManager.OnLevelsInfoInitialize -= OnGenerateButtons;
            }

            if (_closePanelButton != null)
            {
                _closePanelButton.onClick.RemoveListener(OnHidePanel);
            }
        }

        private void OnShowPanel() =>
            _selectLevelsPanel.Show();

        private void OnHidePanel() =>
            _selectLevelsPanel.Hide();

        private void OnGenerateButtons(List<LevelInfoModel> levelInfoModels)
        {
            ClearSelectLevelButtons();

            foreach (var levelInfo in levelInfoModels)
            {
                var newButtonObj = Instantiate(_buttonSelectLevelPrefab.gameObject, _buttonsPivot);


                var buttonText = newButtonObj.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    var str = string.IsNullOrEmpty(levelInfo.levelName)
                        ? levelInfo.sceneConfig.SceneName
                        : levelInfo.levelName;
                    buttonText.text = str;
                }

                if (newButtonObj.TryGetComponent(out Button button))
                {
                    button.onClick.AddListener(SelectLevel);
                }

                continue;

                void SelectLevel()
                {
                    SceneLoader.Instance.LoadScene(levelInfo.sceneConfig);
                    _selectLevelsPanel.Hide();
                }
            }
        }

        private void ClearSelectLevelButtons()
        {
            foreach (Transform child in _buttonsPivot)
            {
                Destroy(child.gameObject);
            }
        }
    }
}