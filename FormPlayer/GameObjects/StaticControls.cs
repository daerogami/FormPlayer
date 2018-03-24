using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using AxAXVLC;

namespace QuantumGate.GameObjects
{
    // TODO: Each control should probably have its own static class
    public static class StaticControls
    {
        public static IEnumerable<Control> StoryNavigationControls { get; } = new HashSet<Control>
        {
            new PictureBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Name = "SceneBox",
                SizeMode = PictureBoxSizeMode.Zoom,
                TabIndex = 0,
                TabStop = false
            },
            new AxVLCPlugin2 {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Enabled = true,
                Name = "MainVideoPlayer",
                TabIndex = 3
            },
            new PictureBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.PanNorth,
                Location = new Point(461, 112),
                Name = "TopNavigation",
                Size = new Size(280,208),
                TabIndex = 5,
                TabStop = false
            },
            new PictureBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.PanEast,
                Location = new Point(1022, 12),
                Name = "RightNavigation",
                Size = new Size(150, 417),
                TabIndex = 6,
                TabStop = false
            },
            new PictureBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.PanSouth,
                Location = new Point(168, 326),
                Name = "BottomNavigation",
                Size = new Size(848, 103),
                TabIndex = 7,
                TabStop = false
            },
            new PictureBox {
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.Transparent,
                Cursor = Cursors.PanWest,
                Location = new Point(12, 12),
                Name = "LeftNavigation",
                Size = new Size(150, 417),
                TabIndex = 8,
                TabStop = false
            }
        };

        public static Control GetControlByName(string name)
        {
            return StoryNavigationControls.SingleOrDefault(c => c.Name == name);
        }
    }
}