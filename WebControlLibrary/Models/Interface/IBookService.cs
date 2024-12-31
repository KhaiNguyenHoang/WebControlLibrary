using WebControlLibrary.Models.Entites;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebControlLibrary.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Adds a new book asynchronously.
        /// </summary>
        /// <param name="book">The book to add.</param>
        /// <returns>The added book.</returns>
        Task<Book> AddBookAsync(Book book);

        /// <summary>
        /// Updates an existing book asynchronously.
        /// </summary>
        /// <param name="book">The book with updated information.</param>
        /// <returns>The updated book.</returns>
        Task<Book> UpdateBookAsync(Book book);

        /// <summary>
        /// Deletes a book by its ID asynchronously.
        /// </summary>
        /// <param name="bookId">The ID of the book to delete.</param>
        /// <returns>True if the book was deleted, otherwise false.</returns>
        Task<bool> DeleteBookAsync(int bookId);

        /// <summary>
        /// Retrieves all books asynchronously.
        /// </summary>
        /// <returns>A list of all books.</returns>
        Task<List<Book>> GetAllBooksAsync();

        /// <summary>
        /// Retrieves a book by its ID asynchronously.
        /// </summary>
        /// <param name="bookId">The ID of the book to retrieve.</param>
        /// <returns>The book with the specified ID.</returns>
        Task<Book> GetBookByIdAsync(int bookId);

        /// <summary>
        /// Searches for books based on the search term.
        /// </summary>
        /// <param name="searchTerm">The term to search for in book title, author, or genre.</param>
        /// <returns>A list of books matching the search criteria.</returns>
        Task<List<Book>> SearchBooksAsync(string searchTerm);
    }
}