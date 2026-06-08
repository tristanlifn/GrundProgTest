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
    private List<Borrower> _borrowers;
    
    public void Main()
    {
        _borrowers =
        [
            new(1, "Kolding bibliotek", "Henrik", "henrik@gmail.com", "+4541267412"),
            new(2, "Sønderborg bibliotek", "Denis", "denis@gmail.com", "+4553152235"),
            new(3, "Kolding bibliotek", "Nikolaj", "nikolaj@gmail.com", "+4572373521")
        ];

        PrintBorrowers(_borrowers);
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
            Console.WriteLine("No borrower found with borrower num: " + borrowerNum);
        else
            Console.WriteLine(foundBorrower.GetBorrower());
        
    }
}