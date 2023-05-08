using Library.Config;
using Library.FileProcessors;
using Library.Types;

namespace Library.Importing;

internal class BooksData : IBooksData
{
    private string booksListPath;
    private readonly ICSVProcessor _csvProcessor;
    private readonly IJSONProcessor _jsonProcesor;
    public string BooksListPath
    {
        get { return booksListPath; }
        set { booksListPath = value; }
    }

    public BooksData(Configuration configuration, ICSVProcessor csvProcessor, IJSONProcessor jsonProcessor)
    {
        BooksListPath = configuration.sourcePath;
        _csvProcessor = csvProcessor;
        _jsonProcesor = jsonProcessor;
    }
    public List<Book> ReadListOfBooks()
    {
        try
        {

            List<Book> books = new List<Book>();
            int index = 0;
            string extension = Path.GetExtension(booksListPath);

            if (extension == ".csv")
            {
                return _csvProcessor.importCsv();
            }
            else if (extension == ".json")
            {
                return _jsonProcesor.importJSON();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe file must be either a JSON or a CSV...\n");
                Console.ResetColor();
                return null;
            }



        }
        catch (FormatException fe)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Incorrect file format \n");

            Console.WriteLine(fe.Message);
            Console.WriteLine(fe.StackTrace);
            Console.ResetColor();
            return null;

        }
        catch (InvalidDataException ide)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\nPassed invalid data... \n");

            Console.WriteLine(ide.Message);
            Console.WriteLine(ide.StackTrace);
            Console.ResetColor();
            return null;

        }
        catch (FileNotFoundException fnfe)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n File not found  \n");

            Console.WriteLine(fnfe.Message);
            Console.WriteLine(fnfe.StackTrace);
            Console.ResetColor();
            return null;

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("\n Something went wrong  \n");

            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
            Console.ResetColor();
            return null;
        }

    }
}
