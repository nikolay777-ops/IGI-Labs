using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;
using WEB_0535005_Vashkevich.Extensions;
using WEB_0535005_Vashkevich.Models;
using WEB_0535005_Vashkevich.Services;

namespace WEB_0535005_Vashkevich.Controllers
{
    public class ProductController : Controller
    {
        private FileUpload fileProvider = new FileUpload();
        private readonly ApplicationDbContext _context;
        private int _pageSize;

        public ProductController(ApplicationDbContext context) 
        {
            _context = context;
            _pageSize = 3;
        }

        [Route("Catalog")]
        [Route("Catalog/Page_{pageNo:int=1}")]
        public async Task<IActionResult> Index(int? group, int pageNo=1)
        {
             List<Album> allAlbums = null;
            /*var result = FillAlbums();
            foreach (var item in result.Item1)
            {
                _context.Albums.Add(item);
            }

            await _context.SaveChangesAsync();
            */

            

            var filtered = ListViewModel<Album>.GetModel(
                _context.Albums.Include(x => x.Category).ToListAsync().Result.AsQueryable(),
                pageNo,
                _pageSize,
                d => !group.HasValue || group.Value == 0 || d.Category.Id == group.Value
                );
                
            ViewData["Groups"] = _context.AlbumCategories.ToListAsync().Result;
            ViewData["CurrentGroup"] = group ?? 0;

            
            if (Request.IsAjax())
                return PartialView("_ListPartial", filtered);
            else
            {
                return View(filtered);
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context == null)
            {
                return NotFound();
            }

            var album = await _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            return View(album);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name, Description, Price, Duration, Image, Category")] Album alb, FileUpload fp)
        {
            if (id == null)
            {
                return NotFound();
            }
            var album = _context.Albums.FindAsync(id);
            if (album == null)
            {
                return NotFound();
            }
            if (album.Result != null)
            {
                album.Result.Price = alb.Price;
                album.Result.Category = alb.Category;
                album.Result.Description = alb.Description;
                album.Result.Name = alb.Name;
                album.Result.Image = alb.Image;
                album.Result.Duration = alb.Duration;
            }
            _context.Update(album.Result);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            
            //return View(alb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, Description, Price, Duration, Image")] Album alb, FileUpload fp) {
            if (ModelState.IsValid)
            {
                _context.Add(alb);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alb);
        }

        public IActionResult Create()
        {
            return View();
        }

        public Tuple<List<Album>, List<AlbumCategory>> FillAlbums() {

            List<AlbumCategory> albumCategories = _context.AlbumCategories.ToListAsync().Result; 

            List<Album> albums = new List<Album> {
                new Album
                {
                    Name = "Ready to Die",
                    Category = albumCategories[0],
                    Description="Debute album of american rapper Notorious B.I.G.\nWhich was released on label Bad Boy Records",
                    Price = (float)9.99,
                    Duration = new TimeSpan(0, 59, 34),
                    Image = "ready-to-die.jpg",
                },
                new Album
                {
                    Name = "Yeezus",
                    Category = albumCategories[0],
                    Description="The sixth album of Kanye West.\nYeezus was published on Def Jam Records and Roc-A-Fella Records",
                    Price = (float)12.99,
                    Duration = new TimeSpan(0, 45, 27),
                    Image = "yeezus.jpg",
                },
                new Album
                {
                    Name = "The Chronic",
                    Category = albumCategories[0],
                    Description="Debute album of american rapper and producer Dr.Dre.\nReleased on Death Row Records",
                    Price = (float)11.99,
                    Duration = new TimeSpan(0, 62, 52),
                    Image = "the-chronic.jpg",
                },
                new Album
                {
                    Name = "Thriller",
                    Category = albumCategories[1],
                    Description="Thriller - the sixth studio album of american pop-king Michael Jackson.\nReleased on Epic Records Label",
                    Price = (float)8.99,
                    Duration = new TimeSpan(0, 42, 19),
                    Image = "thriller.jpg",
                },
                new Album
                {
                    Name = "The Abbey Road",
                    Category = albumCategories[1],
                    Description="12th studio album of British rock-group The Beatls.",
                    Price = (float)8.99,
                    Duration = new TimeSpan(0, 47, 03),
                    Image = "abbey-road.jpg",
                },
                new Album
                {
                    Name = "Dirty Mind",
                    Category = albumCategories[1],
                    Description="3th studio album of american singer and musician Prince",
                    Price = (float)8.99,
                    Duration = new TimeSpan(0, 42, 19),
                    Image = "dirty-mind.jpg",
                },
            };

            return new Tuple<List<Album>, List<AlbumCategory>>(albums, albumCategories);
        }
    }
}
