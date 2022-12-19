using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext _context;

        public CreateModel(WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Album Album { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Albums.Add(Album);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
