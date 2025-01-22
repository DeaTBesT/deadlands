using DL.ConstructibleStructureRuntime.Core;

namespace Structure.ConstructibleStructure.SimpleStructure
{
    public class SimpleStructureControllerUI : ConstructibleStructureControllerUI
    {
        public override void Interact() => 
            OpenPanel();

        public override void FinishInteract() => 
            ClosePanels();
    }
}