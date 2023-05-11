
using Library.Types;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;
using System.IO;
using Library.Config;

namespace Library.FileProcessors;

internal class JSONProcessor : IJSONProcessor
{

    private readonly string path = string.Empty;
    public JSONProcessor(Configuration config)
    {
        path = config.sourcePath;
    }
    public List<Book> ReadJSON()
    {
        List<Book> books = new List<Book>();
        string json = File.ReadAllText(path);

        books = JsonConvert.DeserializeObject<List<Book>>(json);

        return books;
    }
}
