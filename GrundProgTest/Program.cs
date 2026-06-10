using System.Text.RegularExpressions;

namespace GrundProgTest;

class Program
{
    static void Main(string[] args)
    {
        Library library = new();
        library.Main();
    }
}

class Library
{
    private List<Borrower> _borrowers = [];
    
    public void Main()
    {
        bool isRunning = true;

        while (isRunning)
        {
            Console.WriteLine("Vælg en mulighed:\n1. Opret ny låner\n2. Vis alle lånere\n3. Se låner detailer" +
                              "\n4. Udlån en bog\n5. Vis alle udlån\n6. Vis alle bøger med overskredet afleverings dato" +
                              "\n7. Afslut");
        }
    }

    /// <summary>
    /// Print all the borrowers via the 'GetBorrower' method in the Borrower class
    /// </summary>
    /// <param name="borrowers">A list of Borrowers to print</param>
    private static void PrintBorrowers(List<Borrower> borrowers)
    {
        foreach (var borrower in borrowers)
        {
            Console.WriteLine(borrower.GetBorrower());
        }
    }

    /// <summary>
    /// Finds a borrower in the '_borrowers' list, if one exists, writes the info, else writes that no borrower was found
    /// </summary>
    /// <param name="borrowerNum">the borrower number to search for</param>
    private void FindBorrower(int borrowerNum)
    {
        Borrower? foundBorrower = _borrowers.FirstOrDefault(x => x.BorrowerNum == borrowerNum);

        if (foundBorrower == null)
        {
            Console.WriteLine("No borrower found with borrower num: " + borrowerNum);
            return;
        }
        
        Console.WriteLine(foundBorrower.GetBorrower());

        foreach (Book book in foundBorrower.BorrowedBooks)
        {
            Console.WriteLine($"Title: {book.Title}");
            Console.WriteLine($"Author: {book.Author}");
            Console.WriteLine($"IDBN number: {book.IsbnNumber}");

            if (book.WasBorrowed30DaysAgo())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[OVERDUE]");
                Console.ResetColor();
            }
        }
    }

    private void CreateBorrower()
    {
        int borrowerNum = _borrowers.Last().BorrowerNum + 1;
        bool isValidInput = true;
        string? name;
        string? email;
        string? phone;
        string? libraryName;
        
        // Validate the username
        do
        {
            if (!isValidInput)
                Console.WriteLine("Invalid input");
            
            Console.Write("Enter Name: ");
            name = Console.ReadLine();

            isValidInput = !string.IsNullOrWhiteSpace(name);

        } while (!isValidInput);

        // Validate the email using regex
        do
        {
            if (!isValidInput)
                Console.WriteLine("Invalid input");
            
            Console.Write("Enter Email: ");
            email = Console.ReadLine();

            Regex emailRegex = new(@"^((?!\.)[\w\-_.]*[^.])(@\w+)(\.\w+(\.\w+)?[^.\W])$");
            if (string.IsNullOrWhiteSpace(email) || !emailRegex.IsMatch(email))
                isValidInput = false;
            else
                isValidInput = true;

        } while (!isValidInput);

        // Validate the phone number using regex
        do
        {
            if (!isValidInput)
                Console.WriteLine("Invalid input");

            Console.Write("Enter Phone number: ");
            phone = Console.ReadLine();

            Regex phoneRegex =
                new(@"(?:([+]\d{1,4})[-.\s]?)?(?:[(](\d{1,3})[)][-.\s]?)?(\d{1,4})[-.\s]?(\d{1,4})[-.\s]?(\d{1,9})");
            if (string.IsNullOrWhiteSpace(phone) || !phoneRegex.IsMatch(phone))
                isValidInput = false;
            else
                isValidInput = true;
            
        } while (!isValidInput);

        // Validate the library name
        do
        {
            if (!isValidInput)
                Console.WriteLine("Invalid input");
            
            Console.Write("Enter Library name: ");
            libraryName = Console.ReadLine();

            isValidInput = string.IsNullOrWhiteSpace(libraryName);
            
        } while (isValidInput);

        // Double check if any of the values are null or whitespace
        if (string.IsNullOrWhiteSpace(libraryName) || string.IsNullOrWhiteSpace(name) ||
            string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(phone))
        {
            Console.WriteLine("Error: one or more inputs were invalid. ");
        }
        
        _borrowers.Add(new Borrower(borrowerNum, libraryName!, name!, email!, phone!));
    }
}