using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Formulas;
using Dependencies;
using SS;

namespace SpreadsheetGUI
{
	public partial class SpreadsheetGUI : Form
	{


		private Spreadsheet mainSpreadsheet;

		public SpreadsheetGUI()
		{
			InitializeComponent();
			setCellTextBoxToReadonly();

			mainSpreadsheet = new Spreadsheet();

		
		}

		private void SpreadsheetPanel_Load(object sender, EventArgs e)
		{

		}

		private void textBox2_TextChanged(object sender, EventArgs e)
		{

		}

		private void label1_Click(object sender, EventArgs e)
		{

		}

		private void SpreadsheetGUI_Load(object sender, EventArgs e)
		{

		}

		private void menuToolStripMenuItem_Click(object sender, EventArgs e)
		{
			
		}

		/// <summary>
		/// This method for listen for close button.
		/// This will close the current GUI program
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CloseItem_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void panel3_Paint(object sender, PaintEventArgs e)
		{

		}

		private void CellNameTextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void CellContentTextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void CellValueTextBox_TextChanged(object sender, EventArgs e)
		{

		}

		private void Panel2_Paint(object sender, PaintEventArgs e)
		{

		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{

		}


		private void contentEdit_EnterKeyDown(object sender, PaintEventArgs e)
		{

		}

		/// <summary>
		/// Set 
		/// </summary>
		private void setCellTextBoxToReadonly()
		{
			//set the text box to be read only
			CellNameTextBox.ReadOnly = true;
			CellContentTextBox.ReadOnly = true;
			CellValueTextBox.ReadOnly = true;

			// set the text boxes to be gray
			CellNameTextBox.BackColor = System.Drawing.SystemColors.Window;
			CellContentTextBox.BackColor = System.Drawing.SystemColors.Window;
			CellValueTextBox.BackColor = System.Drawing.SystemColors.Window;

		}

		private void ContentEditBox_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				
				Close();
			}
			
		}


		/// <summary>
		/// Every time the selection changes, this method is called with the
		/// Spreadsheet as its parameter.  We display the current time in the cell.
		/// </summary>
		private void displaySelection(SSGui.SpreadsheetPanel sender)
		{
			int row, col;
			
			sender.GetSelection(out col, out row);

			//call this method
			displayCellTextBoxes(col, row);
			
		}

		private void displayCellTextBoxes(int col, int row)
		{
			CellNameTextBox.Text = col + " " + row;
		}
	}
}
