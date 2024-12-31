using WebControlLibrary.Models.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebControlLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebControlLibrary.Services
{
    public class BorrowingService : IBorrowingService
    {
        private readonly LibraryDbContext _context;

        public BorrowingService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Borrowing> BorrowBookAsync(Borrowing borrowing)
        {
            // Kiểm tra xem sách có còn số lượng để mượn hay không
            var book = await _context.Books.FindAsync(borrowing.BookId);
            if (book == null || book.Quantity <= 0)
            {
                return null; // Không thể mượn sách nếu không có sách
            }

            // Giảm số lượng sách khi mượn
            book.Quantity--;
            _context.Borrowings.Add(borrowing);
            await _context.SaveChangesAsync();

            return borrowing;
        }

        public async Task<Returning> ReturnBookAsync(int borrowId, DateOnly returnDate, decimal lateFee)
        {
            var borrowing = await _context.Borrowings
                .Include(b => b.Book)  // Include related Book entity
                .FirstOrDefaultAsync(b => b.BorrowId == borrowId);

            if (borrowing == null)
            {
                throw new InvalidOperationException("Borrowing record does not exist."); // Handle missing record
            }

            if (borrowing.ReturnDate != null)
            {
                throw new InvalidOperationException("This book has already been returned."); // Prevent duplicate returns
            }

            borrowing.ReturnDate = returnDate; // Set the return date

            // Update book quantity
            if (borrowing.Book != null)
            {
                borrowing.Book.Quantity++;
            }

            // Create a new returning record
            var returning = new Returning
            {
                BorrowId = borrowId,
                ReturnDate = returnDate,
                LateFee = lateFee
            };

            _context.Returnings.Add(returning);

            // Save changes to the database
            await _context.SaveChangesAsync();
            return returning;
        }


        public async Task<List<Borrowing>> GetBorrowingsByReaderIdAsync(int readerId)
        {
            // Truy vấn các lần mượn của độc giả
            return await _context.Borrowings
                .Include(b => b.Book)
                .Where(b => b.ReaderId == readerId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Borrowing>> GetBorrowingsByBookIdAsync(int bookId)
        {
            // Truy vấn các lần mượn của sách
            return await _context.Borrowings
                .Include(b => b.Reader)
                .Where(b => b.BookId == bookId)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<bool> IsBookBorrowedAsync(int bookId)
        {
            // Kiểm tra xem sách đã mượn hay chưa
            return await _context.Borrowings
                .AnyAsync(b => b.BookId == bookId && b.ReturnDate == null);
        }

        public async Task<List<Reader>> GetAllReadersAsync()
        {
            // Truy vấn tất cả độc giả
            return await _context.Readers
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Book>> GetAvailableBooksAsync()
        {
            // Truy vấn sách có sẵn (số lượng > 0)
            return await _context.Books
                .Where(b => b.Quantity > 0)
                .AsNoTracking()
                .ToListAsync();
        }
        public async Task<List<Borrowing>> GetAllBorrowingsAsync()
        {
            return await _context.Borrowings
                .Include(b => b.Book)  // Include related Book entity
                .Include(b => b.Reader)  // Include related Reader entity
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Borrowing?> GetBorrowingByIdAsync(int id)
        {
            return await _context.Borrowings
                .Include(b => b.Book)  // Include related Book entity
                .Include(b => b.Reader)  // Include related Reader entity
                .FirstOrDefaultAsync(b => b.BorrowId == id);
        }
    }
}
