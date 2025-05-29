using PowerPoint = Microsoft.Office.Interop.PowerPoint;


namespace Mega_Batch_Printing.Helpers
{
    public static class PowerPointPrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            var app = new PowerPoint.Application();
            try
            {
                var presentation = app.Presentations.Open(filePath, WithWindow: Microsoft.Office.Core.MsoTriState.msoFalse);
                presentation.PrintOptions.ActivePrinter = printerName ?? presentation.PrintOptions.ActivePrinter;
                presentation.PrintOut();
                presentation.Close();
            }
            finally
            {
                app.Quit();
            }
        }
    }
}
