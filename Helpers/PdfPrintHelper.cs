using System.Diagnostics;

namespace Mega_Batch_Printing.Helpers
{
    public static class PdfPrintHelper
    {
        public static void Print(string filePath, string? printerName = null)
        {
            // Simple method using Adobe Reader (make sure it's installed)
            ProcessStartInfo psi = new()
            {
                FileName = @"C:\Program Files\Adobe\Acrobat DC\Acrobat\Acrobat.exe",
                Arguments = $"/s /o /n /h /t \"{filePath}\" \"{printerName ?? ""}\"",
                CreateNoWindow = true,
                UseShellExecute = false
            };
            Process.Start(psi);
        }
    }
}
