using System.Xml;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebCar.Areas.Admin.Models.EF;
using WebCar.Areas.Admin.Models.Entities;
using ThueXe.Areas.Admin.Models.Entities;

namespace ThueXe.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class XeController : Controller
    {
        private readonly CarWebDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public XeController(CarWebDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Xe
        public async Task<IActionResult> Index()
        {
            var carWebDbContext = _context.Xe.Include(x => x.DanhMuc).AsNoTracking();
            return View(await carWebDbContext.ToListAsync());
        }

        // GET: Admin/Xe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.DanhMuc)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // GET: Admin/Xe/Create
        public IActionResult Create()
        {
            ViewData["NameCategory"] = new SelectList(_context.CategoriesCar, "DanhMucId", "NameCategory");
            return View();
        }

        // POST: Admin/Xe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CarId,NameCar,ImageFile,TomTat,NoiDung,GiaThue,CreateDate,NoiBat,LuotXem,DanhMucId")] Xe xe)
        {
            if (ModelState.IsValid)
            {
                string fileName = await UploadedFileAsync(xe);
                xe.CreateDate = DateTime.Now;
                xe.LuotXem = 0;
                xe.ImageName = fileName;

                _context.Add(xe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DanhMucId"] = new SelectList(_context.CategoriesCar, "DanhMucId", "NameCategory", xe.DanhMucId);
            return View(xe);
        }

        // GET: Admin/Xe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe.FindAsync(id);
            if (xe == null)
            {
                return NotFound();
            }
            ViewData["DanhMucId"] = new SelectList(_context.CategoriesCar, "DanhMucId", "NameCategory", xe.DanhMucId);

            return View(xe);
        }

        // POST: Admin/Xe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CarId,NameCar,ImageFile,TomTat,NoiDung,GiaThue,CreateDate,NoiBat,LuotXem,DanhMucId")] Xe xe)
        {
            if (id != xe.CarId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {

                try
                {

                    string fileName = await UploadedFileAsync(xe);
                    var xe1 = await _context.Xe.AsNoTracking().Include(x => x.DanhMuc).FirstOrDefaultAsync(m => m.CarId == id);
                    if (fileName == "")
                    {
                        xe.ImageName = xe1.ImageName;

                    }

                    else
                    {
                        xe.ImageName = fileName;

                    }

                    xe.LuotXem = xe1.LuotXem;
                    xe.CreateDate = xe1.CreateDate;

                    _context.Update(xe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!XeExists(xe.CarId))
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
            ViewData["DanhMucId"] = new SelectList(_context.CategoriesCar, "DanhMucId", "NameCategory", xe.DanhMucId);
            return View(xe);
        }

        // GET: Admin/Xe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.DanhMuc)
                .FirstOrDefaultAsync(m => m.CarId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Admin/Xe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xe = await _context.Xe.FindAsync(id);
            _context.Xe.Remove(xe);
            if (System.IO.File.Exists(Path.Combine(_webHostEnvironment.WebRootPath, "Images", xe.ImageName)))
                System.IO.File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, "Images", xe.ImageName));
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
            return _context.Xe.Any(e => e.CarId == id);
        }

        private async Task<string> UploadedFileAsync(Xe xe)
        {
            string fileNewName = "";

            if (xe.ImageFile != null)
            {
                // check exist file
                if (System.IO.File.Exists(xe.ImageName + Path.GetExtension(xe.ImageFile.FileName)))
                {
                    var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Images", xe.ImageName);
                    System.IO.File.Delete(imagePath);

                }

                else
                {
                    // /Save image to wwwroot/image
                    string wwwRootPath = _webHostEnvironment.WebRootPath;
                    string fileName = Path.GetFileNameWithoutExtension(xe.ImageFile.FileName);
                    string extension = Path.GetExtension(xe.ImageFile.FileName);
                    fileNewName = fileName = fileName + extension;
                    string path = Path.Combine(wwwRootPath + "/Images/", fileName);

                    //  uniqueFileName = Guid.NewGuid().ToString() + "_" + xe.ImageFile.FileName;  

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await xe.ImageFile.CopyToAsync(fileStream);
                    }
                }


            }

            return fileNewName;
        }
    }
}
