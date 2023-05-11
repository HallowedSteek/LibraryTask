using Library.Types;

namespace Library.FileProcessors;

internal interface IDataSource
{
    public List<Book> ReadListOfBooks();
}
