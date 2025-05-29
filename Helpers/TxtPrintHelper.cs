using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Mega_Batch_Printing.Helpers
{
    public static class TxtPrintHelper
    {
        private static StringReader? _stringReader;
        private static string content = string.Empty;
        public static void Print(string filePath, string? printerName = null)
        {
            content = File.ReadAllText(filePath);
            _stringReader = new StringReader(content);

            using PrintDocument pd = new();
            if (!string.IsNullOrEmpty(printerName))
            {
                pd.PrinterSettings.PrinterName = printerName;
            }
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40); // left, right, top, bottom
            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 size in hundredths of an inch

            pd.PrintPage += PrintPageHandler;
            pd.EndPrint += (s, e) => _stringReader?.Dispose();

            pd.Print();
        }

        private static void PrintPageHandler(object? sender, PrintPageEventArgs e)
        {
            if (_stringReader == null)
                return;

            Font font = new("Consolas", 10);
            float leftMargin = e.MarginBounds.Left;
            float topMargin = e.MarginBounds.Top;
            float yPos = topMargin;
            float lineHeight = font.GetHeight(e.Graphics);

            // Calculate the number of lines per page
            int linesPerPage = (int)(e.MarginBounds.Height / lineHeight);

            int linesPrinted = 0;
            string? line;

            // Print lines for this page
            while (linesPrinted < linesPerPage && (line = _stringReader.ReadLine()) != null)
            {
                e.Graphics.DrawString(line, font, Brushes.Black, leftMargin, yPos);
                linesPrinted++;
                yPos += lineHeight;
            }

            // If there are more lines, HasMorePages should be true
            e.HasMorePages = _stringReader.Peek() != -1;
        }
    }
}
