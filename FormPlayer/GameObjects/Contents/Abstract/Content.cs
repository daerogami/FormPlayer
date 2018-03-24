using QuantumGate.GameObjects.Contents.Interface;

namespace QuantumGate.GameObjects.Contents.Abstract
{
    public abstract class Content: IContent
    {
        public string Name { get; protected set; }
        public string Path { get; protected set; }
    }
}