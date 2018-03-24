using System.Collections.Generic;
using System.Linq;
using QuantumGate.GameObjects.GameInitializers.Interface;

namespace QuantumGate.GameObjects.GameInitializers.Abstract
{
    public abstract class StoryData : IStoryData
    {
        public string InitialStage { get; protected set; }
        private ICollection<Stage> Stages { get; } = new HashSet<Stage>();
        public string ExternalGameDataPath { get; }


        protected StoryData(string externalGameDataPath)
        {
            ExternalGameDataPath = externalGameDataPath;
        }

        public virtual void AddStage(Stage stage)
        {
            Stages.Add(stage);
        }

        public virtual Stage GetStageByName(string name)
        {
            return Stages.SingleOrDefault(s => s.Name == name);
        }
    }
}