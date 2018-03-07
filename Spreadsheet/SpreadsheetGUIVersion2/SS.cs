using System;
using System.Windows.Forms;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public partial class Spreadsheet_V2 : Form, SpreadsheetView
    {

       

        public Spreadsheet_V2()
        {
            InitializeComponent();
            setCellTextBoxToReadonly();

        }


        public event Action NewEvent;

        public event Action CloseEvent;

        public event Action<string> ChangeButtonEvent;

        public event Action<SpreadsheetPanel> SelectionChangeEvent;
        

        /// <summary>
        /// Close the App
        /// </summary>
        public void DoClose()
        {
            Close();
        }

        /// <summary>
        /// Open a new app of Spreadsheet
        /// </summary>
        public void OpenNew()
        {
            
            SpreadsheetContext.GetContext().RunNew();
        }

        /// <summary>
        /// Display the current cell value when change button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="content"></param>
        /// <param name="value"></param>
        public void displayValueOnPanel(SpreadsheetPanel sender, Object content, Object value)
        {
           // get coords
            sender.GetSelection(out int col, out int row);

            // set value at coords
            sender.SetValue(col, row, value.ToString());

            //Display the current cell's information
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

        

        /// <summary>
        /// Fired up the NewEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewSpreadsheet_Click(object sender, EventArgs e)
        {

            if (NewEvent != null)
            {
                NewEvent();
            }
            
        }

        /// <summary>
        /// Fired up the CloseEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseSpreadsheet_Click(object sender, EventArgs e)
        {
            if (CloseEvent != null)
            {
                CloseEvent() ;
            }
            
        }

        /// <summary>
        /// Fired up the SelectionChangeEvent
        /// </summary>
        /// <param name="sender"></param>
        private void display_selectionchange(SpreadsheetPanel sender)
        {

            if (SelectionChangeEvent != null)
            {
 
                SelectionChangeEvent(sender);
            }
        }

        /// <summary>
        /// Fired up the ChangeButtonEvent
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

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
