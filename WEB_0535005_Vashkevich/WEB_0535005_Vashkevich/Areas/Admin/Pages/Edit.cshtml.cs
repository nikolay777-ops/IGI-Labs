using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;
using System.Web;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WEB_0535005_Vashkevich.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext _context;

        public EditModel(WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Album Album { get; set; } = default!;

        [Display(Name = "Avatar")]
        public IFormFile? FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Albums == null)
            {
                return NotFound();
            }

            var album =  await _context.Albums.FirstOrDefaultAsync(m => m.Id == id);
            if (album == null)
            {
                return NotFound();
            }
            Album = album;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            IEnumerable<AlbumCategory> categories = _context.AlbumCategories.ToList().Where(d => d.Id == Album.Category.Id);

            Album.Category = categories.First();
            Album.Image = "lox.jpg";

            ModelState.Clear(); // ValidationState("Album.Category.CategoryName");

            if (!TryValidateModel(Album))
            {
                var a = ModelState;
                return Page();
            }

            _context.Attach(Album).State = EntityState.Modified;

            try
            {
                string filename = FileUpload.FileName;
                filename = Path.GetFileName(filename);
                string uploadPath = Path.Combine("wwwroot\\images\\albums", Album.Id.ToString());
                var stream = new FileStream(uploadPath, FileMode.Create);
                FileUpload.CopyToAsync(stream);
            }
            catch  
            {
                return Page();
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlbumExists(Album.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool AlbumExists(int id)
        {
          return _context.Albums.Any(e => e.Id == id);
        }
    }
}
