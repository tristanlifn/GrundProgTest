namespace GrundProgTest;

public class Person(string name, string email, string phone)
{
    public string Name { get; } = name;
    public string Email { get; } = email;
    public string Phone { get; } = phone;
    public List<Book> BorrowedBooks { get; set; } = [];

    public void BorrowBook(Book book)
    {
        book.BorrowDate = DateTime.Now;
        BorrowedBooks.Add(book);
    }
}