/* This file is part of OpenSubtitles Handler
   A library that handle OpenSubtitles.org XML-RPC methods.

   Copyright © Ala Ibrahim Hadid 2013

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
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using XmlRpcHandler;
namespace OpenSubtitlesHandlerTest
{
    public partial class Form_XmlRpcTest : Form
    {
        public Form_XmlRpcTest()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            parms.Add(new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String));
            parms.Add(new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String));
            parms.Add(new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int));
            parms.Add(new XmlRpcValueBasic(33.33, XmlRpcBasicValueType.Double));
            parms.Add(new XmlRpcValueBasic(DateTime.Now, XmlRpcBasicValueType.dateTime_iso8601));
            parms.Add(new XmlRpcValueBasic(1233356434343, XmlRpcBasicValueType.base64));
            parms.Add(new XmlRpcValueBasic(true, XmlRpcBasicValueType.Boolean));
            parms.Add(new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean));
            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);

            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define struct
            // Add members
            List<XmlRpcStructMember> members = new List<XmlRpcStructMember>();
            members.Add(new XmlRpcStructMember("Test member 1",
                new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 2",
                new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 3",
                new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int)));
            members.Add(new XmlRpcStructMember("Test member 4",
                new XmlRpcValueBasic(5.666, XmlRpcBasicValueType.Double)));
            XmlRpcValueStruct strct = new XmlRpcValueStruct(members);
            // Add struct to parameters list
            parms.Add(strct);
            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define array
            XmlRpcValueArray array = new XmlRpcValueArray();
            array.Values = new List<IXmlRpcValue>();
            array.Values.Add(new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String));
            array.Values.Add(new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String));
            array.Values.Add(new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int));
            array.Values.Add(new XmlRpcValueBasic(33.33, XmlRpcBasicValueType.Double));
            array.Values.Add(new XmlRpcValueBasic(DateTime.Now, XmlRpcBasicValueType.dateTime_iso8601));
            array.Values.Add(new XmlRpcValueBasic(1233356434343, XmlRpcBasicValueType.base64));
            array.Values.Add(new XmlRpcValueBasic(true, XmlRpcBasicValueType.Boolean));
            array.Values.Add(new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean));
            // Add array to parameters list
            parms.Add(array);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define array
            XmlRpcValueArray array = new XmlRpcValueArray();
            array.Values = new List<IXmlRpcValue>();
            // Define struct 1
            // Add members
            List<XmlRpcStructMember> members = new List<XmlRpcStructMember>();
            members.Add(new XmlRpcStructMember("Test member 1",
                new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 2",
                new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 3",
                new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int)));
            members.Add(new XmlRpcStructMember("Test member 4",
                new XmlRpcValueBasic(5.666, XmlRpcBasicValueType.Double)));
            XmlRpcValueStruct strct = new XmlRpcValueStruct(members);
            // Add struct to the array
            array.Values.Add(strct);
            // Define struct 2
            // Add members
            members = new List<XmlRpcStructMember>();
            members.Add(new XmlRpcStructMember("Test member 5",
                new XmlRpcValueBasic("string test3", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 6",
                new XmlRpcValueBasic("string test4", XmlRpcBasicValueType.String)));
            members.Add(new XmlRpcStructMember("Test member 7",
                new XmlRpcValueBasic(545, XmlRpcBasicValueType.Int)));
            members.Add(new XmlRpcStructMember("Test member 8",
                new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean)));
            strct = new XmlRpcValueStruct(members);
            // Add struct to the array
            array.Values.Add(strct);

            // Add array to parameters list
            parms.Add(array);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define array
            XmlRpcValueArray array = new XmlRpcValueArray();
            array.Values = new List<IXmlRpcValue>();

            // Define sub array 1
            XmlRpcValueArray subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int));
            // Add subarray 1 to array
            array.Values.Add(subarray);

            // Define sub array 2
            subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic(33.33, XmlRpcBasicValueType.Double));
            subarray.Values.Add(new XmlRpcValueBasic(DateTime.Now, XmlRpcBasicValueType.dateTime_iso8601));
            subarray.Values.Add(new XmlRpcValueBasic(1233356434343, XmlRpcBasicValueType.base64));
            subarray.Values.Add(new XmlRpcValueBasic(true, XmlRpcBasicValueType.Boolean));
            subarray.Values.Add(new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean));
            // Add subarray 2 to array
            array.Values.Add(subarray);

            // Add array to parameters list
            parms.Add(array);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button6_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define array
            XmlRpcValueArray array = new XmlRpcValueArray();
            array.Values = new List<IXmlRpcValue>();

            // Define struct 1
            // Add members
            List<XmlRpcStructMember> members = new List<XmlRpcStructMember>();

            // Define sub array 1
            XmlRpcValueArray subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int));
            // Add subarray 1 to member
            members.Add(new XmlRpcStructMember("Test member 1", subarray));
            // Add member to struct
            XmlRpcValueStruct strct = new XmlRpcValueStruct(members);
            // Add struct to the array
            array.Values.Add(strct);

            // Define struct 2
            // Add members
            members = new List<XmlRpcStructMember>();
            // Define sub array 2
            subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic(33.33, XmlRpcBasicValueType.Double));
            subarray.Values.Add(new XmlRpcValueBasic(DateTime.Now, XmlRpcBasicValueType.dateTime_iso8601));
            subarray.Values.Add(new XmlRpcValueBasic(1233356434343, XmlRpcBasicValueType.base64));
            subarray.Values.Add(new XmlRpcValueBasic(true, XmlRpcBasicValueType.Boolean));
            subarray.Values.Add(new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean));
            // Add subarray 2 to struct
            members.Add(new XmlRpcStructMember("Test member 5", subarray));
            strct = new XmlRpcValueStruct(members);
            // Add struct to the array
            array.Values.Add(strct);

            // Add array to parameters list
            parms.Add(array);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button7_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define struct
            // Add members
            List<XmlRpcStructMember> members = new List<XmlRpcStructMember>();

            // Define sub array 1
            XmlRpcValueArray subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String));
            subarray.Values.Add(new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int));
            // Add subarray 1 to member
            members.Add(new XmlRpcStructMember("Test member 1", subarray));

            // Define sub array 2
            subarray = new XmlRpcValueArray();
            subarray.Values = new List<IXmlRpcValue>();
            subarray.Values.Add(new XmlRpcValueBasic(33.33, XmlRpcBasicValueType.Double));
            subarray.Values.Add(new XmlRpcValueBasic(DateTime.Now, XmlRpcBasicValueType.dateTime_iso8601));
            subarray.Values.Add(new XmlRpcValueBasic(1233356434343, XmlRpcBasicValueType.base64));
            subarray.Values.Add(new XmlRpcValueBasic(true, XmlRpcBasicValueType.Boolean));
            subarray.Values.Add(new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean));
            // Add subarray 2 to struct
            members.Add(new XmlRpcStructMember("Test member 2", subarray));

            // Add members to struct
            XmlRpcValueStruct strct = new XmlRpcValueStruct(members);

            // Add array to parameters list
            parms.Add(strct);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button8_Click(object sender, EventArgs e)
        {
            List<IXmlRpcValue> parms = new List<IXmlRpcValue>();
            // Define struct
            XmlRpcValueStruct strct = new XmlRpcValueStruct(new List<XmlRpcStructMember>());

            // Define struct 1
            // Add members
            List<XmlRpcStructMember> submembers = new List<XmlRpcStructMember>();
            submembers.Add(new XmlRpcStructMember("Test member 1",
                new XmlRpcValueBasic("string test1", XmlRpcBasicValueType.String)));
            submembers.Add(new XmlRpcStructMember("Test member 2",
                new XmlRpcValueBasic("string test2", XmlRpcBasicValueType.String)));
            submembers.Add(new XmlRpcStructMember("Test member 3",
                new XmlRpcValueBasic(5, XmlRpcBasicValueType.Int)));
            submembers.Add(new XmlRpcStructMember("Test member 4",
                new XmlRpcValueBasic(5.666, XmlRpcBasicValueType.Double)));
            XmlRpcValueStruct substrct = new XmlRpcValueStruct(submembers);
            // Add struct 1 to the struct
            strct.Members.Add(new XmlRpcStructMember("Sub Struct 1", substrct));
            // Define struct 2
            // Add members
            submembers = new List<XmlRpcStructMember>();
            submembers.Add(new XmlRpcStructMember("Test member 5",
                new XmlRpcValueBasic("string test3", XmlRpcBasicValueType.String)));
            submembers.Add(new XmlRpcStructMember("Test member 6",
                new XmlRpcValueBasic("string test4", XmlRpcBasicValueType.String)));
            submembers.Add(new XmlRpcStructMember("Test member 7",
                new XmlRpcValueBasic(545, XmlRpcBasicValueType.Int)));
            submembers.Add(new XmlRpcStructMember("Test member 8",
                new XmlRpcValueBasic(false, XmlRpcBasicValueType.Boolean)));
            substrct = new XmlRpcValueStruct(submembers);
            // Add struct 2 to the array
            strct.Members.Add(new XmlRpcStructMember("Sub Struct 2", substrct));

            // Add struct to parameters list
            parms.Add(strct);

            XmlRpcMethodCall call = new XmlRpcMethodCall("Test", parms);
            richTextBox1.Text = Encoding.ASCII.GetString(XmlRpcGenerator.Generate(call));
        }

        private void button9_Click(object sender, EventArgs e)
        {
            XmlRpcMethodCall[] calls = XmlRpcGenerator.DecodeMethodResponse(richTextBox1.Text);
            return;
        }
    }
}
