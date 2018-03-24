using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using QuantumGate.GameObjects.States.Interface;

namespace QuantumGate.GameObjects.States.Abstract
{
    public abstract class State : IState
    {
        internal GameWindow Window { get; }
        public abstract string Id { get; }
        public bool IsInitializationComplete { get; private set; }
        public bool IsPaused { get; private set; }
        private readonly Stopwatch _stateTimer = Stopwatch.StartNew();
        public TimeSpan GetElapsedTime => _stateTimer.Elapsed;
        protected ICollection<Control> Elements { get; } = new HashSet<Control>(); //TODO: Elements should be a part of stage


        internal State(GameWindow gameWindow)
        {
            Window = gameWindow;
        }

        public virtual void Initialize()
        {
            Window.ClearControls();
            foreach (var control in Elements)
            {
                Window.AddControl(control);
            }
            IsInitializationComplete = true;
        }

        public virtual void ReInitialize()
        {
            if (IsInitializationComplete)
            {
                DeInitialize();
            }
            Initialize();
        }

        public virtual void DeInitialize()
        {
            Cleanup();
        }

        public virtual void Pause()
        {
            if (IsPaused) { return; }
            _stateTimer.Stop();
            IsPaused = true;
        }

        public virtual void Resume()
        {
            if (!IsPaused) { return; }
            // LOW: Why was this clearing controls?
            /*Window.ClearControls();
            foreach (var control in Elements)
            {
                Window.AddControl(control);;
            }*/
            _stateTimer.Start();
            IsPaused = false;
        }

        public virtual void Cleanup()
        {
            IsInitializationComplete = false;
            IsPaused = false;
            _stateTimer.Reset();
            Elements.Clear();
        }
    }
}