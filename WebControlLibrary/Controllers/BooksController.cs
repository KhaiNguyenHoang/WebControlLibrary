// Hoàn thiện Controller
using Microsoft.AspNetCore.Mvc;
using WebControlLibrary.Interfaces;
using WebControlLibrary.Models.Entites;
using System.Threading.Tasks;

namespace WebControlLibrary.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        // GET: Book/Create
        public IActionResult Create()
        {
            return View(new Book());
        }

        // POST: Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                await _bookService.AddBookAsync(book);
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        // GET: Book/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id <= 0) // Kiểm tra nếu ID không hợp lệ
            {
                return BadRequest(); // Trả lỗi 400
            }

            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(); // Trả lỗi 404 nếu không tìm thấy sách
            }
            return View(book);
        }


        // POST: Book/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.BookId) // Kiểm tra tính hợp lệ của ID
            {
                return BadRequest("ID không khớp với sách được gửi lên."); // Trả lỗi 400 với thông điệp rõ ràng
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(book);
                    return RedirectToAction(nameof(Index)); // Chuyển hướng về danh sách sách sau khi cập nhật
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Lỗi khi cập nhật sách: {ex.Message}");
                }
            }
            return View(book); // Quay lại view nếu có lỗi
        }

        // GET: Book/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Book/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            return View(book);
        }

        // GET: Book/Index
        public async Task<IActionResult> Index()
        {
            var books = await _bookService.GetAllBooksAsync();
            return View(books);
        }
    }
}
