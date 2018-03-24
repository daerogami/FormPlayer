using System;
using QuantumGate.GameObjects.Beats.Interface;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    [Serializable]
    public sealed class FadeBeat: IBeat, IHaveDuration, ISetOpacity
    {
        public int TargetOpacity { get; set; }
        public int Duration { get; set; }
    }
}