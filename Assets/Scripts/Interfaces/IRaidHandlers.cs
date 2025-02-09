namespace DL.InterfacesRuntime
{
    public interface IRaidHandlers
    {
        void StartRaid(params object[] objects);

        void StopRaid();
    }
}