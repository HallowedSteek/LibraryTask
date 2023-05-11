using Library.Types;

namespace Library.FileProcessors;

internal interface ICSVProcessor
{
    public List<Book> ReadCSV();
}
