using System;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public interface SpreadsheetView
    {

        event Action NewEvent;

        event Action CloseEvent;

        event Action<string> ChangeButtonEvent;

        event Action<SpreadsheetPanel> SelectionChangeEvent;


        void DoClose();

        void OpenNew();


        void displaySelection(SpreadsheetPanel sender, string cellName, Object content, Object value);


        void displayValueOnPanel(SpreadsheetPanel sender, Object content, Object value);

        // a method to recalucate all the cells value on the grid panel


    }
}
