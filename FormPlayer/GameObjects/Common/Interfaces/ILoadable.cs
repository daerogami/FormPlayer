namespace QuantumGate.GameObjects.Common.Interfaces
{
    //LOW: Maybe add LoadStart and LoadComplete events?
    public interface ILoadable
    {
        bool IsLoaded { get; }
        void Load();
        void Unload();
    }
}