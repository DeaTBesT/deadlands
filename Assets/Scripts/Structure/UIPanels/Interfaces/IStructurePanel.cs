using DL.InterfacesRuntime;

namespace DL.StructureRuntime.UIPanels.Interfaces
{
    public interface IStructurePanel : IInitialize
    {
        void Show();
        
        void Hide();
    }
}