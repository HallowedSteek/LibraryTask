
using Library.Types;

namespace Library.FileProcessors;

internal interface IJSONProcessor
{
    public List<Book> importJSON();
}
