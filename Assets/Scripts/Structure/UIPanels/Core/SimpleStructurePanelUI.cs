using DL.UIRuntime;

namespace DL.StructureRuntime.UIPanels.Core
{
    public abstract class SimpleStructurePanelUI : UIPanel
    {
        public abstract void Initialize(params object[] objects);
    }
}