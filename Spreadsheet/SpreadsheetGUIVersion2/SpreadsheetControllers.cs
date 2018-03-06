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

        private Spreadsheet mainSS;

      public SpreadsheetControllers(SpreadsheetView window)
        {
            this.window = window;
            this.mainSS = new Spreadsheet();

            window.NewEvent += HandleNewWindow;
            window.CloseEvent += HandleCloseWindow;
            window.ChangeButtonEvent += HandleChangeButton;

        }

        private void Window_GridEvent()
        {
            throw new NotImplementedException();
        }

        private void HandleNewWindow()
        {
            window.OpenNew();
        }

        private void HandleCloseWindow()
        {
            window.DoClose();
        }

        private void HandleChangeButton()
        {

        }

        private void HandleDisplaySelection(SpreadsheetPanel sender)
        {

        }

    }
}
