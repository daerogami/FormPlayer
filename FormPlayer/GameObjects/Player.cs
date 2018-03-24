using System;
using System.Collections.Generic;

namespace QuantumGate.GameObjects
{
    [Serializable]
    public static class Player
    {
        public static string Name { get; private set; }
        public static int TotalMoveCount { get; private set; }
        public static int Mood { get; private set; } = 5; //5 is neutral, 1 is angry, 10 is happy
        private static IDictionary<string, int> SceneStates = new Dictionary<string, int>();
        public static string CurrentStageName { get; set; }


        public static void UpdateCurrentStage(Stage stage)
        {
            CurrentStageName = stage.Name;
            TotalMoveCount++;
        }

        public static int GetStateForCurrentStage(this Stage stage)
        {
            return SceneStates.ContainsKey(stage.Name) ? SceneStates[stage.Name] : 0;
        }

        public static void SetStateForCurrentStage(this Stage stage, int stateIndex)
        {
            if (SceneStates.ContainsKey(stage.Name))
            {
                SceneStates[stage.Name] = stateIndex;
            }
            else
            {
                SceneStates.Add(stage.Name, stateIndex);
            }
        }
    }
}
