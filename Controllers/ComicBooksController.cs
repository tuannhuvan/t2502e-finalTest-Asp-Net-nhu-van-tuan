using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using T2502E_Comicsys.Data;
using T2502E_Comicsys.Models;

namespace T2502E_Comicsys.Controllers;

public class ComicBooksController(ComicSystemContext context) : Controller
{
    // Dependency Injection: Lấy context từ DI container

        // 1. READ: Hiển thị danh sách
        public async Task<IActionResult> Index()
        {
            return View(await context.ComicBooks.ToListAsync());
        }

        // 2. CREATE: Hiển thị form thêm mới
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComicBook comicBook)
        {
            if (ModelState.IsValid)
            {
                context.Add(comicBook);
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // 3. EDIT: Hiển thị form sửa
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var comicBook = await context.ComicBooks.FindAsync(id);
            if (comicBook == null) return NotFound();
            return View(comicBook);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ComicBook comicBook)
        {
            if (id != comicBook.ComicBookId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    context.Update(comicBook);
                    await context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(comicBook);
        }

        // 4. DELETE: Xác nhận xóa
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var comicBook = await context.ComicBooks.FindAsync(id);
            if (comicBook == null) return NotFound();
            return View(comicBook);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comicBook = await context.ComicBooks.FindAsync(id);
            context.ComicBooks.Remove(comicBook);
            await context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
}