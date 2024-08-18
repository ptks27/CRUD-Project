using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using project.Data;
using project.Models;

namespace project.Controllers
{
    public class productsController : Controller
    {
        private readonly ApiquizContext _context;

        public productsController(ApiquizContext context)
        {
            _context = context;
        }

        // GET: products
        // GET: products
        public async Task<IActionResult> Index(int? page)
        {
            int pageSize = 10; // จำนวนรายการที่ต้องการแสดงในหนึ่งหน้า
            int pageNumber = (page ?? 1); // หน้าที่ต้องการแสดง (หน้าเริ่มต้นเป็นหน้าที่ 1)

            // Query ข้อมูลจากฐานข้อมูล
            var products = await _context.products.ToListAsync();

            // หาจำนวนข้อมูลทั้งหมด
            int totalCount = products.Count;

            // คำนวณจำนวนหน้าทั้งหมด
            int totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            // กรณีมีการกำหนดหน้าที่เกินจำนวนหน้าทั้งหมด
            if (pageNumber > totalPages)
            {
                pageNumber = totalPages;
            }

            // กรณีมีการกำหนดหน้าที่น้อยกว่าหน้าเริ่มต้น
            if (pageNumber < 1)
            {
                pageNumber = 1;
            }

            // แบ่งหน้าข้อมูล
            var pagedProducts = products.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            ViewData["TotalPages"] = totalPages; // ส่งจำนวนหน้าทั้งหมดไปยัง View
            ViewData["CurrentPage"] = pageNumber; // ส่งหน้าปัจจุบันไปยัง View

            return View(pagedProducts.ToList());
        }


        // GET: products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {

                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Brand,Category,Price,Description,ImageFileName,CreateAt,Stock")] product product)
        {
            if (ModelState.IsValid)
            {

                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Brand,Category,Price,Description,ImageFileName,CreateAt,Stock")] product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!productExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.products.FindAsync(id);
            if (product != null)
            {
                _context.products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool productExists(int id)
        {
            return _context.products.Any(e => e.Id == id);
        }
    }
}
