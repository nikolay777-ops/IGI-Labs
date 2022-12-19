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
    public class IndexModel : PageModel
    {
        private readonly WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext _context;

        public IndexModel(WEB_0535005_Vashkevich.Areas.Identity.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Album> Album { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Albums != null)
            {
                Album = await _context.Albums.Include(x => x.Category).ToListAsync();
            }
        }
    }
}
