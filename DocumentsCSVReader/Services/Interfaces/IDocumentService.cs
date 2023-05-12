namespace DocumentsCSVReader.Services.Interfaces
{
    public interface IDocumentService
    {

        bool UploadDocumentsToDataBase(string filePath);
        bool UploadDocumentItemsToDataBase(string filePath);

    }
}
