using System;
using System.Windows.Forms;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public partial class Spreadsheet_V2 : Form, SpreadsheetView
    {

        private SpreadsheetPanel MainSheetPanel;

        public Spreadsheet_V2()
        {
            InitializeComponent();
            setCellTextBoxToReadonly();
            MainSheetPanel = new SpreadsheetPanel();
          
        }


        public event Action NewEvent;

        public event Action CloseEvent;

        public event Action<string> ChangeButtonEvent;

        public event Action<SpreadsheetPanel> SelectionChangeEvent;
        

        public void DoClose()
        {
            Close();
        }

        public void OpenNew()
        {
            
            SpreadsheetContext.GetContext().RunNew();
        }

        public void displayValueOnPanel(SpreadsheetPanel sender, Object content, Object value)
        {
           
            sender.GetSelection(out int col, out int row);
            sender.SetValue(col, row, value.ToString());
            CellContentText.Text = content.ToString();
            CellValueText.Text = value.ToString();
        }

        public void displaySelection(SpreadsheetPanel sender, string cellNamed, Object cellContent, Object cellValue)
        {
            ContentEditTextBox.Text = cellContent.ToString();

            CellNameText.Text = cellNamed;
            CellContentText.Text = cellContent.ToString();
            CellValueText.Text = cellValue.ToString();
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

        private void display_selectionchange(SpreadsheetPanel sender)
        {

            if (SelectionChangeEvent != null)
            {
 
                SelectionChangeEvent(sender);
            }
        }


        private void ChangeButton_Click(object sender, EventArgs e)
        {
            if (ChangeButtonEvent != null)
            {
                ChangeButtonEvent(ContentEditTextBox.Text);
               
            }
        }

        private void setCellTextBoxToReadonly()
        {
            //set the text box to be read only
            CellNameText.ReadOnly = true;
            CellContentText.ReadOnly = true;
            CellValueText.ReadOnly = true;

            // set the text boxes to be gray
            CellNameText.BackColor = System.Drawing.SystemColors.Window;
            CellContentText.BackColor = System.Drawing.SystemColors.Window;
            CellValueText.BackColor = System.Drawing.SystemColors.Window;

        }

      
    }
}
