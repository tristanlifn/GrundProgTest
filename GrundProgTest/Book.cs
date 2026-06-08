namespace GrundProgTest;

public class Book (string title, string author, string isbnNumber, DateTime? borrowDate)
{
    public string Title { get; } = title;
    public string Author { get; } = author;
    public string IsbnNumber { get; } = isbnNumber;
    public DateTime? BorrowDate { get; set; } = borrowDate;
}