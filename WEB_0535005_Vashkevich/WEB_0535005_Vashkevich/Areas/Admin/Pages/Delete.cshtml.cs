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
    public class DeleteModel : PageModel
    {
        private readonly WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext _context;

        public DeleteModel(WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }
            var album = await _context.Albums.FindAsync(id);

            if (album != null)
            {
                Album = album;
                _context.Albums.Remove(Album);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
