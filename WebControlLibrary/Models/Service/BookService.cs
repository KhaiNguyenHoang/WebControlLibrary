using WebControlLibrary.Interfaces;
using WebControlLibrary.Models.Entites;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace WebControlLibrary.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryDbContext _context;

        public BookService(LibraryDbContext context)
        {
            _context = context;
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            // Ensure the book doesn't already exist (optional, depending on your requirements)
            var existingBook = await _context.Books
                .FirstOrDefaultAsync(b => b.Isbn == book.Isbn);

            if (existingBook != null)
            {
                throw new InvalidOperationException($"A book with ISBN {book.Isbn} already exists.");
            }

            _context.Books.Add(book);
            await _context.SaveChangesAsync(); // Save changes asynchronously
            return book;
        }

        public async Task<List<Book>> GetAllBooksAsync()
        {
            // Fetch all books from the database without tracking them (for read-only queries)
            return await _context.Books.AsNoTracking().ToListAsync(); // Use AsNoTracking for read-only operations
        }

        public async Task<Book> GetBookByIdAsync(int bookId)
        {
            // Fetch the book by its ID with no tracking (for read-only access)
            var book = await _context.Books
                .AsNoTracking() // No need to track the entity as we are only reading
                .FirstOrDefaultAsync(b => b.BookId == bookId); // Find book by ID

            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {bookId} not found.");
            }

            return book;
        }

        public async Task<Book> UpdateBookAsync(Book book)
        {
            // Find the existing book in the database
            var existingBook = await _context.Books.FindAsync(book.BookId);
            if (existingBook == null)
            {
                throw new KeyNotFoundException($"Book with ID {book.BookId} not found."); // Throw custom exception if book is not found
            }

            // Update the book details
            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Genre = book.Genre;
            existingBook.Isbn = book.Isbn;
            existingBook.Quantity = book.Quantity;

            await _context.SaveChangesAsync(); // Save changes to the database
            return existingBook; // Return the updated book
        }

        public async Task<bool> DeleteBookAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book != null)
            {
                _context.Books.Remove(book); // Remove the book from the database
                await _context.SaveChangesAsync(); // Save changes asynchronously
                return true; // Successfully deleted
            }

            // If the book is not found, return false
            return false;
        }

        public async Task<List<Book>> SearchBooksAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return await _context.Books.AsNoTracking().ToListAsync(); // Return all books if no search term
            }

            // Search for books by title, author, or genre (case insensitive search)
            var books = await _context.Books
                .AsNoTracking()
                .Where(b => b.Title.Contains(searchTerm) || b.Author.Contains(searchTerm) || b.Genre.Contains(searchTerm))
                .ToListAsync();

            return books;
        }
    }
}
