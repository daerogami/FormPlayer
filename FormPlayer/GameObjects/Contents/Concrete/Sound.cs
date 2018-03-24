using System;
using System.IO;
using Newtonsoft.Json;
using QuantumGate.GameObjects.Common.Interfaces;
using QuantumGate.GameObjects.Contents.Abstract;

namespace QuantumGate.GameObjects.Contents.Concrete
{
    [Serializable]
    public class Sound: Content, ILoadable
    {
        [JsonIgnore]
        public Stream Data { get; set; }


        public bool IsLoaded => Data?.CanRead ?? false;

        public void Load()
        {
            if (IsLoaded)
            {
                return;
            }
            Data = File.OpenRead(Path);
        }

        public void Unload()
        {
            if (!IsLoaded)
            {
                return;
            }
            Data = null;
        }
    }
}