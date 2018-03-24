using System.Windows.Forms;
using QuantumGate.GameObjects.GameInitializers.Abstract;

namespace QuantumGate.GameObjects.GameInitializers.Concrete
{
    public sealed class CustomStoryInitializer : GameDataInitializer
    {
        private string Name { get; }
        public override string CachePath => $@"{CacheFolderPath}\{Name}";

        public CustomStoryInitializer(ProgressBar progressBar)
            : base(progressBar)
        {
        }

        public override void VerifyGameData()
        {
            throw new System.NotImplementedException();
        }

        public override void BuildGameCache()
        {
            throw new System.NotImplementedException();
        }
    }
}