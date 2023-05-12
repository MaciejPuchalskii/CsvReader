using CsvHelper.Configuration;
using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DocumentsCSVReader.Services
{
    public class CSVReader : IFileReader
    {
        public List<T> ReadData<T>(string path)
        {

            using (var reader = new StreamReader(path))
            {
                using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
                {
                    MissingFieldFound = null,
                    Delimiter = ";"
                }))
                {
                    csv.Context.RegisterClassMap<DocumentMap>();
                    var records = csv.GetRecords<T>();
                    return records.ToList();
                }
            }
            
        }
    }
}
