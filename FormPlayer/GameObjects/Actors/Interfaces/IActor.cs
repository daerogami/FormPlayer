using System.Collections.Generic;
using QuantumGate.GameObjects.Common.Interfaces;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Actors.Interfaces
{
    public interface IActor: ILoadable
    {
        ICollection<ICue> Cues { get; }

        void RegisterCues();
    }
}