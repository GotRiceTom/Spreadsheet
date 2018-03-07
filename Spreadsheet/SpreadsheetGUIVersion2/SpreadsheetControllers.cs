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

            MainSpreadsheet.SetContentsOfCell(currentCellNamed, cellEditContent);

            window.displayValueOnPanel(panel, MainSpreadsheet.GetCellContents(currentCellNamed), MainSpreadsheet.GetCellValue(currentCellNamed));
        }




        private string columLetters(int col)
        {
            switch (col)
            {

                case 0:
                    return "A";

                case 1:
                    return "B";

                case 2:
                    return "C";

                case 3:
                    return "D";

                case 4:
                    return "E";

                case 5:
                    return "F";

                case 6:
                    return "G";

                case 7:
                    return "H";

                case 8:
                    return "I";

                case 9:
                    return "J";

                case 10:
                    return "K";

                case 11:
                    return "L";

                case 12:
                    return "M";

                case 13:
                    return "N";

                case 14:
                    return "O";

                case 15:
                    return "P";

                case 16:
                    return "Q";

                case 17:
                    return "R";

                case 18:
                    return "S";

                case 19:
                    return "T";

                case 20:
                    return "U";

                case 21:
                    return "V";

                case 22:
                    return "W";

                case 23:
                    return "X";

                case 24:
                    return "Y";

                case 25:
                    return "Z";

            }

            return "";
        }


    }
}
