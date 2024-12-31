using WebControlLibrary.Models.Entites;
using System.Collections.Generic;

namespace WebControlLibrary.Interfaces
{
    public interface IBorrowingService
    {
        // Mượn sách
        Task<Borrowing> BorrowBookAsync(Borrowing borrowing);

        // Trả sách
        Task<Returning> ReturnBookAsync(int borrowId, DateOnly returnDate, decimal lateFee);

        // Lấy danh sách các lần mượn của độc giả
        Task<List<Borrowing>> GetBorrowingsByReaderIdAsync(int readerId);

        // Lấy danh sách các lần mượn theo sách
        Task<List<Borrowing>> GetBorrowingsByBookIdAsync(int bookId);

        // Kiểm tra có phải sách đã mượn hay chưa
        Task<bool> IsBookBorrowedAsync(int bookId);
        Task<List<Reader>> GetAllReadersAsync();
        Task<List<Book>> GetAvailableBooksAsync();
        Task<List<Borrowing>> GetAllBorrowingsAsync();
        Task<Borrowing?> GetBorrowingByIdAsync(int id);
    }
}