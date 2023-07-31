using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NguyenBinhAn_DACN.Data;
using Entities.Entities;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace NguyenBinhAn_DACN.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AddressController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-district-list")]
        public async Task<IActionResult> GetDistrictList(string ProvinceId)
        {
            var list = await (from d in _context.Districts 
                        where d.ProvinceId == ProvinceId select d).ToListAsync();
            var pList = new SelectList(list, "DistrictId", "Name");
            return Ok(pList);
        }
        [HttpGet("get-ward-list")]
        public async Task<IActionResult> GetWardList(string DistrictId)
        {
            var list = await (from d in _context.Wards
                              where d.DistrictId == DistrictId
                              select d).ToListAsync();
            var pList = new SelectList(list, "WardId", "Name");
            return Ok(pList);
        }
    }
}
