using System;
using QuantumGate.GameObjects.Contents.Abstract;

namespace QuantumGate.GameObjects.Contents.Concrete
{
    [Serializable]
    public class Video : Content
    {
        protected Video()
        {
        }

        [Obsolete("Only for initializing manifest")]
        public Video(string name, string path)
        {
            Name = name;
            Path = path;
        }
    }
}