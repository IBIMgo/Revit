using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Okno_Revit
{
    public partial class Form1 : System.Windows.Forms.
    {
        string Dane;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dane = textBox1.Text;
            double Liczba = Int32.Parse(Dane);
 
        }
    }
}
