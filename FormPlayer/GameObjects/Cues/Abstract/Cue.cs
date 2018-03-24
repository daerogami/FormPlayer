using System.Collections.Generic;
using QuantumGate.GameObjects.Beats.Interface;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Cues.Abstract
{
    public abstract class Cue : ICue
    {
        public ICollection<IBeat> Beats { get; set; } = new HashSet<IBeat>();
    }
}