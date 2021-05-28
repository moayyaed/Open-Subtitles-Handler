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
namespace OpenSubtitlesHandlerTest
{
    partial class Form_XmlRpcTest
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button5 = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.button7 = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(436, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(421, 426);
            this.richTextBox1.TabIndex = 0;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Normal values";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(140, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(290, 26);
            this.label1.TabIndex = 2;
            this.label1.Text = "Generate request with method call and normal parameters.\r\nTest all types of param" +
    "eters like string, int ...etc";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 286);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(122, 41);
            this.button2.TabIndex = 3;
            this.button2.Text = "Struct of normal values";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(281, 26);
            this.label2.TabIndex = 4;
            this.label2.Text = "Generate request with method call and struct parameter.\r\nThe struct will contain " +
    "normal values.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(142, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(279, 26);
            this.label3.TabIndex = 6;
            this.label3.Text = "Generate request with method call and array parameter.\r\nThe array  will contain n" +
    "ormal values.";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 71);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(122, 41);
            this.button3.TabIndex = 5;
            this.button3.Text = "Array of normal values";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(142, 120);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(279, 39);
            this.label4.TabIndex = 8;
            this.label4.Text = "Generate request with method call and array parameter.\r\nThe array will contain 2 " +
    "structs each one contain normal\r\nvalues.";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 118);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(122, 41);
            this.button4.TabIndex = 7;
            this.button4.Text = "Array of normal structs !!";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "ARRAYS________________________________________________________";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(142, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(279, 39);
            this.label6.TabIndex = 11;
            this.label6.Text = "Generate request with method call and array parameter.\r\nThe array will contain 2 " +
    "arrays each one contain normal\r\nvalues.";
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 165);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(122, 41);
            this.button5.TabIndex = 10;
            this.button5.Text = "Array of normal arrays !!";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(142, 214);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(279, 39);
            this.label7.TabIndex = 13;
            this.label7.Text = "Generate request with method call and array parameter.\r\nThe array will contain 2 " +
    "structs each one contain array\r\nof normal values.";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 212);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(122, 41);
            this.button6.TabIndex = 12;
            this.button6.Text = "Array of structs of arrays !!";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 270);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(382, 13);
            this.label8.TabIndex = 14;
            this.label8.Text = "STRUCTS_______________________________________________________";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(140, 335);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(281, 39);
            this.label9.TabIndex = 16;
            this.label9.Text = "Generate request with method call and struct parameter.\r\nThe struct will contain " +
    "2 arrays each one contain normal \r\nvalues.";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 333);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(122, 41);
            this.button7.TabIndex = 15;
            this.button7.Text = "Struct of arrays !!";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(140, 382);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(281, 39);
            this.label10.TabIndex = 18;
            this.label10.Text = "Generate request with method call and struct parameter.\r\nThe struct will contain " +
    "2 structs each one contain normal \r\nvalues.";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 380);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(122, 41);
            this.button8.TabIndex = 17;
            this.button8.Text = "Struct of structs !!";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // Form_XmlRpcTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 450);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.richTextBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form_XmlRpcTest";
            this.Text = "XML-RPC Generator Test";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button button8;
    }
}