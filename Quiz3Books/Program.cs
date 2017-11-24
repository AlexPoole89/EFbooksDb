using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz3Books
{
    class Program
    {
    //  Your program will display and implement the following menu:

    //Make a choice[0 - 5]:
    //1. Add book
    //2. List all books(with Ids)
    //3. Delete book by Id
    //4. Modify a book by Id
    //5. List all books from oldest to newest using LINQ
    //6. Find a book by author name or title using LINQ
    //0. Exit
    //Enter your choice: _
        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;

            do
            {
                Console.WriteLine();
                Console.WriteLine("Make a choice [0 - 5]");
                Console.WriteLine("1. Add book");
                Console.WriteLine("2. List all books (with Ids)");
                Console.WriteLine("3. Delete book by Id");
                Console.WriteLine("4. Modify a book by Id");
                Console.WriteLine("5. List all books from oldest to newest using LINQ");
                Console.WriteLine("6. Find a book by author name or title using LINQ");
                Console.WriteLine("0. Exit");
                Console.WriteLine("Enter your choice: ");
                Console.WriteLine();

                keyInfo = Console.ReadKey();

                switch (keyInfo.Key)
                {

                    case ConsoleKey.D1:
                        //add new book
                        Console.WriteLine();
                        Book book = new Book();
                        Console.Write("Enter Book Title: ");
                        book.Title = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Enter Author Name: ");
                        book.Author = Console.ReadLine();
                        Console.WriteLine();
                        Console.Write("Enter Publishing Year: ");
                        //allows only year entry
                        book.Published = new DateTime(int.Parse(Console.ReadLine()), 1,1);
                        Console.WriteLine();

                        if(book.Published.Year < 1700 || book.Published.Year > 2100)
                        {
                            Console.WriteLine("Publishing year must be between 1700 and 2100");
                            continue;
                        }
                        else
                        {
                            using (LibraryDbContext ctx = new LibraryDbContext())
                            {
                                ctx.Books.Add(book); //schedules book to be added to table
                                ctx.SaveChanges();
                                Console.WriteLine("Book saved!");
                            }
                        }                        
                        break;
                    case ConsoleKey.D2:
                        //list all books with Ids
                        Console.WriteLine();
                        using (LibraryDbContext ctx = new LibraryDbContext())
                        {
                            var books = (from t in ctx.Books select t).ToList<Book>();

                            foreach (var b in books)
                            {

                                Console.WriteLine("Book #{0} titled {1} written by {2} in {3}", b.Id, b.Title, b.Author, b.Published.Year);
                            }
                        }
                        break;
                    case ConsoleKey.D3:
                        Console.WriteLine();
                        //delete book by ID
                        using (LibraryDbContext ctx = new LibraryDbContext())
                        {
                            //delete method 1: fetch then delete
                            Console.WriteLine("Select an Id");
                            int id = Convert.ToInt32(Console.ReadLine());
                            var bookList = (from r in ctx.Books where r.Id == id select r).ToList<Book>();
                            if (bookList.Count == 0)
                            {
                                Console.WriteLine("No records to delete. :(");
                            }
                            else
                            {
                                Book btd = bookList[0];
                                ctx.Books.Remove(btd);
                                ctx.SaveChanges();
                                Console.WriteLine("Book successfully deleted!");
                            }
                        }
                        break;
                    case ConsoleKey.D4:
                        Console.WriteLine();
                        //modify a book by ID
                        using (LibraryDbContext ctx = new LibraryDbContext())
                        {
                            Console.Write("Select Book ID: ");
                            int BookId = Convert.ToInt32(Console.ReadLine());
                            var books = (from b in ctx.Books where b.Id == BookId select b).ToList<Book>();

                            if (books.Count == 0)
                            {
                                Console.WriteLine("Unable to find record with Id=" + BookId);
                                continue;
                            }

                            Book updateBook = books[0];
                            Console.WriteLine();
                            Console.WriteLine("Make a selection");
                            Console.WriteLine("7. Update Book Title");
                            Console.WriteLine("8. Update Book Author");
                            Console.WriteLine("9. Update Publishing Date");
                            ConsoleKeyInfo KeyClicked;
                            KeyClicked = Console.ReadKey();
                            switch (KeyClicked.Key)
                            {
                                case ConsoleKey.D7:
                                    //update title
                                    Console.WriteLine("Enter a new book title");
                                    updateBook.Title = Console.ReadLine();
                                    ctx.SaveChanges();
                                    Console.WriteLine("Successfully updated Title!");
                                    break;
                                case ConsoleKey.D8:
                                    //update author
                                    Console.WriteLine("Enter a new author");
                                    updateBook.Author = Console.ReadLine();
                                    ctx.SaveChanges();
                                    Console.WriteLine("Successfully updated author!");
                                    break;
                                case ConsoleKey.D9:
                                    //update to where
                                    Console.WriteLine("Enter a new publishing date");
                                    updateBook.Published = DateTime.Parse( Console.ReadLine());
                                    ctx.SaveChanges();
                                    Console.WriteLine("Successfully updated publishing date!");
                                    break;
                                default:
                                    Console.WriteLine("Select between 7-9");
                                    break;
                            }
                        }
                        break;
                    case ConsoleKey.D5:
                        //List all books from oldest to newest using LINQ
                        Console.WriteLine();
                        using (LibraryDbContext ctx = new LibraryDbContext())
                        {

                            List<Book> selectBook = (from b in ctx.Books orderby b.Published ascending select b).ToList<Book>();
                           
                            foreach (var b in selectBook)
                            {
                                Console.WriteLine("Book #{0} titled {1} written by {2} in {3}", b.Id, b.Title, b.Author, b.Published.Year);
                            }
                        }
                        break;
                    case ConsoleKey.D6:
                        //Find a book by author name or title using LINQ
                        Console.WriteLine();
                        using (LibraryDbContext ctx = new LibraryDbContext())
                        {
                            Console.WriteLine("Enter book title or author name: ");
                            string title = Console.ReadLine();
                            List<Book> selectBook = (from b in ctx.Books where b.Title == title || b.Author == title select b).ToList<Book>();

                            foreach (var b in selectBook)
                            {

                                Console.WriteLine("Book #{0} titled {1} written by {2} in {3}", b.Id, b.Title, b.Author, b.Published.Year);
                            }
                        }
                        break;
                    default:
                        Console.WriteLine();
                        Console.WriteLine("Press 0 to exit");
                        break;
                }
            }
            while (keyInfo.Key != ConsoleKey.D0);
        }
    }
}
