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
using Formulas;


namespace SpreadsheetGUI
{
    public class SpreadsheetControllers
    {


        private SpreadsheetView window;

        private Spreadsheet MainSpreadsheet;

        private SpreadsheetPanel panel;

        private string FilePath;

        public Boolean ReachedSaved { get; private set; }


        public SpreadsheetControllers(SpreadsheetView ViewInput, String filePath)
        {
            //for code coverage purpose
            ReachedSaved = false;

            this.window = ViewInput;

            if (filePath != null )
            {


                using (System.IO.TextReader readFile = new StreamReader(filePath))
                {
                    Regex reg = new Regex(@"^[A-z]+[1-9][0-9]*$");

                    MainSpreadsheet = new Spreadsheet(readFile, reg);

                    ///iterate
                    foreach (string currentName in MainSpreadsheet.GetNamesOfAllNonemptyCells())
                    {
                        int colNum = columNumber(currentName[0].ToString());
                        int rowNum = Int32.Parse(currentName.Substring(1));

                        window.DisplayValueOnPanel(colNum, rowNum - 1, MainSpreadsheet.GetCellContents(currentName), MainSpreadsheet.GetCellValue(currentName));
                    }

                }

            }
            else
            {
                MainSpreadsheet = new Spreadsheet();
            }

            panel = new SpreadsheetPanel();


            ViewInput.NewEvent += HandleNewWindow;
            ViewInput.CloseEvent += HandleCloseWindow;
            ViewInput.SelectionChangeEvent += HandleDisplaySelection;
            ViewInput.ChangeButtonEvent += HandleChangeButton;
            ViewInput.SaveToEvent += HandleSaveTo;
            ViewInput.FormClosingEvent += HandleSaveTo;
            ViewInput.KeyArrowsEvent += HandleKeysArrow;
            ViewInput.OpenEvent += HandleOpen;
            ViewInput.SaveEvent += HandleSave;
            ViewInput.HelpEvent += HandleHelp;
            

        }




        private void HandleHelp()
        {
            window.openHelp();
        }

        private void HandleOpen()
        {
            ReachedSaved = true;
            OpenFileDialog openBox = new OpenFileDialog();


            openBox.Filter = "Spreadsheet File (.ss) |* .ss| Text file (.txt) |* .txt|All files (*.*)|*.* ";

            if (openBox.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {


                string path = openBox.FileName;

                FilePath = path;

                SpreadsheetContext.GetContext().RunNew(path);

            }
        }

        private void HandleKeysArrow(Keys obj)
        {
            if (panel != null) {
                panel.GetSelection(out int col, out int row);

                if (obj == Keys.Left)
                {
                    ReachedSaved = true;

                    if (col != 0)
                    {
                        panel.SetSelection(col - 1, row);
                        HandleDisplaySelection(panel);
                    }

                }

                if (obj == Keys.Up)
                {
                    ReachedSaved = true;

                    if (row != 0)
                    {
                        panel.SetSelection(col, row - 1);
                        HandleDisplaySelection(panel);
                    }

                }

                if (obj == Keys.Right)
                {
                    ReachedSaved = true;

                    if (col != 25)
                    {
                        panel.SetSelection(col + 1, row);
                        HandleDisplaySelection(panel);

                    }

                }

                if (obj == Keys.Down)
                {
                    ReachedSaved = true;

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
            string temp = "="; 

            panel = sender;

            panel.GetSelection(out int col, out int row);

            //Get Letter of col
            string colLetter = columLetters(col);

            //combine colLetter and row to get the cell name
            string currentCellNamed = colLetter + "" + (row + 1);

            if (MainSpreadsheet.GetCellContents(currentCellNamed) is Formula)
            {
                temp += MainSpreadsheet.GetCellContents(currentCellNamed).ToString();

                window.DisplaySelection(currentCellNamed, temp, MainSpreadsheet.GetCellValue(currentCellNamed));
            }
            else
            {
                window.DisplaySelection(currentCellNamed, MainSpreadsheet.GetCellContents(currentCellNamed).ToString(), MainSpreadsheet.GetCellValue(currentCellNamed));
            }
           
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
                    window.DisplaySelection(currentCellNamed, cellEditContent, MainSpreadsheet.GetCellValue(currentCellNamed));
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



        private void HandleSaveTo()
        {
           
            ReachedSaved = true;

            SaveFileDialog sfd = new SaveFileDialog();


            sfd.Filter = "Spreadsheet File (.ss) |* .ss| Text file (.txt) |* .txt|All files (*.*)|*.* ";

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {



                string path = sfd.FileName;

                FilePath = path;

                using (System.IO.TextWriter writeFile = new StreamWriter(path))
                {
                    MainSpreadsheet.Save(writeFile);
                }



            }
        
        
         
 }



        private void HandleSave()
        {
            
            SaveFileDialog sfd = new SaveFileDialog();

            ReachedSaved = true;

            sfd.Filter = "Spreadsheet File (.ss) |* .ss| Text file (.txt) |* .txt|All files (*.*)|*.* ";

            if (FilePath == null)
            {
                HandleSaveTo();
            }
            else
            {
                using (System.IO.TextWriter writeFile = new StreamWriter(FilePath))
                {
                    MainSpreadsheet.Save(writeFile);
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
