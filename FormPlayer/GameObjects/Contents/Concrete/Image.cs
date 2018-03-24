using System;
using System.Drawing;
using System.IO;
using Newtonsoft.Json;
using QuantumGate.GameObjects.Common.Interfaces;
using QuantumGate.GameObjects.Contents.Abstract;

namespace QuantumGate.GameObjects.Contents.Concrete
{
    [Serializable]
    public class Image : Content, ILoadable
    {
        [JsonIgnore]
        public Bitmap Data { get; private set; }


        public Image(string path, string name = "")
        {
            Path = path;
            Name = string.IsNullOrEmpty(name) ? new Guid().ToString() : name;
        }

        public bool IsLoaded => Data != null;

        public void Load()
        {
            if (IsLoaded)
            {
                return;
            }
            var stream = File.OpenRead(Path);
            Data = new Bitmap(stream);
        }

        public void Unload()
        {
            if (!IsLoaded)
            {
                return;
            }
            Data.Dispose();
        }
    }
}