
using Library.Importing;
using Library.Types;

namespace Library.Utilities
{
    internal class BookDataManipulation
    {
        private readonly IBooksData _booksData;
        public BookDataManipulation(IBooksData booksData)
        {
            _booksData = booksData;
        }
        public void GroupByAuthor()
        {

            try
            {
                List<Book> importedBooks = _booksData.ReadListOfBooks();
                List<string> authorNames = new List<string>();

                importedBooks.Sort(delegate (Book x, Book y)
                {
                    if (x.AuthorName == null && y.AuthorName == null) return 0;
                    else if (x.AuthorName == null) return -1;
                    else if (y.AuthorName == null) return 1;
                    else return x.AuthorName.CompareTo(y.AuthorName);
                });



                foreach (Book book in importedBooks)
                {
                    Console.WriteLine($"Book -> {book.NameOfBook} by {book.AuthorName}");
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

                Console.WriteLine();

                foreach (string author in authorNames)
                {
                    Console.WriteLine($"{author}");
                }


                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nSorting successfull");
                Console.ResetColor();
            }
            catch (NullReferenceException ne)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Null object passed...\n");

                Console.WriteLine(ne.Message);
                Console.WriteLine(ne.StackTrace);

                Console.ResetColor();


            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\n Something went wrong while sorting...\n");

                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);

                Console.ResetColor();

            }


        }
    }
}
