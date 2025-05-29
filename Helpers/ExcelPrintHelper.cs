using Excel = Microsoft.Office.Interop.Excel;


namespace Mega_Batch_Printing.Helpers
{
    public static class ExcelPrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            var app = new Excel.Application();
            try
            {
                var workbook = app.Workbooks.Open(filePath);
                app.ActivePrinter = printerName ?? app.ActivePrinter;
                workbook.PrintOut();
                workbook.Close(false);
            }
            finally
            {
                app.Quit();
            }
        }
    }
}

