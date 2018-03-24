using System.Collections.Generic;
using QuantumGate.GameObjects.Actors.Interfaces;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Actors.Abstract
{
    public abstract class Actor: IActor
    {
        public ICollection<ICue> Cues { get; } = new HashSet<ICue>();
        public abstract void RegisterCues();

        public abstract bool IsLoaded { get; }
        public abstract void Load();
        public abstract void Unload();
    }
}