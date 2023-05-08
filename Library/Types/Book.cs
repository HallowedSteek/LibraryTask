namespace Library.Types;

internal class Book
{

    private string _nameOfBook;
    private string _authorName;

    public string NameOfBook
    {
        get { return _nameOfBook; }
        set { _nameOfBook = value; }
    }

    public string AuthorName
    {
        get { return _authorName; }
        set { _authorName = value; }
    }

}
