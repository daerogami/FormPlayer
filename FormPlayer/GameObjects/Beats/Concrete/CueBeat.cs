using System;
using QuantumGate.GameObjects.Beats.Interface;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    [Serializable]
    public sealed class CueBeat: IBeat, ITriggerCue
    {
        public ICue CueToTrigger { get; set; }
    }
}