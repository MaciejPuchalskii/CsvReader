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
                    MessageBox.Show("Failed upload items to db.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }
        private void UploadDocumentsButton_Click_1(object sender, RoutedEventArgs e)
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
                var uploadResult = _documentService.UploadDocumentsToDataBase(selectedFilePath);
                if (uploadResult)
                {
                    MessageBox.Show("Operation succeeded.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Failed upload documents to db.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }

        private void ReadDataFromDBButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                documents = _databaseService.GetDocuments().ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Failed reading Documents", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try 
            {
                documentItems = _databaseService.GetDocumentItems().ToList();
            }
            catch
            {
                MessageBox.Show("Failed reading Items.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            SendDataEvent.Invoke(documents, documentItems);
        }
        private void DocumentsCsvReaderView_SendDataEvent(List<Document> documents, List<DocumentItem> documentItems)
        {
            documentsDisplayControl.ReceiveData(documents, documentItems);
        }


    }

}
