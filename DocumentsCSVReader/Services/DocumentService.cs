using DocumentsCSVReader.Services.Interfaces;
using DocumentsCSVReader.Models;
using System.Linq;
using System;

namespace DocumentsCSVReader.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IFileReader _fileReader;
        private readonly IDatabaseService _databaseService;

        public DocumentService(IDatabaseService databaseService, IFileReader fileReader)
        {
            _databaseService = databaseService;
            _fileReader = fileReader;
        }

        public bool UploadDocumentsToDataBase(string filePath)
        {
            try
            {
                var documents = _fileReader.ReadData<Document>(filePath);
                if(documents.Any())
                {
                    _databaseService.SaveDocuments(documents);
                }
                return true;

            }
            catch(Exception e)
            {
                return false;
            }
           
        }

        public bool UploadDocumentItemsToDataBase(string filePath)
        {
            try
            {
                var documentItems = _fileReader.ReadData<DocumentItem>(filePath);
                if (documentItems.Any())
                {
                    _databaseService.SaveDocumentItems(documentItems);
                }
                return true;
            }
            catch(Exception e)
            {

                return false;
            }
            
        }
    }
}
