using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GiaoDichesController : ControllerBase
    {
        private readonly MyDBContext _context;

        public GiaoDichesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/GiaoDiches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GiaoDich>>> GetgiaoDiches()
        {
          if (_context.giaoDiches == null)
          {
              return NotFound();
          }
            return await _context.giaoDiches.ToListAsync();
        }

        // GET: api/GiaoDiches/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GiaoDich>> GetGiaoDich(string id)
        {
          if (_context.giaoDiches == null)
          {
              return NotFound();
          }
            var giaoDich = await _context.giaoDiches.FindAsync(id);

            if (giaoDich == null)
            {
                return NotFound();
            }

            return giaoDich;
        }

        // POST: api/GiaoDiches
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GiaoDich>> PostGiaoDich(GiaoDich giaoDich)
        {
          if (_context.giaoDiches == null)
          {
              return Problem("Entity set 'MyDBContext.giaoDiches'  is null.");
          }
            _context.giaoDiches.Add(giaoDich);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (GiaoDichExists(giaoDich.idGiaoDich))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetGiaoDich", new { id = giaoDich.idGiaoDich }, giaoDich);
        }

        private bool GiaoDichExists(string id)
        {
            return (_context.giaoDiches?.Any(e => e.idGiaoDich == id)).GetValueOrDefault();
        }
    }
}
