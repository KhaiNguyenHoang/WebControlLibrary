using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using WebControlLibrary.Interfaces;
using WebControlLibrary.Models.Entites;

namespace WebControlLibrary.Controllers
{
    public class BorrowingController : Controller
    {
        private readonly IBorrowingService _borrowingService;

        public BorrowingController(IBorrowingService borrowingService)
        {
            _borrowingService = borrowingService;
        }

        // GET: Borrowing
        public async Task<IActionResult> Index()
        {
            var borrowings = await _borrowingService.GetAllBorrowingsAsync();

            foreach (var borrowing in borrowings)
            {
                System.Console.WriteLine($"Borrowing ID: {borrowing.BorrowId}, Reader: {borrowing.Reader?.FullName}, Book: {borrowing.Book?.Title}");
            }

            return View(borrowings);
        }

        // GET: Borrowing/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Readers = await _borrowingService.GetAllReadersAsync();
            ViewBag.Books = await _borrowingService.GetAvailableBooksAsync();
            return View();
        }

        // POST: Borrowing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Borrowing borrowing)
        {
            if (ModelState.IsValid)
            {
                var result = await _borrowingService.BorrowBookAsync(borrowing);
                if (result == null)
                {
                    ModelState.AddModelError("", "Unable to borrow book. Please check availability.");
                    return View(borrowing);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(borrowing);
        }

        // GET: Borrowing/Return/5
        [HttpGet]
        public async Task<IActionResult> Return(int id)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            if (borrowing.ReturnDate != null)
            {
                ModelState.AddModelError("", "This book has already been returned.");
                return View("Error");
            }

            return View(borrowing);
        }

        // POST: Borrowing/Return/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Return(int id, DateOnly returnDate, decimal lateFee)
        {
            var borrowing = await _borrowingService.GetBorrowingByIdAsync(id);
            if (borrowing == null)
            {
                return NotFound();
            }

            try
            {
                await _borrowingService.ReturnBookAsync(id, returnDate, lateFee);
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(borrowing);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
