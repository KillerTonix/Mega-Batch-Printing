using System.Drawing;
using System.Drawing.Printing;

namespace Mega_Batch_Printing.Helpers
{
    public static class ImagePrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            using PrintDocument pd = new();
            if (!string.IsNullOrEmpty(printerName))            
                pd.PrinterSettings.PrinterName = printerName;            
            
            pd.DefaultPageSettings.Landscape = false;
            pd.DefaultPageSettings.Margins = new Margins(40, 40, 40, 40); // left, right, top, bottom
            pd.DefaultPageSettings.PaperSize = new PaperSize("A4", 827, 1169); // A4 size in hundredths of an inch (8.27 x 11.69 inches)
            // Load image to get its size and DPI
            using var image = Image.FromFile(filePath);

            // Convert image size from pixels to hundredths of an inch
            float imageWidthInches = image.Width / image.HorizontalResolution;
            float imageHeightInches = image.Height / image.VerticalResolution;
            int imageWidthHundredths = (int)(imageWidthInches * 100);
            int imageHeightHundredths = (int)(imageHeightInches * 100);

            // Get page size (printable area) in hundredths of an inch
            var pageSettings = pd.DefaultPageSettings;
            int pageWidth = pageSettings.PaperSize.Width - pageSettings.Margins.Left - pageSettings.Margins.Right;
            int pageHeight = pageSettings.PaperSize.Height - pageSettings.Margins.Top - pageSettings.Margins.Bottom;

            // Set landscape if image is wider than page
            if (imageWidthInches > pageWidth)
                pageSettings.Landscape = true;

            pd.PrintPage += (sender, args) =>
            {
                args.Graphics?.DrawImage(image, 40, 40, imageWidthHundredths, imageHeightHundredths);
            };

            pd.Print();
        }
    }
}
