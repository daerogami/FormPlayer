using System.Collections.Generic;
using QuantumGate.GameObjects.Beats.Interface;

namespace QuantumGate.GameObjects.Cues.Interface
{
    public interface ICue
    {
        ICollection<IBeat> Beats { get; set; }
    }
}