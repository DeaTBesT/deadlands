using DL.CommonStructure.SimpleStructureRuntime;

namespace Structure.CommonStructure.WardrobeStructure
{
    public class WardrobeStructureControllerUI : SimpleStructureControllerUI
    {
        public override void Initialize(params object[] objects)
        {
            _generalPanel.Initialize();
            _generalPanel.Hide();
        }
    }
}