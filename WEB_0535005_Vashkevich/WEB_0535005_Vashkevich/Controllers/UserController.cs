using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WEB_0535005_Vashkevich.Areas.Identity.Data;
using WEB_0535005_Vashkevich.Entities;

namespace WEB_0535005_Vashkevich.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            return View();
        }

        public FileResult GetAvatarFromBytes(byte[] bytesAvatar)
        {
            return File(bytesAvatar, "image/*");
        }

        [HttpGet]
        public async Task<IActionResult> GetAvatar()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return null;
            }
            if (user.Avatar == null) {
                return NotFound();
            }
            FileResult imageUserFile = GetAvatarFromBytes(user.Avatar);
            return imageUserFile;
        }
    }
}
