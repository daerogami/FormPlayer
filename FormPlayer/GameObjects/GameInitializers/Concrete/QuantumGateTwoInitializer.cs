using System.Windows.Forms;
using QuantumGate.GameObjects.GameInitializers.Abstract;

namespace QuantumGate.GameObjects.GameInitializers.Concrete
{
    public sealed class QuantumGateTwoInitializer : GameDataInitializer
    {
        public override string CachePath => $@"{CacheFolderPath}\QuantumGateTwo";

        public QuantumGateTwoInitializer(ProgressBar progressBar)
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