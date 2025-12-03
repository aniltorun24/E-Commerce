using e_CommerceApi.Models.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace e_CommerceApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly Context _context;
        public ProductController(Context context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var products =await _context.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Index(int id)
        {
            if(id == null)
            {
                return NotFound();    
            }
            //var value = await _context.Products.FindAsync(id);
            var value = await _context.Products.FirstOrDefaultAsync(x => x.Id == id);

            if (value == null) 
            {
                return NotFound();
            }
            return Ok(value);
        }
    }
}
