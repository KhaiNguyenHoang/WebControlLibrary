// Controller cho Reader
using Microsoft.AspNetCore.Mvc;
using WebControlLibrary.Interfaces;
using WebControlLibrary.Models.Entites;
using System.Threading.Tasks;

namespace WebControlLibrary.Controllers
{
    public class ReaderController : Controller
    {
        private readonly IReaderService _readerService;

        public ReaderController(IReaderService readerService)
        {
            _readerService = readerService;
        }

        // GET: Reader/Create
        public IActionResult Create()
        {
            return View(new Reader());
        }

        // POST: Reader/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Reader reader)
        {
            if (ModelState.IsValid)
            {
                await _readerService.AddReaderAsync(reader);
                return RedirectToAction(nameof(Index));
            }
            return View(reader);
        }

        // GET: Reader/Edit/{id}
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var reader = await _readerService.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Reader/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Reader reader)
        {
            if (id != reader.ReaderId)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _readerService.UpdateReaderAsync(reader);
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", $"Có lỗi xảy ra: {ex.Message}");
                }
            }
            return View(reader);
        }

        // GET: Reader/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var reader = await _readerService.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // POST: Reader/Delete/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var result = await _readerService.DeleteReaderAsync(id);
            if (!result)
            {
                return NotFound();
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Reader/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var reader = await _readerService.GetReaderByIdAsync(id);
            if (reader == null)
            {
                return NotFound();
            }
            return View(reader);
        }

        // GET: Reader/Index
        public async Task<IActionResult> Index()
        {
            var readers = await _readerService.GetAllReadersAsync();
            return View(readers);
        }

        // GET: Reader/Search
        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var readers = await _readerService.SearchReadersAsync(searchTerm);
            return View("Index", readers);
        }
    }
}

// View cho Create, Edit, Delete, Details, và Index cần được tạo tương tự như BookController.
