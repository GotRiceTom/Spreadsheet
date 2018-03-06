using System;
using System.Windows.Forms;


namespace SpreadsheetGUIVersion2
{
    public partial class Spreadsheet_V2 : Form, SpreadsheetView
    {


        public Spreadsheet_V2()
        {
            InitializeComponent();
           
        }


        public event Action NewEvent;

        public event Action CloseEvent;
      

        public void DoClose()
        {
            Close();
        }

        public void OpenNew()
        {
            
            SpreadsheetContext.GetContext().RunNew();
        }

        private void NewSpreadsheet_Click(object sender, EventArgs e)
        {

            if (NewEvent != null)
            {
                NewEvent();
            }
            
        }

        private void CloseSpreadsheet_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent() ;
            }
            
        }

        


    }
}
