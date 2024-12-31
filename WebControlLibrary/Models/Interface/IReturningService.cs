using WebControlLibrary.Models.Entites;
using System.Collections.Generic;

namespace WebControlLibrary.Interfaces
{
    public interface IReturningService
    {
        // Trả sách
        Task<Returning> ReturnBookAsync(int borrowId, DateOnly returnDate, decimal lateFee);

        // Lấy thông tin trả sách theo BorrowId
        Task<Returning> GetReturningByBorrowIdAsync(int borrowId);

        // Lấy danh sách các lần trả sách
        Task<List<Returning>> GetAllReturningsAsync();
    }
}