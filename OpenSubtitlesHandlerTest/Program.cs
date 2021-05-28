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
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OpenSubtitlesHandlerTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <param name="args">Commandlines</param>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (args != null)
            {
                if (args.Length > 0)
                {
                    foreach (string arg in args)
                    {
                        switch (arg)
                        {
                            case "xmlrpc": Application.Run(new Form_XmlRpcTest()); return;
                        }
                    }
                }
            }
            Application.Run(new Form_Main()); // comment this and uncomment the next line to activate the XML-RPC test window
            //Application.Run(new Form_XmlRpcTest());
        }
    }
}
