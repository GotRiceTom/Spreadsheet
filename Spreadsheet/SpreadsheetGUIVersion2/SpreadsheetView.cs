using System;
using System.Windows.Forms;
using SSGui;


namespace SpreadsheetGUIVersion2
{
    public interface SpreadsheetView
    {

        event Action NewEvent;

        event Action CloseEvent;

        event Action SaveEvent;

        event Action OpenEvent;

        event Action FormClosingEvent;

        event Action<string> ChangeButtonEvent;

        event Action <Keys> KeyArrowsEvent;

        event Action<SpreadsheetPanel> LoadPanelEvent;

        

        

        event Action<SpreadsheetPanel> SelectionChangeEvent;


        void DoClose();

        void OpenNew();


         void DisplaySelection(string cellName, Object content, Object value);


         void DisplayValueOnPanel(int col, int row, Object content, Object value);

        void DialogBoxOFNoCellIsSelected();

        void DialogBoxFormulaFormat();

        void DialogBoxCircular();
    }
}
