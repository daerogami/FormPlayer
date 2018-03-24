using System;
using QuantumGate.GameObjects.Cues.Abstract;
using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Cues.Concrete
{
    [Serializable]
    public sealed class OnVideoCompleteCue : Cue, IVideoCue
    {
    }
}