using System.ComponentModel;
using System.Windows.Forms;
using QuantumGate.GameObjects;
using QuantumGate.GameObjects.States.Concrete;

namespace QuantumGate
{
    public partial class GameWindow : Form
    {
        private delegate void NoArgReturningVoidDelegate();
        private delegate void ControlArgReturningVoidDelegate(Control text);
        private string _title = $"{Application.ProductName} v{Application.ProductVersion}";
        private readonly BackgroundWorker _backgroundWorker = new BackgroundWorker();


        public GameWindow()
        {
            InitializeComponent();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            StateManager.LoadState(new MenuState(this));
        }

        #region ThreadSafeCalls
        public void AddControl(Control control)
        { 
            if (InvokeRequired)
            {
                var d = new ControlArgReturningVoidDelegate(AddControl);
                Invoke(d, control);
            }
            else
            {
                Controls.Add(control);
            }
        }

        public void ClearControls()
        {
            if (InvokeRequired)
            {
                var d = new NoArgReturningVoidDelegate(ClearControls);
                Invoke(d, null);
            }
            else
            {
                Controls.Clear();
            }
        }
        #endregion
    }
}
