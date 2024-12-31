using WebControlLibrary.Models.Entites;
using WebControlLibrary.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebControlLibrary.Services
{
    public class ReaderService : IReaderService
    {
        private readonly LibraryDbContext _context;

        public ReaderService(LibraryDbContext context)
        {
            _context = context;
        }

        // Thêm mới độc giả
        public async Task<Reader> AddReaderAsync(Reader reader)
        {
            _context.Readers.Add(reader);
            await _context.SaveChangesAsync();
            return reader;
        }

        // Cập nhật thông tin độc giả
        public async Task<Reader> UpdateReaderAsync(Reader reader)
        {
            var existingReader = await _context.Readers.FindAsync(reader.ReaderId);
            if (existingReader == null)
            {
                return null; // Nếu độc giả không tồn tại
            }

            // Cập nhật các thuộc tính của độc giả
            existingReader.FullName = reader.FullName;
            existingReader.DateOfBirth = reader.DateOfBirth;
            existingReader.Address = reader.Address;
            existingReader.PhoneNumber = reader.PhoneNumber;
            existingReader.Email = reader.Email;

            await _context.SaveChangesAsync();
            return existingReader;
        }

        // Xóa độc giả
        public async Task<bool> DeleteReaderAsync(int readerId)
        {
            var reader = await _context.Readers.FindAsync(readerId);
            if (reader != null)
            {
                _context.Readers.Remove(reader);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        // Lấy tất cả độc giả
        public async Task<List<Reader>> GetAllReadersAsync()
        {
            return await _context.Readers
                .AsNoTracking() // Truy vấn không cần theo dõi đối tượng
                .ToListAsync();
        }

        // Lấy thông tin độc giả theo ID
        public async Task<Reader> GetReaderByIdAsync(int readerId)
        {
            return await _context.Readers
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ReaderId == readerId);
        }

        // Tìm kiếm độc giả theo tên
        public async Task<List<Reader>> SearchReadersAsync(string searchTerm)
        {
            return await _context.Readers
                .AsNoTracking()
                .Where(r => r.FullName.Contains(searchTerm))
                .ToListAsync();
        }
    }
}
