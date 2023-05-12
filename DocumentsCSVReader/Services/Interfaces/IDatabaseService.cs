using DocumentsCSVReader.Models;
using System.Collections.Generic;

namespace DocumentsCSVReader.Services.Interfaces
{
    public interface IDatabaseService
    {
        void SaveDocuments(IEnumerable<Document> documents);
        void SaveDocumentItems(IEnumerable<DocumentItem> documentItems);
        
        IEnumerable<Document> GetDocuments();
        IEnumerable<DocumentItem> GetDocumentItems();

        

    }
}
