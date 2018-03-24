using QuantumGate.GameObjects.States.Abstract;

namespace QuantumGate.GameObjects.States.Concrete
{
    public class EditorState : State
    {
        public override string Id { get; } = typeof(EditorState).Name;

        public EditorState(GameWindow gameWindow)
            : base(gameWindow)
        {
        }
    }
}