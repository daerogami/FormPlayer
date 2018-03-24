using System;
using System.Drawing;
using QuantumGate.GameObjects.Beats.Interface;

namespace QuantumGate.GameObjects.Beats.Concrete
{
    [Serializable]
    public sealed class MovementBeat: IBeat
    {
        public Point Destination { get; set; }
        public int Speed { get; set; }
    }
}