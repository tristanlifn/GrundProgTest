namespace GrundProgTest;

public class Borrower(int borrowerNum, string libraryName, string name, string email, string phone)
    : Person(name, email, phone)
{
    public int BorrowerNum { get; } = borrowerNum;
    public string LibraryName { get; } = libraryName;

    public string GetBorrower() => $"{Name};{Email};{Phone};{BorrowerNum};{LibraryName}";
}