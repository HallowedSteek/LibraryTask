using Library.Config;
using Library.Types;
using static System.Reflection.Metadata.BlobBuilder;
using System;

namespace Library.FileProcessors;

internal class CSVProcessor : ICSVProcessor
{
    private string importedCSV;
    private readonly List<Book> books = new List<Book>();
    public string ImportedCSV
    {
        get { return importedCSV; }
        set { importedCSV = value; }
    }
    public CSVProcessor(Configuration configuration)
    {
        importedCSV = configuration.sourcePath;
    }
    public List<Book> importCsv()
    {
        string[] importedFile = File.ReadAllLines(importedCSV);
        int index = 0;

        for (int i = 1; i < importedFile.Length; i++)
        {
            string[] splitLine = importedFile[i].Split(',');

            Book aux = new Book();

            aux.NameOfBook = splitLine[0];
            aux.AuthorName = splitLine[1];

            books.Add(aux);
        }

        foreach (var item in books)
        {
            Console.WriteLine($"Book {index} -> {item.NameOfBook} by {item.AuthorName}");
            index++;
        }


        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("\nCSV Read successfully...\n");
        Console.ResetColor();

        return books;
    }
}
