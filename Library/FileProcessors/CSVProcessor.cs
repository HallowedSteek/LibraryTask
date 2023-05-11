
using CsvHelper;
using CsvHelper.Configuration;
using Library.Config;
using Library.Types;
using System.Globalization;
using System.Runtime.Intrinsics.X86;

namespace Library.FileProcessors;

internal class CSVProcessor : ICSVProcessor
{
    private readonly string path = string.Empty;
    public CSVProcessor(Configuration config)
    {
        path = config.sourcePath;
    }
    public List<Book> ReadCSV()
    {

        List<Book> books = new List<Book>();

        using (var reader = new StreamReader(path))
        using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        {
            var records = csv.GetRecords<Book>();

            foreach (var item in records)
            {
                books.Add(item);
            }
        }

        return books;
    }
}
