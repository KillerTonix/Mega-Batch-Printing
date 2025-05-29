using Mega_Batch_Printing.Helpers;
using Mega_Batch_Printing.Models;
using Microsoft.Win32;
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
            if (filesToPrint.Count == 0)
            {
                MessageBox.Show("No files to print.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            foreach (var job in filesToPrint)
            {
                try
                {
                    job.Status = "Printing"; // Set status to Printing
                    FilesListBox.Items.Refresh();
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
                    job.Status = "Printed"; // Set status to Printed
                    FilesListBox.Items.Refresh();
                }
                catch
                {
                    job.Status = "Failed";
                    FilesListBox.Items.Refresh();
                }
            }

        }

        private void OpenButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new()
            {
                Filter = "Files (*.pdf; *.doc; *.docx; *.xls; *.xlsx; *.ppt; *.pptx; *.jpg; *.jpeg; *.png; *.tiff; *.txt;)| *.pdf; *.doc; *.docx; *.xls; *.xlsx; *.ppt; *.pptx; *.jpg; *.jpeg; *.png; *.tiff; *.txt;",
                Title = "Select files",
                Multiselect = true,
                CheckFileExists = true,   // Ensure selected file exists
                CheckPathExists = true    // Ensure selected path exists
            };

            // Show the dialog and check if the user selected a file
            if (openFileDialog.ShowDialog() == true)
            {
                int number = 1;
                foreach (string file in openFileDialog.FileNames)
                {
                    if (File.Exists(file))
                    {
                        filesToPrint.Add(new PrintJob { Number = number++, FilePath = file, FileType = Path.GetExtension(file) });
                    }
                }
            }
        }
    }
}

