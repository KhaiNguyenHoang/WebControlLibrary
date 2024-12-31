using WebControlLibrary.Models.Entites;
using System.Collections.Generic;

namespace WebControlLibrary.Interfaces
{
    public interface IReaderService
    {
        // Thêm mới độc giả
        Task<Reader> AddReaderAsync(Reader reader);

        // Sửa thông tin độc giả
        Task<Reader> UpdateReaderAsync(Reader reader);

        // Xóa độc giả
        Task<bool> DeleteReaderAsync(int readerId);

        // Lấy danh sách tất cả độc giả
        Task<List<Reader>> GetAllReadersAsync();

        // Lấy thông tin độc giả theo ID
        Task<Reader> GetReaderByIdAsync(int readerId);

        // Tìm kiếm độc giả theo tên
        Task<List<Reader>> SearchReadersAsync(string searchTerm);
    }
}