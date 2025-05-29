using System.Drawing;
using System.Drawing.Printing;

namespace Mega_Batch_Printing.Helpers
{
    public static class ImagePrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            using PrintDocument pd = new();
            if (!string.IsNullOrEmpty(printerName)) {
                pd.PrinterSettings.PrinterName = printerName;
    

            }
            pd.PrintPage += (sender, args) =>
            {

                using var image = Image.FromFile(filePath);
                args.Graphics?.DrawImage(image, args.MarginBounds);
            };

            pd.Print();
        }
    }
}
