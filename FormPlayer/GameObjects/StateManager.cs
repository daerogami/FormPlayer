using System.Collections.Generic;
using QuantumGate.GameObjects.States.Interface;

namespace QuantumGate.GameObjects
{
    public static class StateManager
    {
        private static Stack<IState> States { get; } = new Stack<IState>();
        public static IState ActiveState => States.Peek();

        public static void LoadState(IState state)
        {
            States.Push(state);
            state.Initialize();
        }

        public static void PauseActiveState()
        {
            States.Peek().Pause();
        }

        public static void DropActive()
        {
            ActiveState.Cleanup();
            States.Pop();
        }
    }
}