using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SS;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public class SpreadsheetControllers
    {


        private SpreadsheetView window;

        private Spreadsheet MainSpreadsheet;

        private SpreadsheetPanel panel;


        public SpreadsheetControllers(SpreadsheetView ViewInput, String filePath, SpreadsheetPanel sender)
        {

            this.window = ViewInput;

            if (filePath != null && sender != null)
            {

                panel = sender;

                using (System.IO.TextReader readFile = new StreamReader(filePath))
                {
                    Regex reg = new Regex(@"^[A-z]+[1-9][0-9]*$");

                    MainSpreadsheet = new Spreadsheet(readFile, reg);

                    ///iterate
                    foreach (string currentName in MainSpreadsheet.GetNamesOfAllNonemptyCells())
                    {
                        int colNum = columNumber(currentName[0].ToString());
                        int rowNum = Int32.Parse(currentName.Substring(1));


                        panel.SetValue(colNum, rowNum - 1, MainSpreadsheet.GetCellValue(currentName).ToString());
                    }

                }

            }
            else
            {
                MainSpreadsheet = new Spreadsheet();
            }


          

            ViewInput.NewEvent += HandleNewWindow;
            ViewInput.CloseEvent += HandleCloseWindow;
            ViewInput.SelectionChangeEvent += HandleDisplaySelection;
            ViewInput.ChangeButtonEvent += HandleChangeButton;
            ViewInput.SaveEvent += HandleSave;
            ViewInput.FormClosingEvent += HandleSave;
            ViewInput.KeyArrowsEvent += HandleKeysArrow;
            ViewInput.OpenEvent += HandleOpen;

        }

        private void HandleOpen()
        {
            OpenFileDialog openBox = new OpenFileDialog();


            openBox.Filter = "Spreadsheet File |* .ss";

            if (openBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {


                string path = openBox.FileName;

                SpreadsheetContext.GetContext().RunNew(path, panel);


            }
        }

        private void HandleKeysArrow(Keys obj)
        {
            if (panel != null) {
                panel.GetSelection(out int col, out int row);

                if (obj == Keys.Left)
                {
                    if (col != 0)
                    {
                        panel.SetSelection(col - 1, row);
                        HandleDisplaySelection(panel);
                    }

                }

                if (obj == Keys.Up)
                {
                    if (row != 0)
                    {
                        panel.SetSelection(col, row - 1);
                        HandleDisplaySelection(panel);
                    }

                }

                if (obj == Keys.Right)
                {
                    if (col != 25)
                    {
                        panel.SetSelection(col + 1, row);
                        HandleDisplaySelection(panel);

                    }

                }

                if (obj == Keys.Down)
                {
                    if (row != 98)
                    {
                        panel.SetSelection(col, row + 1);
                        HandleDisplaySelection(panel);
                    }

                }

            }



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

            window.DisplaySelection(currentCellNamed, MainSpreadsheet.GetCellContents(currentCellNamed), MainSpreadsheet.GetCellValue(currentCellNamed));
        }

        private void HandleChangeButton(string cellEditContent)
        {
            if (panel != null)
            {
                panel.GetSelection(out int col, out int row);

                //Get Letter of col
                string colLetter = columLetters(col);

                //combine colLetter and row to get the cell name
                string currentCellNamed = colLetter + "" + (row + 1);

                //set content
                try
                {
                    //try adding the content to the spreadsheet
                    // then get it's dependents
                    HashSet<String> cellList = new HashSet<string>(MainSpreadsheet.SetContentsOfCell(currentCellNamed, cellEditContent));


                    panel.SetValue(col, row, MainSpreadsheet.GetCellValue(currentCellNamed).ToString());

                    foreach (string name in cellList)
                    {
                        if (currentCellNamed != name)
                        {
                            int colNum = columNumber(name[0].ToString());
                            int rowNum = Int32.Parse(name.Substring(1));


                            panel.SetValue(colNum, rowNum - 1, MainSpreadsheet.GetCellValue(name).ToString());
                        }
                    }

                    // display the top- right content box
                    window.DisplaySelection(currentCellNamed, MainSpreadsheet.GetCellContents(currentCellNamed), MainSpreadsheet.GetCellValue(currentCellNamed));
                }

                catch (Formulas.FormulaFormatException)
                {
                    window.DialogBoxFormulaFormat();
                }

                catch (CircularException)
                {
                    window.DialogBoxCircular();
                }

            }

            else
            {
                window.DialogBoxOFNoCellIsSelected();
            }
        }



        private void HandleSave()
        {

            if (MainSpreadsheet.Changed) { 
            SaveFileDialog sfd = new SaveFileDialog();


            sfd.Filter = "Spreadsheet File |* .ss";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {



                string path = sfd.FileName;

                using (System.IO.TextWriter writeFile = new StreamWriter(path))
                {
                    MainSpreadsheet.Save(writeFile);
                }


            }
         }
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
