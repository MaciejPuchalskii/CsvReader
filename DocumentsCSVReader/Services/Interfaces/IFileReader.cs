using System.Collections.Generic;

namespace DocumentsCSVReader.Services
{
    public interface IFileReader
    {
        List<T> ReadData<T>(string path);
    }
}
