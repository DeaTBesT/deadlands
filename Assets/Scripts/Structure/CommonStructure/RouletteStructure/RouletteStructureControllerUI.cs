using DL.CommonStructure.SimpleStructureRuntime;

namespace Structure.CommonStructure.RouletteStructure
{
    public class RouletteStructureControllerUI : SimpleStructureControllerUI
    {
        public override void Initialize(params object[] objects)
        {
            _generalPanel.Initialize();
            _generalPanel.Hide();
        }
    }
}