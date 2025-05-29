using Word = Microsoft.Office.Interop.Word;


namespace Mega_Batch_Printing.Helpers
{
    public static class WordPrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            var app = new Word.Application();
            try
            {
                var doc = app.Documents.Open(filePath);
                app.ActivePrinter = printerName ?? app.ActivePrinter;
                doc.PrintOut();
                doc.Close(false);
            }
            finally
            {
                app.Quit();
            }
        }
    }
}
