using CsvHelper.Configuration;
using DocumentsCSVReader.Models;

namespace DocumentsCSVReader.Services
{
    public class DocumentMap : ClassMap<Document>
    {
        public DocumentMap() 
        {
            Map(d => d.Id).Index(0);
            Map(d => d.Type).Index(1);
            Map(d => d.Date).Index(2);
            Map(d => d.FirstName).Index(3);
            Map(d => d.LastName).Index(4);
            Map(d => d.City).Index(5);

        }
    }
}
