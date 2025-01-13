using Interfaces;
using UnityEngine;

namespace UI.PlacePanels.Core
{
    public abstract class PlacePanelUI : MonoBehaviour, IPlacePanel
    {
        public abstract void Show();

        public abstract void Hide();
    }
}