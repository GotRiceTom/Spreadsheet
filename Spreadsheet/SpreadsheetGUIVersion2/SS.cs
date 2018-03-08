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
            SetCellTextBoxToReadonly();

            KeyPreview = true;
        }

        public event Action NewEvent;

        public event Action CloseEvent;

        public event Action SaveEvent;

        public event Action FormClosingEvent;

        public  event Action<Keys> KeyArrowsEvent;

        public event Action<string> ChangeButtonEvent;

        public event Action<SpreadsheetPanel> SelectionChangeEvent;

        public event Action OpenEvent;


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
        public void DisplayValueOnPanel(SpreadsheetPanel sender, Object content, Object value)
        {
            // get coords
            sender.GetSelection(out int col, out int row);

            // set value at coords
            sender.SetValue(col, row, value.ToString());

            //Display the current cell's information
            CellContentText.Text = content.ToString();
            CellValueText.Text = value.ToString();
        }

        public void DisplaySelection(string cellNamed, Object cellContent, Object cellValue)
        {
            //display the content on the contentEditBox
            ContentEditTextBox.Text = cellContent.ToString();

            CellNameText.Text = cellNamed;
            CellContentText.Text = cellContent.ToString();
            CellValueText.Text = cellValue.ToString();
        }


        public void DialogBoxOFNoCellIsSelected()
        {
            MessageBox.Show("Please select a cell to change before attempting to enter data.");
        }



        private void SaveItem_Click(object sender, EventArgs e)
        {
            if(SaveEvent != null){

                SaveEvent();
            }
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
                CloseEvent();
            }
            
        }

        /// <summary>
        /// Fired up the SelectionChangeEvent
        /// </summary>
        /// <param name="sender"></param>
        private void Display_selectionchange(SpreadsheetPanel sender)
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


        private void SetCellTextBoxToReadonly()
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

        public void DialogBoxFormulaFormat()
        {
            MessageBox.Show("That formula is not valid.");
        }

        public void DialogBoxCircular()
        {
            MessageBox.Show("That formula creates a circular error.");
        }

        private void Spreadsheet_V2_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (FormClosingEvent != null)
            {
                FormClosingEvent();
            }
        }

        private void Spreadsheet_V2_KeyDown(object sender, KeyEventArgs e)
        {
            if (KeyArrowsEvent != null)
            {
                KeyArrowsEvent(e.KeyCode);
                e.Handled = true;
            }
           
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (OpenEvent != null)
            {
                OpenEvent();
            }
        }

    }
}
