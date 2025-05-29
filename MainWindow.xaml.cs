using Mega_Batch_Printing.Helpers;
using Mega_Batch_Printing.Models;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Mega_Batch_Printing
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<PrintJob> filesToPrint = [];

        public MainWindow()
        {
            InitializeComponent();
            FilesListBox.ItemsSource = filesToPrint;
        }

        private void FilesListBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effects = DragDropEffects.Copy;
            else
                e.Effects = DragDropEffects.None;
        }

        private void FilesListBox_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                int number = 1;
                string[] droppedFiles = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in droppedFiles)
                {
                    if (File.Exists(file))
                    {
                        filesToPrint.Add(new PrintJob { Number = number++, FilePath = file, FileType = Path.GetExtension(file) });
                    }
                }
            }
        }

        private void FilesListBox_DragLeave(object sender, DragEventArgs e)
        {
            // Optional visual feedback
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            foreach (var job in filesToPrint)
            {
                try
                {
                    switch (job.FileType.ToLower())
                    {
                        case ".pdf":
                            PdfPrintHelper.Print(job.FilePath);
                            break;
                        case ".doc":
                        case ".docx":
                            WordPrintHelper.Print(job.FilePath);
                            break;
                        case ".xls":
                        case ".xlsx":
                            ExcelPrintHelper.Print(job.FilePath);
                            break;
                        case ".ppt":
                        case ".pptx":
                            PowerPointPrintHelper.Print(job.FilePath);
                            break;
                        case ".jpg":
                        case ".jpeg":
                        case ".png":
                        case ".tiff":
                            ImagePrintHelper.Print(job.FilePath);
                            break;
                        case ".txt":
                            TxtPrintHelper.Print(job.FilePath);
                            break;
                        default:
                            job.Status = "Unsupported";
                            continue;
                    }
                    job.Status = "Printed";
                }
                catch
                {
                    job.Status = "Failed";
                }
            }
        }
    }
}

