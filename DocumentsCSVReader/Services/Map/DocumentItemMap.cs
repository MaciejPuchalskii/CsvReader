using CsvHelper.Configuration;
using DocumentsCSVReader.Models;

namespace DocumentsCSVReader.Services
{
    internal class DocumentItemMap : ClassMap<DocumentItem>
    {
        public DocumentItemMap()
        {
            Map(di => di.DocumentId).Index(0);
            Map(di => di.Ordinal).Index(1);
            Map(di => di.Product).Index(2);
            Map(di => di.Quantity).Index(3);
            Map(di => di.Price).Index(4);
            Map(di => di.TaxRate).Index(5);

        }
    }
    
}
