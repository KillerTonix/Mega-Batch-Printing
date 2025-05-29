using System.Drawing;
using System.Drawing.Printing;
using System.IO;

namespace Mega_Batch_Printing.Helpers
{
    public static class TxtPrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            string content = File.ReadAllText(filePath);
            using PrintDocument pd = new();
            if (!string.IsNullOrEmpty(printerName))
                pd.PrinterSettings.PrinterName = printerName;

            pd.PrintPage += (sender, e) =>
            {
                e.Graphics?.DrawString(content, new Font("Consolas", 10), Brushes.Black, e.MarginBounds);
            };

            pd.Print();
        }
    }
}
