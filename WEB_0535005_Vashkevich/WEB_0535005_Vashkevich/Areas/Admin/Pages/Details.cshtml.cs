using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext _context;

        public DetailsModel(WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

      public Album Album { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            else 
            {
                Album = album;
            }
            return Page();
        }
    }
}
