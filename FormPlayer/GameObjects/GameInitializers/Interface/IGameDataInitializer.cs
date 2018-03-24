namespace QuantumGate.GameObjects.GameInitializers.Interface
{
    public interface IGameDataInitializer
    {
        IStoryData StoryData { get; }
        void VerifyGameData();
        void BuildGameCache();
    }
}