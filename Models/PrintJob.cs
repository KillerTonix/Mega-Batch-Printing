namespace Mega_Batch_Printing.Models
{
    public class PrintJob
    {
        public int Number { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public string FileType { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public int Copies { get; set; } = 1;
    }
}
