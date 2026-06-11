using System.Text.RegularExpressions;

namespace GrundProgTest;

class Program
{
    static void Main(string[] _)
    {
        Library library = new();
        library.Main();
    }
}

partial class Library
{
    [GeneratedRegex(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$")]
    private static partial Regex EmailRegex();
    
    [GeneratedRegex(@"(?:([+]\d{1,4})[-.\s]?)?(?:[(](\d{1,3})[)][-.\s]?)?(\d{1,4})[-.\s]?(\d{1,4})[-.\s]?(\d{1,9})")]
    private static partial Regex PhoneRegex();
    
    private readonly List<Borrower> _borrowers = [];
    
    public void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.Clear();
            Console.WriteLine("Vælg en mulighed:\n1. Opret ny låner\n2. Vis alle lånere\n3. Se låner detailer" +
                              "\n4. Udlån en bog\n5. Vis alle udlån\n6. Vis alle bøger med overskredet afleverings dato" +
                              "\n7. Afslut");
            
            string? input = Console.ReadLine();

            Console.Clear();
            switch (input)
            {
                case "1":
                    CreateBorrower();
                    break;
                case "2":
                    PrintBorrowers(_borrowers);
                    Console.WriteLine("Tryk på en knap for at fortsætte...");
                    Console.ReadKey();
                    break;
                case "3":
                    FindBorrower();
                    Console.WriteLine("Tryk på en knap for at fortsætte...");
                    Console.ReadKey();
                    break;
                case "4":
                    CreateBorrowing();
                    Console.WriteLine("Tryk på en knap for at fortsætte...");
                    Console.ReadKey();
                    break;
                case "5":
                    ShowAllBorrowedBooks();
                    Console.WriteLine("Tryk på en knap for at fortsætte...");
                    Console.ReadKey();
                    break;
                case "6":
                    PrintAllOverdueBooks();
                    Console.WriteLine("Tryk på en knap for at fortsætte...");
                    Console.ReadKey();
                    break;
                case "7":
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Ugyldig værdi, try again");
                    break;
            }
        }
    }

    /// <summary>
    /// Print all the borrowers via the 'GetBorrower' method in the Borrower class
    /// </summary>
    /// <param name="borrowers">A list of Borrowers to print</param>
    private static void PrintBorrowers(List<Borrower> borrowers)
    {
        foreach (Borrower borrower in borrowers)
        {
            Console.WriteLine(borrower.GetBorrower());
        }
    }

    private static void PrintBook(Book book)
    {
        Console.WriteLine($"Title: {book.Title}");
        Console.WriteLine($"Author: {book.Author}");
        Console.WriteLine($"ISBN number: {book.IsbnNumber}");

        if (!book.WasBorrowed30DaysAgo()) 
            return;
        
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("[OVERDUE]");
        Console.ResetColor();
    }
    
    /// <summary>
    /// Creates a new borrower with input validation, and adds it to the list
    /// </summary>
    private void CreateBorrower()
    {
        int borrowerNum = 1;
        if (_borrowers.Count != 0)
            borrowerNum = _borrowers.Last().BorrowerNum + 1;
        
        bool isValidInput = true;
        string? name;
        string? email;
        string? phone;
        string? libraryName;
        
        // Validate the username
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdi");
            
            Console.Write("Indtast Navn: ");
            name = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(name);

        } while (!isValidInput);

        // Validate the email using regex
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdi");
            
            Console.Write("Indtast Email: ");
            email = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(email) || !EmailRegex().IsMatch(email))
                isValidInput = false;
            else
                isValidInput = true;

        } while (!isValidInput);

        // Validate the phone number using regex
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdi");

            Console.Write("Indtast Tlf. nr.: ");
            phone = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(phone) || !PhoneRegex().IsMatch(phone))
                isValidInput = false;
            else
                isValidInput = true;
            
        } while (!isValidInput);

        // Validate the library name
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdi");
            
            Console.Write("Indtast Bibliotek navn: ");
            libraryName = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(libraryName);
            
        } while (!isValidInput);

        // Double check if any of the values are null or whitespace
        if (string.IsNullOrWhiteSpace(libraryName) || string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine("Error: En eller flere indtastede værdier er ugyldige.");
        }
        
        _borrowers.Add(new Borrower(borrowerNum, libraryName!, name!, email!, phone!));
    }

    /// <summary>
    /// Gets a borrower number from the user, the number is not validated to exist
    /// </summary>
    /// <returns>The borrower number</returns>
    private static int GetBorrowerNumber()
    {
        bool isValidInput = true;
        int borrowerNum = 0;
        
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdi");
            
            Console.Write("Indtast Låner nr.: ");
            string? borrowerNumStr = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(borrowerNumStr) || !int.TryParse(borrowerNumStr, out borrowerNum))
                isValidInput = false;
            else 
                isValidInput = true;
            
        } while (!isValidInput);
        
        return borrowerNum;
    }
    
    /// <summary>
    /// Finds a borrower in the '_borrowers' list, if one exists, writes the info, else writes that no borrower was found
    /// </summary>
    private void FindBorrower()
    {
        int borrowerNum = GetBorrowerNumber();
        
        Borrower? foundBorrower = _borrowers.FirstOrDefault(x => x.BorrowerNum == borrowerNum);

        if (foundBorrower == null)
        {
            Console.WriteLine("No borrower found with borrower num: " + borrowerNum);
            return;
        }
        
        Console.WriteLine(foundBorrower.GetBorrower());

        foreach (Book book in foundBorrower.BorrowedBooks)
        {
            PrintBook(book);
        }
    }

    /// <summary>
    /// Creates a new borrowing for a specified borrower. checks if the borrower has more than 3 books, if not, creates a borrowing
    /// </summary>
    private void CreateBorrowing()
    {
        int borrowerNum = 0;
        bool borrowerExists = true;
        bool isValidInput = true;
        string? title;
        string? author;
        string? isbnNumber;

        do
        {
            if (!isValidInput || !borrowerExists)
                Console.WriteLine("No borrower found with borrower num: " + borrowerNum);
            
            borrowerNum = GetBorrowerNumber();

            if (_borrowers.Exists(x => x.BorrowerNum == borrowerNum))
            {
                borrowerExists = true;
                isValidInput = true;
            }
            else
            {
                borrowerExists = false;
                isValidInput = false;
            }
            
        } while (!isValidInput);
        
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdig");

            Console.Write("Indtast Titel: ");
            title = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(title);

        } while (!isValidInput);
        
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdig");

            Console.Write("Indtast Forfatter: ");
            author = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(author);

        } while (!isValidInput);
        
        do
        {
            if (!isValidInput)
                Console.WriteLine("Ugyldig værdig");

            Console.Write("Indtast ISBN nr.: ");
            isbnNumber = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(isbnNumber);

        } while (!isValidInput);

        if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author) ||
            string.IsNullOrWhiteSpace(isbnNumber))
        {
            Console.WriteLine("Error: En eller flere indtastede værdier er ugyldige.");
            return;
        }
        
        Borrower borrower = _borrowers.FirstOrDefault(x => x.BorrowerNum == borrowerNum)!;

        if (borrower.BorrowedBooks.Count >= 3)
        {
            Console.WriteLine("Du kan nul låne 3 bøger af gange. Aflever en for at låne en ny");
            return;
        }
        
        borrower.BorrowedBooks.Add(new Book(title, author, isbnNumber, DateTime.Now));
    }

    /// <summary>
    /// Prints all the borrowed books of every person
    /// </summary>
    private void ShowAllBorrowedBooks()
    {
        foreach (Borrower borrower in _borrowers)
        {
            Console.WriteLine($"\nLåner: {borrower.Name}");
            foreach (Book book in borrower.BorrowedBooks)
            {
                PrintBook(book);
            }
        }
    }

    private void PrintAllOverdueBooks()
    {
        foreach (Borrower borrower in _borrowers)
        {
            Console.WriteLine($"\nLåner: {borrower.Name}");
            foreach (Book book in borrower.BorrowedBooks.Where(book => book.WasBorrowed30DaysAgo()))
            {
                Console.WriteLine($"Title: {book.Title}");
                Console.WriteLine($"Author: {book.Author}");
                Console.WriteLine($"ISBN number: {book.IsbnNumber}");

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[OVERDUE]");
                Console.ResetColor();
            }
        }
    }
}