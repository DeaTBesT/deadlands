namespace DL.InterfacesRuntime
{
    public interface IInitialize
    {
        bool IsEnable { get; set; }
        
        void Initialize(params object[] objects);
    }
}