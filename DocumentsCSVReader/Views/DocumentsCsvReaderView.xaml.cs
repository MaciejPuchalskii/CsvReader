using DocumentsCSVReader.Models;
using DocumentsCSVReader.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DocumentsCSVReader.Views
{
    public partial class DocumentsCsvReaderView : UserControl
    {
        public delegate void SendDataDelegate(List<Document> documents, List<DocumentItem> documentItems);

        public List<DocumentItem> documentItems; 
        public List<Document> documents;
        private readonly IDatabaseService _databaseService;
        private readonly IDocumentService _documentService;

        public event SendDataDelegate SendDataEvent;

        public DocumentsCsvReaderView()
        {
            InitializeComponent();

            _databaseService = App.serviceProvider.GetService<IDatabaseService>();
            _documentService = App.serviceProvider.GetService<IDocumentService>();
            documentItems = new List<DocumentItem> { };
            documents = new List<Document> { };
            SendDataEvent += DocumentsCsvReaderView_SendDataEvent;


        }
        private void UploadItemsButton_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv"
            };

            string solutionPath = Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            string parentDirPath = Directory.GetParent(solutionPath).FullName;

            openFileDialog.InitialDirectory = parentDirPath;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                var uploadResult = _documentService.UploadDocumentItemsToDataBase(selectedFilePath);
                if(uploadResult)
                {
                    // Otwiera okienko informacja udalo sie
                    MessageBox.Show("Operation succeeded.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Otwiera okienko informacja nie udalo sie
                    MessageBox.Show("Operation failed.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }
        private void UploadDocumentsButton_Click_1(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
            openFileDialog.Filter = "CSV files (*.csv)|*.csv";

            string solutionPath = System.IO.Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory);

            string parentDirPath = Directory.GetParent(solutionPath).FullName;

            openFileDialog.InitialDirectory = parentDirPath;

            bool? result = openFileDialog.ShowDialog();

            if (result == true)
            {
                string selectedFilePath = openFileDialog.FileName;
                var uploadResult = _documentService.UploadDocumentsToDataBase(selectedFilePath);
                if (uploadResult)
                {
                    // Otwiera okienko informacja udalo sie
                    MessageBox.Show("Operation succeeded.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    // Otwiera okienko informacja nie udalo sie
                    MessageBox.Show("Operation failed.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }

        private void ReadDataFromDBButton_Click(object sender, RoutedEventArgs e)
        {
            documents = _databaseService.GetDocuments().ToList();
            documentItems = _databaseService.GetDocumentItems().ToList();

            SendDataEvent.Invoke(documents, documentItems);
        }
        private void DocumentsCsvReaderView_SendDataEvent(System.Collections.Generic.List<Models.Document> documents, System.Collections.Generic.List<Models.DocumentItem> documentItems)
        {
            documentsDisplayControl.ReceiveData(documents, documentItems);
        }


    }

}
