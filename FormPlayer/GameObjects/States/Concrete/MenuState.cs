using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using QuantumGate.GameObjects.GameInitializers.Concrete;
using QuantumGate.GameObjects.GameInitializers.Interface;
using QuantumGate.GameObjects.States.Abstract;

namespace QuantumGate.GameObjects.States.Concrete
{
    public class MenuState : State
    {
        private Bitmap QuantumGateOneCover { get; }
        private Bitmap QuantumGateTwoCover { get; }
        public override string Id => typeof(MenuState).Name;

        public MenuState(GameWindow gameWindow)
            : base(gameWindow)
        {
            QuantumGateOneCover = new Bitmap($@"{Environment.CurrentDirectory}\Resources\QuantumGate\quantumgatecover_sega.jpg");
            QuantumGateTwoCover = new Bitmap($@"{Environment.CurrentDirectory}\Resources\QuantumGate2\quantumgate2cover.jpg");

            BuildElements();
        }

        private void BuildElements()
        {
            Elements.Add(new PictureBox
            {
                Name = "Background",
                Size = new Size(Window.Width / 2, Window.Height),
                Location = new Point(Window.Width / 2, 0)
            });
            Elements.Add(new Label
            {
                Name = "Title",
                BackColor = Color.Ivory,
                Size = new Size(200, 20),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(0, 0),
                Text = @"Welcome to the MenuState!"
            });
            Elements.Add(new Button
            {
                Name = "Button1",
                BackColor = Color.Ivory,
                Size = new Size(200, 25),
                Location = new Point(0, 25),
                Text = @"Start: Quantum Gate One!"
            });
            Elements.Add(new Button
            {
                Name = "Button2",
                BackColor = Color.Ivory,
                Size = new Size(200, 25),
                Location = new Point(0, 50),
                Text = @"Start: Quantum Gate Two!"
            });
            Elements.Add(new ProgressBar
            {
                Name = "Progress",
                ForeColor = Color.DarkBlue,
                BackColor = Color.Ivory,
                Size = new Size((int)Math.Round(Window.Size.Width * 0.9), 15),
                Location = new Point((int)Math.Round(Window.Size.Width * 0.05), (int)Math.Round(Window.Size.Height * 0.85)),
                Style = ProgressBarStyle.Continuous,
                Minimum = 0,
                Maximum = 100,
                Step = 1,
                Visible = false
            });
            Elements.Add(new Label
            {
                Name = "ProgressStatus",
                BackColor = Color.Ivory,
                Size = new Size((int)Math.Round(Window.Size.Width * 0.9), 15),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point((int)Math.Round(Window.Size.Width * 0.05), (int)Math.Round(Window.Size.Height * 0.85) - 20),
                Text = "",
                Visible = false
            });
            var progressBar = Elements.Single(c => c.Name == "Progress");
            progressBar.TextChanged += ProgressBar_TextChanged;
            var button1 = Elements.Single(c => c.Name == "Button1");
            button1.Click += Button1_Click;
            button1.MouseHover += Button1_Hover;
            button1.MouseLeave += Button1_Leave;
            var button2 = Elements.Single(c => c.Name == "Button2");
            button2.Click += Button2_Click;
            button2.MouseHover += Button2_Hover;
            button2.MouseLeave += Button2_Leave;
        }

        private void ProgressBar_TextChanged(object sender, EventArgs e)
        {
            var progressBar = (ProgressBar)sender;
            var statusText = Elements.Single(c => c.Name == "ProgressStatus");
            statusText.Text = progressBar.Text;
            if (!statusText.Visible)
            {
                statusText.Visible = true;
            }
        }

        public override void Initialize()
        {
            base.Initialize();
            //LOW: There should be a cleaner way to manage a control's zIndex (maybe create a class that manages composition, i.e. Stage Manager)
            var progressBar = Elements.Single(c => c.Name == "Progress");
            progressBar.BringToFront();
            var statusText = Elements.Single(c => c.Name == "ProgressStatus");
            statusText.BringToFront();
        }

        #region EventHandlers
        private void Button1_Click(object sender, EventArgs e)
        {
            StartGame(Story.QuantumGateOne); //HIGH: Need to prompt or detect game location
        }

        private void Button1_Hover(object sender, EventArgs e)
        {
            var background = Elements.OfType<PictureBox>().Single(c => c.Name == "Background");
            background.Size = new Size(Window.Height, Window.Height);
            background.Location = new Point(Window.Width - Window.Height, 0);
            background.Image = QuantumGateOneCover;
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            background.Visible = true;
        }

        private void Button1_Leave(object sender, EventArgs e)
        {
            var background = Elements.OfType<PictureBox>().Single(c => c.Name == "Background");
            background.Image = null;
            background.Visible = false;
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            StartGame(Story.QuantumGateTwo); //HIGH: Need to prompt or detect game location
        }

        private void Button2_Hover(object sender, EventArgs e)
        {
            var background = Elements.OfType<PictureBox>().Single(c => c.Name == "Background");
            background.Size = new Size(Window.Height, Window.Height);
            background.Location = new Point(Window.Width - Window.Height, 0);
            background.Image = QuantumGateTwoCover;
            background.SizeMode = PictureBoxSizeMode.StretchImage;
            background.Visible = true;
        }

        private void Button2_Leave(object sender, EventArgs e)
        {
            var background = Elements.OfType<PictureBox>().Single(c => c.Name == "Background");
            background.Image = null;
            background.Visible = false;
        }
        #endregion

        private void StartGame(Story story)
        {
            var progressBar = Elements.OfType<ProgressBar>().Single(c => c.Name == "Progress"); // LOW: Could make static class to check the statemanager's states for MenuState and return progress bar instead of passing as param

            IGameDataInitializer initializer;
            try
            {
                switch (story)
                {
                    case Story.Custom:
                        initializer = new CustomStoryInitializer(progressBar);
                        break;
                    case Story.QuantumGateOne:
                        initializer = new QuantumGateInitializer(progressBar);
                        break;
                    case Story.QuantumGateTwo:
                        initializer = new QuantumGateTwoInitializer(progressBar);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(story), story, null);
                }
                Window.ClearControls();
                Window.AddControl(Elements.Single(c => c.Name == "Progress"));
                Window.AddControl(Elements.Single(c => c.Name == "ProgressStatus"));
                initializer.VerifyGameData();
            }
            catch (OperationCanceledException ex) when (ex.Message.Contains("Cancelled locating game data"))
            {
                const string errorMessage = "Cancelled locating game data";
                //throw new FailedToInitializeStateException(errorMessage, ex);
                MessageBox.Show(errorMessage);
                return;
            }
            catch (Exception ex) when (ex is InvalidGameDataException || ex is FileNotFoundException)
            {
                var errorMessage = "";
                var exception = ex as InvalidGameDataException;
                if (exception != null)
                {
                    errorMessage = $"Could not start {story}, {exception.BadFiles.Count()} bad files";
                }
                if (ex is FileNotFoundException)
                {
                    errorMessage = $"Could not start {story}, are you sure you have the right directory?";
                }
                //throw new FailedToInitializeStateException(errorMessage, ex); //HIGH: Maybe send this to a label in the menu instead of blowing up?
                MessageBox.Show(errorMessage);
                return;
            }
            Window.ClearControls();
            StateManager.LoadState(new GameState(Window, initializer.StoryData));
        }
    }
}