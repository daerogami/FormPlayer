using System;

namespace QuantumGate.GameObjects.States.Interface
{
    public interface IState
    {
        string Id { get; }
        bool IsInitializationComplete { get; }
        bool IsPaused { get; }
        TimeSpan GetElapsedTime { get; }

        void Initialize();
        void ReInitialize();
        void DeInitialize();
        void Pause();
        void Resume();
        void Cleanup();
    }
}