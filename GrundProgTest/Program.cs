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
    public void Main()
    {
        List<Borrower> borrowers =
        [
            new(1, "Kolding bibliotek", "Henrik", "henrik@gmail.com", "+4541267412"),
            new(2, "Sønderborg bibliotek", "Denis", "denis@gmail.com", "+4553152235"),
            new(3, "Kolding bibliotek", "Nikolaj", "nikolaj@gmail.com", "+4572373521")
        ];

        PrintBorrowers(borrowers);
    }

    /// <summary>
    /// Print all the borrowers via the 'GetBorrower' mwthon in the Borrower class
    /// </summary>
    /// <param name="borrowers">A list of Borrowers to print</param>
    private static void PrintBorrowers(List<Borrower> borrowers)
    {
        foreach (var borrower in borrowers)
        {
            Console.WriteLine(borrower.GetBorrower());
        }
    }
}