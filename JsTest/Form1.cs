using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace JsTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            wbEncryption.DocumentText = JsTest.Properties.Resources.Encryption;
            wbEncryption.DocumentCompleted += wbEncryption_DocumentCompleted;
        }

        void wbEncryption_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var ss = this.wbEncryption.Document.InvokeScript("test1");
            Console.WriteLine(ss);
        }
    }
}
