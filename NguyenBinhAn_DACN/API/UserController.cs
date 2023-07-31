using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NguyenBinhAn_DACN.API.Models;
using NguyenBinhAn_DACN.Data;
using System.Threading.Tasks;
using System.Linq;
using NguyenBinhAn_DACN.Utility;
using Microsoft.AspNetCore.Identity;
using Entities.Entities;

namespace NguyenBinhAn_DACN.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(ApplicationDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(UserModel user)
        {
            var teacher = await _context.Teachers.FindAsync(user.UserName);
            if(teacher != null)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                if(await _userManager.CheckPasswordAsync(currentUser, user.Password))
                {
                    // neu password dung
                    return Ok(new { status = true, userType = true });
                }
                // neu password sai
                return Ok(new { status = false, message = "Login Failed" });
            }

            // dang nhap sinh vien
            var student = await _context.Students.FindAsync(user.UserName);
            if (student != null)
            {
                var currentUser = await _userManager.FindByNameAsync(user.UserName);
                if (await _userManager.CheckPasswordAsync(currentUser, user.Password))
                {
                    
                    return Ok(new { status = true, UserType = false });
                }
                else
                    return Ok(new { status = false, message = "Login Failed" });
            }
            return Ok(new { status = false, message = "Login Failed" });
        }


        [HttpGet("get-all-user")]
        public async Task<IActionResult> GetUsers()
        {
            //var user =from a in _context.ApplicationUsers
            //          where a.Id == "210b24af-ed5a-4f46-9c49-b1788fc5ecb2"
            //          select a;
            IdentityUser user = await _userManager.FindByNameAsync("1911002");
            return Ok(user);
        }

    }
}
