using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BimGo.Revit.Ribbon
{



    public partial class Form1 : System.Windows.Forms.Form
    {
        private UIApplication uiapp;
        private UIDocument uidoc;
        private Autodesk.Revit.ApplicationServices.Application app;
        private Document doc;

        public double wallDisplacement;

        public Form1(ExternalCommandData commandData)
        {
            InitializeComponent();

            uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            app = uiapp.Application;
            doc = uidoc.Document;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            wallDisplacement = Convert.ToDouble(WallDisplacementTextBox.Text);

            okButton.DialogResult = DialogResult.OK;
            Debug.WriteLine("ok button was clicked");
            Close();

            return;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cancelButton.DialogResult = DialogResult.Cancel;
            Debug.WriteLine("cancel button was clicked");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
