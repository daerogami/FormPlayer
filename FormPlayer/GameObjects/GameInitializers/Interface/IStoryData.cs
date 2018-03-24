
namespace QuantumGate.GameObjects.GameInitializers.Interface
{
    public interface IStoryData
    {
        string InitialStage { get; }
        string ExternalGameDataPath { get; }

        void AddStage(Stage stage);
        Stage GetStageByName(string name);
    }
}