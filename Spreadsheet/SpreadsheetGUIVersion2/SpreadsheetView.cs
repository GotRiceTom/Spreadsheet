using System;


namespace SpreadsheetGUIVersion2
{
    public interface SpreadsheetView
    {

        event Action NewEvent;

        event Action CloseEvent;

        void DoClose();

        void OpenNew();

        
    }
}
