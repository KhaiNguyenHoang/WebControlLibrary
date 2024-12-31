using WebControlLibrary.Models.Entites;
using WebControlLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace WebControlLibrary.Services
{
    public class ReturningService : IReturningService
    {
        private readonly LibraryDbContext _context;

        public ReturningService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Returning> ReturnBookAsync(int borrowId, DateOnly returnDate, decimal lateFee)
        {
            // Find the borrowing record
            var borrowing = await _context.Borrowings
                .Include(b => b.Book)
                .FirstOrDefaultAsync(b => b.BorrowId == borrowId);

            if (borrowing == null)
            {
                return null; // Borrowing record does not exist
            }

            if (borrowing.ReturnDate != null)
            {
                throw new InvalidOperationException("This book has already been returned."); // Prevent double returns
            }

            borrowing.ReturnDate = returnDate; // Update return date

            // Update the book quantity
            if (borrowing.Book != null)
            {
                borrowing.Book.Quantity++;
            }

            // Create a new returning record
            var returning = new Returning
            {
                BorrowId = borrowId,
                ReturnDate = returnDate,
                LateFee = lateFee // Set the calculated late fee
            };

            _context.Returnings.Add(returning);

            // Save all changes to the database
            await _context.SaveChangesAsync();

            return returning;
        }
        // Lấy thông tin trả sách theo BorrowId
        public async Task<Returning> GetReturningByBorrowIdAsync(int borrowId)
        {
            return await _context.Returnings
                .Include(r => r.Borrow)
                .FirstOrDefaultAsync(r => r.BorrowId == borrowId);
        }

        // Lấy tất cả các lần trả sách
        public async Task<List<Returning>> GetAllReturningsAsync()
        {
            return await _context.Returnings
                .Include(r => r.Borrow)
                .AsNoTracking() // Truy vấn không cần theo dõi đối tượng
                .ToListAsync();
        }
    }
}
