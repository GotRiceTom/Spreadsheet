using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SSGui;

namespace SpreadsheetGUIVersion2
{
    public partial class Spreadsheet_V2 : Form, SpreadsheetView
    {

        private int row, col;

        public Spreadsheet_V2()
        {
            InitializeComponent();
        }

        public event Action NewEvent;
        public event Action CloseEvent;
        public event Action ChangeButtonEvent;
        public event Action GridEvent;

        public void displayCellTextBoxes(string currentCell)
        {
            throw new NotImplementedException();
        }

        public void displaySelection(SpreadsheetPanel sender)
        {
          
        }

        public void DoClose()
        {
            Close();
        }

        public void OpenNew()
        {
            SpreadsheetContext.GetContext().RunNew();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (NewEvent != null)
            {
                NewEvent();
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent();
            }
        }

        private void spreadsheetPanel1_Load(object sender, EventArgs e)
        {
            if (GridEvent != null)
            {
                GridEvent();
            }
        }

        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (ChangeButtonEvent != null)
            {
                ChangeButtonEvent();
            }

        }
    }
}
