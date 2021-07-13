using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NeiraEngine.Run
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            Activate();
            BackgroundImage = Image.FromFile(EngineHelper.path_resources_textures + "/SplashScreen.png");
            Width = BackgroundImage.Width;
            Height = BackgroundImage.Height;
        }
    }
}
