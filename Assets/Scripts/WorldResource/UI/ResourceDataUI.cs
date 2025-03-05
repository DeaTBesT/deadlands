using DL.Data.Resource;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DL.WorldResourceRuntime.UI
{
    public class ResourceDataUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textTitle;
        [SerializeField] private TextMeshProUGUI _textDescription;
        [SerializeField] private TextMeshProUGUI _textAmmount;
        [SerializeField] private Image _resourceImage;

        public ResourceDataModel ResourceData { get; private set; }

        public void ChangeResourceData(ResourceDataModel resourceData)
        {
            ResourceData = resourceData;
            _resourceImage.sprite = ResourceData.ResourceConfig.ItemSprite;
            UpdateResource();
        }

        public void UpdateResource() => 
            _textAmmount.text = $"{ResourceData.AmountResource}";
    }
}