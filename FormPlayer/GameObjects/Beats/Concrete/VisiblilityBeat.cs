using System;
using QuantumGate.GameObjects.Beats.Interface;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    [Serializable]
    public class VisiblilityBeat: IBeat, ISetVisibility
    {
        public bool TargetVisibility { get; set; }
    }
}