using System.Collections.Generic;
using UI.Core;

namespace DL.UtilsRuntime
{
    public class UIPanelList : List<IPanelUI>
    {
        public UIPanelList() : base()
        {
        }

        public void OpenPanels() => 
            ForEach(panel => panel.Show());

        public void ClosePanels() => 
            ForEach(panel => panel.Hide());
    }
}