using System.Collections.Generic;
using DL.Data.Scene;
using DL.SceneTransitionRuntime;
using DL.StructureRuntime.UIPanels.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.StructureRuntime.UIPanels.StructuresPanels
{
    public class MapSelectLevelPanelUI : SimpleStructurePanelUI
    {
        [SerializeField] private Button _closePanelButton;
        [SerializeField] private List<ButtonSelectLevel> _buttonsSelectLevels;
        
        [System.Serializable]
        public struct ButtonSelectLevel
        {
            public Button button;
            public string levelName;
            public SceneConfig sceneConfig;
        }

        public override void Initialize(params object[] objects)
        {
            if (IsEnable)
            {
                return;
            }
            
            InitializeButtons();
            IsEnable = true;
        }

        //TODO: сделать генерацию кнопок
        private void InitializeButtons()
        {
            _closePanelButton.onClick.AddListener(OnClickClosePanelButton);
            
            foreach (var buttonSelectedLevel in _buttonsSelectLevels)
            {
                var buttonText = buttonSelectedLevel.button.GetComponentInChildren<TextMeshProUGUI>();

                if (buttonText != null)
                {
                    var str = string.IsNullOrEmpty(buttonSelectedLevel.levelName)
                        ? buttonSelectedLevel.sceneConfig.SceneName
                        : buttonSelectedLevel.levelName;
                    buttonText.text = str;
                }

                buttonSelectedLevel.button.onClick.AddListener(SelectLevel);
                continue;

                void SelectLevel() => 
                    SceneLoader.Instance.LoadScene(buttonSelectedLevel.sceneConfig);
            }
        }

        private void OnClickClosePanelButton() => 
            Hide();
    }
}