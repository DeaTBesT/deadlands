using DL.InterfacesRuntime;

namespace UI.Core
{
    public interface IPanelUI : IInitialize
    {
        void Show();
        
        void Hide();
    }
}