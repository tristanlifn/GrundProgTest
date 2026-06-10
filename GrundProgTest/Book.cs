namespace GrundProgTest;

public class Book (string title, string author, string isbnNumber, DateTime? borrowDate)
{
    public string Title { get; } = title;
    public string Author { get; } = author;
    public string IsbnNumber { get; } = isbnNumber;
    public DateTime? BorrowDate { get; set; } = borrowDate;

    public bool WasBorrowed30DaysAgo()
    {
        if (BorrowDate == null) 
            return false;
        
        DateTime endDate = BorrowDate.Value.AddDays(30);
        
        return (endDate - BorrowDate).Value.TotalDays > 30;
    }

    public void ExtendBorrowing()
    {
        if (BorrowDate == null)
            return;
        
        if (BorrowDate > DateTime.Now)
            return;
        
        BorrowDate = DateTime.Now;
    }
}