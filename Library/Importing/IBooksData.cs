using Library.Types;

namespace Library.Importing;

internal interface IBooksData
{
    public List<Book> ReadListOfBooks();
}
