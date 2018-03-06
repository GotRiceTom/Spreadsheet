using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SS;


namespace SpreadsheetGUIVersion2
{
    public class SpreadsheetControllers
    {


        private SpreadsheetView window;

        private Spreadsheet MainSpreadsheet;

      public SpreadsheetControllers(SpreadsheetView ViewInput)
        {
            
            this.window = ViewInput;

            MainSpreadsheet = new Spreadsheet();

            ViewInput.NewEvent += HandleNewWindow;
            ViewInput.CloseEvent += HandleCloseWindow;


        }

        private void HandleNewWindow()
        {
            window.OpenNew();
        }

        private void HandleCloseWindow()
        {
            window.DoClose();
        }


    }
}
