using Microsoft.AspNetCore.Identity;
using System.Linq;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Areas.Identity.Data
{
    public interface IDbInitializer
    {
        public void Initialize();
    }

    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
    
        public DbInitializer(ApplicationDbContext context, 
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async void Initialize()
        {
            await _context.Database.EnsureCreatedAsync();
            if (!_context.AlbumCategories.Any()) 
            {
                _context.AlbumCategories.Add(new AlbumCategory { CategoryName = "Rap" });
                _context.AlbumCategories.Add(new AlbumCategory { CategoryName = "Pop" });
                await _context.SaveChangesAsync();
            }
            
            
            if (_context.Users.Any())
            {
                return;
            }

            await _roleManager.CreateAsync(new IdentityRole("admin"));
            await _roleManager.CreateAsync(new IdentityRole("user"));

            string pass = "12341234";

            ApplicationUser admin = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@gmail.com",
                EmailConfirmed = true
            };

            ApplicationUser user = new ApplicationUser
            {
                UserName = "User",
                Email = "user@gmail.com",
                EmailConfirmed = true
            };

            var result =  await _userManager.CreateAsync(admin, pass);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(admin, "admin");
            }
            result = await _userManager.CreateAsync(user, pass);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "user");
            }

            await _context.SaveChangesAsync();
        }
    }
}
