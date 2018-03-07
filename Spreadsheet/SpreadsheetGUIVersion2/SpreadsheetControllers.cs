using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public class SpreadsheetControllers
    {


        private SpreadsheetView window;

        private Spreadsheet MainSpreadsheet;

        private SpreadsheetPanel panel;

      public SpreadsheetControllers(SpreadsheetView ViewInput)
        {
            
            this.window = ViewInput;

            MainSpreadsheet = new Spreadsheet();

            ViewInput.NewEvent += HandleNewWindow;
            ViewInput.CloseEvent += HandleCloseWindow;
            ViewInput.SelectionChangeEvent += HandleDisplaySelection;
            ViewInput.ChangeButtonEvent += HandleChangeButton;

        }

        private void HandleNewWindow()
        {
            window.OpenNew();
        }

        private void HandleCloseWindow()
        {
            window.DoClose();
        }

        private void HandleDisplaySelection(SpreadsheetPanel sender)
        {
            panel = sender;
            sender.GetSelection(out int col, out int row);

            //Get Letter of col
            string colLetter = columLetters(col);

            //combine colLetter and row to get the cell name
            string currentCellNamed = colLetter + "" + (row + 1);

            window.displaySelection(sender, currentCellNamed, MainSpreadsheet.GetCellContents(currentCellNamed), MainSpreadsheet.GetCellValue(currentCellNamed) );
        }

        private void HandleChangeButton(string cellEditContent)
        {
            panel.GetSelection(out int col, out int row);
            //Get Letter of col
            string colLetter = columLetters(col);

            //combine colLetter and row to get the cell name
            string currentCellNamed = colLetter + "" + (row + 1);

            //set content
            MainSpreadsheet.SetContentsOfCell(currentCellNamed, cellEditContent);

            // callback
            window.displayValueOnPanel(panel, MainSpreadsheet.GetCellContents(currentCellNamed), MainSpreadsheet.GetCellValue(currentCellNamed));

            
            
        }




        private string columLetters(int col)
        {


            String[] AtoZ = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };


            return AtoZ[col];
        }



        private int columNumber(String col)
        {
            String[] AtoZ = new String[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            for (int i = 0; i < AtoZ.Length; i++)
            {
                if (AtoZ[i] == col.ToUpper())
                {
                    return i;
                }
            }
            return 0;
        }


    }
}
