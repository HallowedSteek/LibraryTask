
using Library.FileProcessors;
using Library.Types;
using Microsoft.Extensions.Logging;

namespace Library.Utilities;

internal class ListOfBooksActions
{
    private readonly IDataSource _source;
    private readonly ILogger _logger;
    public ListOfBooksActions(IDataSource source, ILogger<ListOfBooksActions> logger)
    {
        _source = source;
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public List<Book> GroupByAuthor()
    {
        List<Book> importedBooks = _source.ReadListOfBooks();
        List<string> authorNames = new List<string>();

        if (importedBooks.Count == 0)
        {
            return null;
        }


        importedBooks.Sort(delegate (Book x, Book y)
        {
            if (x.AuthorName == null && y.AuthorName == null) return 0;
            else if (x.AuthorName == null) return -1;
            else if (y.AuthorName == null) return 1;
            else return x.AuthorName.CompareTo(y.AuthorName);
        });


        try
        {
            foreach (Book book in importedBooks)
            {
                if (string.IsNullOrEmpty(book.AuthorName) || string.IsNullOrEmpty(book.NameOfBook))
                {
                    throw new InvalidDataException();
                };
                if (!authorNames.Contains(book.AuthorName))
                {
                    authorNames.Add(book.AuthorName);
                }
            }



            for (int i = 0; i < authorNames.Count(); i++)
            {
                int numberOfBooks = 0;
                foreach (Book book in importedBooks)
                {
                    if (book.AuthorName.Equals(authorNames.ElementAt(i))) { numberOfBooks++; }
                }
                authorNames[i] += $" has {numberOfBooks} books!";
            }

            _logger.LogInformation("Sorting successfull");


            Console.WriteLine();

            foreach (string author in authorNames)
            {
                Console.WriteLine($"{author}");
            }


            return new List<Book>();
        }
        catch (InvalidDataException ide)
        {
            _logger.LogError("The file contains invalid data such as an empty field...");
            _logger.LogTrace(ide.StackTrace);
            return null;
        }
        catch (Exception ex)
        {

            _logger.LogError(ex.Message);
            _logger.LogTrace(ex.StackTrace);
            return null;
        }

    }
}
