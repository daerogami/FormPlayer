using QuantumGate.GameObjects.Cues.Interface;

namespace QuantumGate.GameObjects.Beats.Interface
{
    public interface ITriggerCue
    {
        ICue CueToTrigger { get; set; }
    }
}