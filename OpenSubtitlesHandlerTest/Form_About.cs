/* This file is part of OpenSubtitles Handler
   A library that handle OpenSubtitles.org XML-RPC methods.

   Copyright © Ala Ibrahim Hadid 2013 - 2021

   This program is free software: you can redistribute it and/or modify
   it under the terms of the GNU General Public License as published by
   the Free Software Foundation, either version 3 of the License, or
   (at your option) any later version.

   This program is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
   GNU General Public License for more details.

   You should have received a copy of the GNU General Public License
   along with this program.  If not, see <http://www.gnu.org/licenses/>.

   Author email: mailto:alaahadidfreeware@gmail.com

 */
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
