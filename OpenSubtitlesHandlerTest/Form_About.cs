using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
namespace OpenSubtitlesHandlerTest
{
    public partial class Form_About : Form
    {
        public Form_About()
        {
            InitializeComponent();
            Version ver = Assembly.GetExecutingAssembly().GetName().Version;
            label_version.Text = "Version: " + ver.Major + "." + ver.Minor + "." + ver.Build + " Revision " + ver.Revision;

            Assembly asm = Assembly.LoadFile
                (System.IO.Path.Combine(Application.StartupPath,
                    "OpenSubtitlesHandler.dll"));

            ver = asm.GetName().Version;
            
            label_coreVersion.Text = "Core version: " + ver.Major + "." + ver.Minor + "." + ver.Build + " Revision " + ver.Revision;
      
        }

        private void label3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.opensubtitles.org");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
