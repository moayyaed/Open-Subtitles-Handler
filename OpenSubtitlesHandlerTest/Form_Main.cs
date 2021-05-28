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
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OpenSubtitlesHandler;

namespace OpenSubtitlesHandlerTest
{
    public partial class Form_Main : Form
    {
        public Form_Main()
        {
            InitializeComponent();
            OSHConsole.LineWritten += OSHConsole_LineWritten;
            OSHConsole.UpdateLastLine += OSHConsole_UpdateLastLine;

            propertyGrid_tryUploadParameter.SelectedObject = new TryUploadSubtitlesParameters();
            propertyGrid_uploadSubtitles.SelectedObject = new UploadSubtitleInfoParameters();
            propertyGrid_InserMovieHash.SelectedObject = new InsertMovieHashParameters();
            OpenSubtitles.SetUserAgent("OS Test User Agent");
        }

        private delegate void WriteLineDelegate(string line, DebugCode status);
        private delegate void AddItemDelegate(object item);
        private string username, password, movieHash, movieSize, languageId, movieTitle, movieIMBID,
            insertMovieTitle, insertMovieYear, idSubMovieFile, comment, program, iso639, format, sublanguageid;
        private int score, idsubtitle, badsubtitle;
        private int[] downloadsList;
        private string[] detectLanguageTexts, hashes;
        private TryUploadSubtitlesParameters[] tryuploadParameters;
        private UploadSubtitleInfoParameters uploadParameters;
        private InsertMovieHashParameters[] insertMovieHashParameters;
        private Thread thr;

        private void ShowObjectProperties(object item)
        {
            if (!this.InvokeRequired)
            {
                this.ShowObjectProperties1(item);
            }
            else
            {
                this.Invoke(new AddItemDelegate(ShowObjectProperties1), new object[] { item });
            }
        }
        private void ShowObjectProperties1(object item)
        { propertyGrid1.SelectedObject = item; }

        private void AddItem(object item)
        {
            if (!this.InvokeRequired)
            {
                this.AddItem1(item);
            }
            else
            {
                this.Invoke(new AddItemDelegate(AddItem1), new object[] { item });
            }
        }
        private void AddItem1(object item)
        {
            listBox1.Items.Add(item);
        }
        private void CallMethod(Action method)
        {
            if (thr != null)
                if (thr.IsAlive)
                    thr.Abort();
            thr = new Thread(new ThreadStart(method));
            thr.Start();
        }

        #region OS Methods
        private void LogIn()
        {
            OpenSubtitles.LogIn(username, password, "en");
        }
        private void LogOut()
        {
            OpenSubtitles.LogOut();
        }
        private void NoOperation()
        {
            IMethodResponse result = OpenSubtitles.NoOperation();
            if (result is MethodResponseNoOperation)
            {
                ShowObjectProperties(result);
            }
        }
        private void SearchSubtitle()
        {
            List<SubtitleSearchParameters> parms = new List<SubtitleSearchParameters>();
            parms.Add(new SubtitleSearchParameters(languageId, movieHash, int.Parse(movieSize)));
            IMethodResponse result = OpenSubtitles.SearchSubtitles(parms.ToArray());
            if (result is MethodResponseSubtitleSearch)
            {
                foreach (SubtitleSearchResult sub in ((MethodResponseSubtitleSearch)result).Results)
                {
                    AddItem(sub);
                }
            }
        }
        private void DownloadSubtitle()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OpesSubtitlesHanderTest\\");
            IMethodResponse result = OpenSubtitles.DownloadSubtitles(downloadsList);
            if (result is MethodResponseSubtitleDownload)
            {
                foreach (SubtitleDownloadResult res in ((MethodResponseSubtitleDownload)result).Results)
                {
                    byte[] data = Convert.FromBase64String(res.Data);
                    byte[] target = Utilities.Decompress(new MemoryStream(data));
                    // now save the subtitle
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OpesSubtitlesHanderTest\\";
                    // Get file name
                    bool found = false;
                    foreach (SubtitleSearchResult v in listBox1.Items)
                    {
                        if (v.IDSubtitleFile == res.IdSubtitleFile)
                        {
                            fileName += v.SubFileName; found = true;
                            break;
                        }
                    }
                    if (!found) fileName += "Untitled.srt";// just in case
                    // write data
                    Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    stream.Write(target, 0, target.Length);
                    stream.Close();

                    OSHConsole.WriteLine("Done ! file saved to:");
                    OSHConsole.WriteLine(fileName);
                }
            }
        }
        private void SearchMoviesOnIMDB()
        {
            IMethodResponse result = OpenSubtitles.SearchMoviesOnIMDB(movieTitle);
            if (result is MethodResponseMovieSearch)
            {
                foreach (MovieSearchResult res in ((MethodResponseMovieSearch)result).Results)
                {
                    AddItem(res);
                }
            }
        }
        private void GetIMDBMovieDetails()
        {
            IMethodResponse result = OpenSubtitles.GetIMDBMovieDetails(movieIMBID);
            if (result is MethodResponseMovieDetails)
            {
                ShowObjectProperties(result);
            }
        }
        private void InsertMovie()
        {
            OpenSubtitles.InsertMovie(insertMovieTitle, insertMovieYear);
        }
        private void ServerInfo()
        {
            IMethodResponse result = OpenSubtitles.ServerInfo();
            if (result is MethodResponseServerInfo)
            {
                ShowObjectProperties(result);
            }
        }
        private void ReportWrongMovieHash()
        {
            OpenSubtitles.ReportWrongMovieHash(idSubMovieFile);
        }
        private void SubtitlesVote()
        { OpenSubtitles.SubtitlesVote(idsubtitle, score); }
        private void AddComment()
        {
            OpenSubtitles.AddComment(idsubtitle, comment, badsubtitle);
        }
        private void GetSubLanguages()
        {
            IMethodResponse result = OpenSubtitles.GetSubLanguages("en");
            if (result is MethodResponseGetSubLanguages)
            {
                ShowObjectProperties(result);
            }
        }
        private void DetectLanguage()
        {
            IMethodResponse result = OpenSubtitles.DetectLanguage(detectLanguageTexts, Encoding.UTF8);
            if (result is MethodResponseDetectLanguage)
            {
                ShowObjectProperties(result);
            }
        }
        private void GetAvailableTranslations()
        {
            IMethodResponse result = OpenSubtitles.GetAvailableTranslations(program);
            if (result is MethodResponseGetAvailableTranslations)
            {
                ShowObjectProperties(result);
            }
        }
        private void GetTranslation()
        {
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\OpesSubtitlesHanderTest\\");
            IMethodResponse result = OpenSubtitles.GetTranslation(iso639, format, program);
            if (result is MethodResponseGetTranslation)
            {
                if (((MethodResponseGetTranslation)result).ContentData != null)
                {
                    byte[] data = Convert.FromBase64String(((MethodResponseGetTranslation)result).ContentData);
                    byte[] target = Utilities.Decompress(new MemoryStream(data));
                    // now save the subtitle
                    string fileName = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) +
                        "\\OpesSubtitlesHanderTest\\Translation_" + program + "(" + iso639 + ")." + format;

                    // write data
                    Stream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
                    stream.Write(target, 0, target.Length);
                    stream.Close();

                    OSHConsole.WriteLine("Done ! file saved to:", DebugCode.Good);
                    OSHConsole.WriteLine(fileName, DebugCode.Good);
                }
            }
        }
        private void AutoUpdate()
        {
            IMethodResponse result = OpenSubtitles.AutoUpdate(program);
            if (result is MethodResponseAutoUpdate)
            {
                ShowObjectProperties(result);
            }
        }
        private void CheckMovieHash()
        {
            IMethodResponse result = OpenSubtitles.CheckMovieHash(hashes);
            if (result is MethodResponseCheckMovieHash)
            {
                ShowObjectProperties(result);
            }
        }
        private void CheckMovieHash2()
        {
            IMethodResponse result = OpenSubtitles.CheckMovieHash2(hashes);
            if (result is MethodResponseCheckMovieHash2)
            {
                ShowObjectProperties(result);
            }
        }
        private void CheckSubHash()
        {
            IMethodResponse result = OpenSubtitles.CheckSubHash(hashes);
            if (result is MethodResponseCheckSubHash)
            {
                ShowObjectProperties(result);
            }
        }
        private void TryUploadSubtitles()
        {
            IMethodResponse result = OpenSubtitles.TryUploadSubtitles(tryuploadParameters);
            if (result is MethodResponseTryUploadSubtitles)
            {
                ShowObjectProperties(result);
            }
        }
        private void UploadSubtitles()
        {
            IMethodResponse result = OpenSubtitles.UploadSubtitles(uploadParameters);
            if (result is MethodResponseUploadSubtitles)
            {
                ShowObjectProperties(result);
            }
        }
        private void GetComments()
        {
            IMethodResponse result = OpenSubtitles.GetComments(downloadsList);
            if (result is MethodResponseGetComments)
            {
                ShowObjectProperties(result);
            }
        }
        private void AddRequest()
        {
            IMethodResponse result = OpenSubtitles.AddRequest(sublanguageid, movieIMBID, comment);
            if (result is MethodResponseAddRequest)
            {
                ShowObjectProperties(result);
            }
        }
        private void ReportWrongImdbMovie()
        {
            IMethodResponse result = OpenSubtitles.ReportWrongImdbMovie(movieHash, movieSize, movieIMBID);
            if (result is MethodResponseReportWrongImdbMovie)
            {
                ShowObjectProperties(result);
            }
        }
        private void InsertMovieHash()
        {
            IMethodResponse result = OpenSubtitles.InsertMovieHash(insertMovieHashParameters);
            if (result is MethodResponseInsertMovieHash)
            {
                ShowObjectProperties(result);
            }
        }
        private void SearchToMail()
        {
            SearchToMailMovieParameter par = new SearchToMailMovieParameter();
            par.moviesize = double.Parse(movieSize);
            par.moviehash = movieHash;
            IMethodResponse result = OpenSubtitles.SearchToMail(new string[] { languageId },
                new SearchToMailMovieParameter[] { par });
            if (result is MethodResponseSearchToMail)
            {
                ShowObjectProperties(result);
            }
        }
        #endregion

        private void OSHConsole_LineWritten(object sender, DebugEventArgs e)
        {
            if (!this.InvokeRequired)
            { WriteLine(e.Text, e.Code); }
            else
            {
                this.Invoke(new WriteLineDelegate(WriteLine), new object[] { e.Text, e.Code });
            }
        }
        private void WriteLine(string line, DebugCode status = DebugCode.None)
        {
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            switch (status)
            {
                case DebugCode.Error: richTextBox1.SelectionColor = Color.Red; break;
                case DebugCode.Good: richTextBox1.SelectionColor = Color.LightGreen; break;
                case DebugCode.None: richTextBox1.SelectionColor = Color.White; break;
                case DebugCode.Warning: richTextBox1.SelectionColor = Color.Yellow; break;
            }
            richTextBox1.SelectedText = line + "\n";
            richTextBox1.ScrollToCaret();
        }
        private void OSHConsole_UpdateLastLine(object sender, DebugEventArgs e)
        {
            if (!this.InvokeRequired)
            { UpdateLastLine(e.Text, e.Code); }
            else
            {
                this.Invoke(new WriteLineDelegate(UpdateLastLine), new object[] { e.Text, e.Code });
            }
        }
        void UpdateLastLine(string line, DebugCode status = DebugCode.None)
        {
            WriteLine(line, status);//TODO: this should update last written line
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox_password.UseSystemPasswordChar = !checkBox1.Checked;
        }
        private void textBox_useragent_TextChanged(object sender, EventArgs e)
        {
            OpenSubtitles.SetUserAgent(textBox_useragent.Text);
        }
        // Log in
        private void button1_Click(object sender, EventArgs e)
        {
            username = textBox_username.Text;
            password = textBox_password.Text;
            CallMethod(LogIn);
        }
        // Log out
        private void button2_Click(object sender, EventArgs e)
        {
            username = textBox_username.Text;
            password = textBox_password.Text;
            CallMethod(LogOut);
        }
        // No Operation
        private void button3_Click(object sender, EventArgs e)
        {
            username = textBox_username.Text;
            password = textBox_password.Text;
            CallMethod(NoOperation);
        }
        private void Form_Main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (thr != null)
                if (thr.IsAlive)
                    thr.Abort();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            movieHash = textBox_movieHash.Text;
            movieSize = textBox_movieSize.Text;
            languageId = textBox_languageID.Text;
            listBox1.Items.Clear();
            CallMethod(SearchSubtitle);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = listBox1.SelectedItem;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            downloadsList = new int[] { int.Parse(((SubtitleSearchResult)listBox1.SelectedItem).IDSubtitleFile) };
            CallMethod(DownloadSubtitle);
        }

        private void button8_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            movieTitle = textBox_movieTitle.Text;
            CallMethod(SearchMoviesOnIMDB);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            if (!(listBox1.SelectedItem is MovieSearchResult))
            {
                MessageBox.Show("This is not a movie search result. Search for movie first.");
                return;
            }
            movieIMBID = ((MovieSearchResult)listBox1.SelectedItem).ID;
            CallMethod(GetIMDBMovieDetails);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            insertMovieTitle = textBox_insertMovieTitle.Text;
            insertMovieYear = textBox_insertMovieYear.Text;
            CallMethod(InsertMovie);
        }

        private void button11_Click(object sender, EventArgs e)
        {
            CallMethod(ServerInfo);
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                textBox_movieHash.Text = Utilities.ComputeHash(op.FileName);
                FileInfo info = new FileInfo(op.FileName);
                textBox_movieSize.Text = info.Length.ToString();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            if (!(listBox1.SelectedItem is SubtitleSearchResult))
            {
                MessageBox.Show("This is not a subtitle search result. Search for subtitles first.");
                return;
            }
            idSubMovieFile = ((SubtitleSearchResult)listBox1.SelectedItem).IDSubMovieFile;
            CallMethod(ReportWrongMovieHash);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            if (!(listBox1.SelectedItem is SubtitleSearchResult))
            {
                MessageBox.Show("This is not a subtitle search result. Search for subtitles first.");
                return;
            }
            idsubtitle = int.Parse(((SubtitleSearchResult)listBox1.SelectedItem).IDSubtitle);
            score = (int)numericUpDown1.Value;
            CallMethod(SubtitlesVote);
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            if (!(listBox1.SelectedItem is SubtitleSearchResult))
            {
                MessageBox.Show("This is not a subtitle search result. Search for subtitles first.");
                return;
            }
            idsubtitle = int.Parse(((SubtitleSearchResult)listBox1.SelectedItem).IDSubtitle);
            badsubtitle = checkBox_badSubtitle.Checked ? 1 : 0;
            comment = textBox_comment.Text;
            CallMethod(AddComment);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            CallMethod(GetSubLanguages);
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_EnterText frm = new Form_EnterText();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                comboBox1.Items.Add(frm.TextByUser);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count == 0)
            {
                MessageBox.Show("Please add texts first.");
                return;
            }
            List<string> texts = new List<string>();
            foreach (string txt in comboBox1.Items)
                texts.Add(txt);
            detectLanguageTexts = texts.ToArray();
            CallMethod(DetectLanguage);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (textBox_program.Text.Length == 0)
            {
                MessageBox.Show("Please enter program name first");
                return;
            }
            program = textBox_program.Text;
            CallMethod(GetAvailableTranslations);
        }

        private void button16_Click(object sender, EventArgs e)
        {
            iso639 = textBox_iso639.Text;
            format = textBox_format.Text;
            program = textBox_program.Text;
            CallMethod(GetTranslation);
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (textBox_program.Text.Length == 0)
            {
                MessageBox.Show("Please enter program name first");
                return;
            }
            program = textBox_program.Text;
            CallMethod(AutoUpdate);
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (comboBox_hashes.Items.Count == 0)
            {
                MessageBox.Show("Please add texts first.");
                return;
            }
            List<string> texts = new List<string>();
            foreach (string txt in comboBox_hashes.Items)
                texts.Add(txt);
            hashes = texts.ToArray();
            CallMethod(CheckMovieHash);
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_EnterText frm = new Form_EnterText();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                comboBox_hashes.Items.Add(frm.TextByUser);
            }
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox1.SelectedIndex >= 0)
            {
                comboBox1.Items.RemoveAt(comboBox1.SelectedIndex);
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
            }
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox_hashes.SelectedIndex >= 0)
            {
                comboBox_hashes.Items.RemoveAt(comboBox_hashes.SelectedIndex);
                if (comboBox_hashes.Items.Count > 0)
                    comboBox_hashes.SelectedIndex = 0;
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (comboBox_subHashes.Items.Count == 0)
            {
                MessageBox.Show("Please add texts first.");
                return;
            }
            List<string> texts = new List<string>();
            foreach (string txt in comboBox_hashes.Items)
                texts.Add(txt);
            hashes = texts.ToArray();
            CallMethod(CheckSubHash);
        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form_EnterText frm = new Form_EnterText();
            if (frm.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                comboBox_subHashes.Items.Add(frm.TextByUser);
            }
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox_subHashes.SelectedIndex >= 0)
            {
                comboBox_subHashes.Items.RemoveAt(comboBox_subHashes.SelectedIndex);
                if (comboBox_subHashes.Items.Count > 0)
                    comboBox_subHashes.SelectedIndex = 0;
            }
        }

        private void linkLabel8_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TryUploadSubtitlesParameters par = new TryUploadSubtitlesParameters();
            TryUploadSubtitlesParameters old = (TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject;
            par.moviebytesize = old.moviebytesize;
            par.moviefilename = old.moviefilename;
            par.moviefps = old.moviefps;
            par.movieframes = old.movieframes;
            par.moviehash = old.moviehash;
            par.movietimems = old.movietimems;
            par.subfilename = old.subfilename;
            par.subhash = old.subhash;
            comboBox_subtitlesToUpload.Items.Add(par);
        }

        private void linkLabel9_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ((TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject).subhash = Utilities.ComputeMd5(op.FileName);
                ((TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject).subfilename = Path.GetFileName(op.FileName);
            }
        }

        private void linkLabel10_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                ((TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject).moviehash = Utilities.ComputeHash(op.FileName);
                ((TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject).moviefilename = Path.GetFileName(op.FileName);
                FileInfo info = new FileInfo(op.FileName);
                ((TryUploadSubtitlesParameters)propertyGrid_tryUploadParameter.SelectedObject).moviebytesize = info.Length.ToString();
            }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (comboBox_subtitlesToUpload.Items.Count == 0)
            {
                MessageBox.Show("Please add parameters first.");
                return;
            }
            List<TryUploadSubtitlesParameters> pars = new List<TryUploadSubtitlesParameters>();
            foreach (TryUploadSubtitlesParameters txt in comboBox_subtitlesToUpload.Items)
                pars.Add(txt);
            tryuploadParameters = pars.ToArray();
            CallMethod(TryUploadSubtitles);
        }

        private void button21_Click(object sender, EventArgs e)
        {
            uploadParameters = (UploadSubtitleInfoParameters)propertyGrid_uploadSubtitles.SelectedObject;
            CallMethod(UploadSubtitles);
        }
        // add subtitle to upload
        private void linkLabel11_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "OPEN SUBTITLE FILE";
            if (op.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                UploadSubtitleParameters par = new UploadSubtitleParameters();
                string fileName = op.FileName;
                // let's see if the file is not larger that 100KB
                FileInfo inf = new FileInfo(op.FileName);
                if (inf.Length >= 102400)
                {
                 //   MessageBox.Show("This file is too large for the server ! (size > 100 KB)");
                  //  return;
                }
                // compress file
                byte[] fdata = Utilities.Compress(new FileStream(fileName, FileMode.Open, FileAccess.Read));
                // base64 decode
                par.subcontent = Convert.ToBase64String(fdata);

                par.subhash = Utilities.ComputeMd5(fileName);
                par.subfilename = Path.GetFileName(fileName);

                OpenFileDialog op1 = new OpenFileDialog();
                op1.Title = "OPEN MOVIE";
                if (op1.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    par.moviehash = Utilities.ComputeHash(op1.FileName);
                    par.moviefilename = Path.GetFileName(op1.FileName);
                    FileInfo info = new FileInfo(op1.FileName);
                    par.moviebytesize = info.Length;
                }
                ((UploadSubtitleInfoParameters)propertyGrid_uploadSubtitles.SelectedObject).CDS = new List<UploadSubtitleParameters>();
                ((UploadSubtitleInfoParameters)propertyGrid_uploadSubtitles.SelectedObject).CDS.Add(par);
            }
        }

        private void linkLabel12_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.opensubtitles.org");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            Form_About frm = new Form_About();
            frm.ShowDialog(this);
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (comboBox_hashes.Items.Count == 0)
            {
                MessageBox.Show("Please add texts first.");
                return;
            }
            List<string> texts = new List<string>();
            foreach (string txt in comboBox_hashes.Items)
                texts.Add(txt);
            hashes = texts.ToArray();
            CallMethod(CheckMovieHash2);
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex < 0)
            {
                MessageBox.Show("Please select result first !");
                return;
            }
            downloadsList = new int[] { int.Parse(((SubtitleSearchResult)listBox1.SelectedItem).IDSubtitleFile) };
            CallMethod(GetComments);
        }

        private void button26_Click(object sender, EventArgs e)
        {
            comment = textBox_AddRequest_comment.Text;
            movieIMBID = textBox_AddRequest_idmoviedb.Text;
            sublanguageid = textBox_AddRequest_sublanguageid.Text;
            CallMethod(AddRequest);
        }

        private void button27_Click(object sender, EventArgs e)
        {
            movieHash = textBox_report_moviehash.Text;
            movieSize = textBox_report_moviebytesize.Text;
            movieIMBID = textBox_report_imdbid.Text;
            CallMethod(ReportWrongImdbMovie);
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (comboBox_insertMovieHash.Items.Count == 0)
            {
                MessageBox.Show("Please add texts first.");
                return;
            }
            List<InsertMovieHashParameters> prs = new List<InsertMovieHashParameters>();
            foreach (InsertMovieHashParameters par in comboBox_insertMovieHash.Items)
                prs.Add(par);
            insertMovieHashParameters = prs.ToArray();
            CallMethod(InsertMovieHash);
        }

        private void linkLabel14_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            InsertMovieHashParameters par = new InsertMovieHashParameters();
            InsertMovieHashParameters old = (InsertMovieHashParameters)propertyGrid_InserMovieHash.SelectedObject;
            par.moviebytesize = old.moviebytesize;
            par.moviefilename = old.moviefilename;
            par.moviefps = old.moviefps;
            par.moviehash = old.moviehash;
            par.movietimems = old.movietimems;
            comboBox_insertMovieHash.Items.Add(par);
        }

        private void linkLabel13_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (comboBox_insertMovieHash.SelectedIndex >= 0)
            {
                comboBox_insertMovieHash.Items.RemoveAt(comboBox_insertMovieHash.SelectedIndex);
                if (comboBox_insertMovieHash.Items.Count > 0)
                    comboBox_insertMovieHash.SelectedIndex = 0;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            movieHash = textBox_movieHash.Text;
            movieSize = textBox_movieSize.Text;
            languageId = textBox_languageID.Text;
            CallMethod(SearchToMail);
        }
    }
}
