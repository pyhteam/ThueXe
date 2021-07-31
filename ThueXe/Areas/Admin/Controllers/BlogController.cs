using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ThueXe.Areas.Admin.Models.Entities;
using WebCar.Areas.Admin.Models.EF;
using WebCar.Areas.Admin.Models.Entities;

namespace ThueXe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly CarWebDbContext _context;

        private readonly IWebHostEnvironment _webHostEnvironment;
        public BlogController(CarWebDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Blog
        public async Task<IActionResult> Index()
        {
            return View(await _context.Blogs.ToListAsync());
        }

        // GET: Admin/Blog/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // GET: Admin/Blog/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Blog/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BlogId,TieuDe,ImagesFile,TomTat,NoiDung,NgayTao")] Blog blog)
        {
            if (ModelState.IsValid)
            {
                string fileName = await UploadedFileAsync(blog);
                blog.NgayTao = DateTime.Now;
                blog.Images = fileName;

                _context.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        private async Task<string> UploadedFileAsync(Blog blog)
        {
            string fileNewName = "";

            if (blog.ImagesFile != null)
            {
                // check exist file
                if (System.IO.File.Exists(blog.Images + Path.GetExtension(blog.ImagesFile.FileName)))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "blog", blog.Images);
                    System.IO.File.Delete(imagePath);

                }

                else
                {
                    // /Save image to wwwroot/image
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(blog.ImagesFile.FileName);
                    string extension = Path.GetExtension(blog.ImagesFile.FileName);
                    fileNewName = fileName = fileName + extension;
                    string path = Path.Combine(wwwRootPath + "/blog/", fileName);

                    //  uniqueFileName = Guid.NewGuid().ToString() + "_" + xe.ImageFile.FileName;  

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await blog.ImagesFile.CopyToAsync(fileStream);
                    }
                }


            }

            return fileNewName;
        }

        // GET: Admin/Blog/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null)
            {
                return NotFound();
            }
            return View(blog);
        }

        // POST: Admin/Blog/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BlogId,TieuDe,ImagesFile,TomTat,NoiDung,NgayTao")] Blog blog)
        {
            if (id != blog.BlogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = await UploadedFileAsync(blog);
                    var getBlog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.BlogId == id);
                    if (fileName == "")
                    {
                        blog.Images = getBlog.Images;

                    }

                    else
                    {
                        blog.Images = fileName;

                    }

                  
                    blog.NgayTao = getBlog.NgayTao;
                    _context.Update(blog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlogExists(blog.BlogId))
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
            return View(blog);
        }

        // GET: Admin/Blog/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs
                .FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);
        }

        // POST: Admin/Blog/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _context.Blogs.AsNoTracking().FirstOrDefaultAsync(m => m.BlogId == id);
            _context.Blogs.Remove(blog);
            if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "blog", blog.Images)))
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "blog", blog.Images));

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlogExists(int id)
        {
            return _context.Blogs.Any(e => e.BlogId == id);
        }
    }
}
