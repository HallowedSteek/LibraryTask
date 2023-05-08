
using Library.Config;
using Library.Types;
using Newtonsoft.Json;
using System;

namespace Library.FileProcessors;

internal class JSONProcessor : IJSONProcessor
{
    private string importedJSON;
    private List<Book> books = new List<Book>();
    public string ImportedJSON
    {
        get { return importedJSON; }
        set { importedJSON = value; }
    }
    public JSONProcessor(Configuration configuration)
    {
        importedJSON = configuration.sourcePath;
    }
    public List<Book> importJSON()
    {
        string json = File.ReadAllText(importedJSON);
        books = JsonConvert.DeserializeObject<List<Book>>(json);

        int index = 0;

        foreach (var item in books)
        {
            Console.WriteLine($"Book {index} -> {item.NameOfBook} by {item.AuthorName}");
            index++;
        }

        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\n JSON Read successfully...\n");
        Console.ResetColor();
        return books;

    }
}
