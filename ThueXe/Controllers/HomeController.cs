using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using ThueXe.Models;
using WebCar.Areas.Admin.Models.EF;
using WebCar.Areas.Admin.Models.Entities;

namespace ThueXe.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly CarWebDbContext _context;
        public HomeController(ILogger<HomeController> logger, CarWebDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            //get all list
            var carWebDbContext = _context.Xe.Include(x => x.DanhMuc).AsNoTracking();

            //get ramdo 2 product
            ViewBag.Slide = _context.Xe.OrderBy(r => Guid.NewGuid()).Take(2);
            ViewBag.ViewCount = _context.Xe.OrderByDescending(r => r.LuotXem).Take(10); // get product viewcount top 10




            return View(await carWebDbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ChiTietXe(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            var car = await _context.Xe
                 .Include(x => x.DanhMuc)
                 .FirstOrDefaultAsync(m => m.CarId == id);

            car.LuotXem++;
            await _context.SaveChangesAsync();

            if (car == null)
            {
                return NotFound();
            }

            return View(car);

        }


        public IActionResult About()
        {
            return View();
        }
        public async Task<IActionResult> Blog()
        {

            return View(await _context.Blogs.ToListAsync());

        }
        public async Task<IActionResult> XemChiTiet(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.BlogId == id);
            if (blog == null)
            {
                return NotFound();
            }

            return View(blog);

        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Single()
        {
            return View();
        }
        public IActionResult Xebantai()
        {
            return View();
        }
        public IActionResult Xecau()
        {
            return View();
        }
        public IActionResult Xetai()
        {
            return View();

        }
        public IActionResult Xedaukeo()
        {
            return View();

        }

        public IActionResult XeRanger()
        {
            return View();
        }
        public IActionResult XeHilux()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}
