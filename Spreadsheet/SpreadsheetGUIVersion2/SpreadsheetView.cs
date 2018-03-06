using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SSGui;

namespace SpreadsheetGUIVersion2
{
    public interface SpreadsheetView
    {

        event Action NewEvent;

        event Action CloseEvent;

        event Action ChangeButtonEvent;

        event Action GridEvent;


        void DoClose();

        void OpenNew();

        void displayCellTextBoxes(string currentCell);

        void displaySelection(SpreadsheetPanel sender);
        
    }
}
