using System;
using QuantumGate.GameObjects.Beats.Interface;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    [Serializable]
    public class TriggerVideoStartBeat: IVideoBeat, ITriggerVideoStart
    {
        public bool TargetVisibility { get; set; }
    }
}