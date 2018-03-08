using System;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public interface SpreadsheetView
    {

        event Action NewEvent;

        event Action CloseEvent;

        event Action SaveEvent;

        event Action<string> ChangeButtonEvent;

        

        event Action<SpreadsheetPanel> SelectionChangeEvent;


        void DoClose();

        void OpenNew();


         void DisplaySelection(string cellName, Object content, Object value);


         void DisplayValueOnPanel(SpreadsheetPanel sender, Object content, Object value);

        void DialogBoxOFNoCellIsSelected();

        void DialogBoxFormulaFormat();

        void DialogBoxCircular();
    }
}
