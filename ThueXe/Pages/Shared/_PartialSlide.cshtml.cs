using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCar.Areas.Admin.Models.EF;

namespace ThueXe.Pages.Shared
{
    public class _PartialSlideModel : PageModel
    {
        private readonly CarWebDbContext _context;
        public void OnGet()
        {
          
        }
    }
}
