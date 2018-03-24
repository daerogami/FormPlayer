using System;
using QuantumGate.GameObjects.GameInitializers.Abstract;

namespace QuantumGate.GameObjects.GameInitializers.Concrete
{
    [Serializable]
    public class QuantumGateStoryData : StoryData
    {
        public QuantumGateStoryData(string externalGameDataPath)
            : base(externalGameDataPath)
        {
            InitialStage = "MENUSCRN";
        }
    }
}