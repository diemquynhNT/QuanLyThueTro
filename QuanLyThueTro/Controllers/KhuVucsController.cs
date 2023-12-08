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
    public class KhuVucsController : ControllerBase
    {
        private readonly MyDBContext _context;

        public KhuVucsController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/KhuVucs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<KhuVuc>>> GetkhuVucs()
        {
          if (_context.khuVucs == null)
          {
              return NotFound();
          }
            return await _context.khuVucs.ToListAsync();
        }
        [HttpGet("GetKhuVucsByIdTP")]
        public async Task<ActionResult<IEnumerable<KhuVuc>>> GetKhuVucsByIdTP(string id)
        {
            if (_context.khuVucs == null)
            {
                return NotFound();
            }
            return await _context.khuVucs.Where(t=>t.idThanhPho==id).ToListAsync();
        }

        // GET: api/KhuVucs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<KhuVuc>> GetKhuVuc(string id)
        {
          if (_context.khuVucs == null)
          {
              return NotFound();
          }
            var khuVuc = await _context.khuVucs.FindAsync(id);

            if (khuVuc == null)
            {
                return NotFound();
            }

            return khuVuc;
        }

        // PUT: api/KhuVucs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutKhuVuc(string id, KhuVuc khuVuc)
        {
            if (id != khuVuc.idKhuVuc)
            {
                return BadRequest();
            }

            _context.Entry(khuVuc).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KhuVucExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/KhuVucs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<KhuVuc>> PostKhuVuc(KhuVuc khuVuc)
        {
          if (_context.khuVucs == null)
          {
              return Problem("Entity set 'MyDBContext.khuVucs'  is null.");
          }
            _context.khuVucs.Add(khuVuc);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (KhuVucExists(khuVuc.idKhuVuc))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetKhuVuc", new { id = khuVuc.idKhuVuc }, khuVuc);
        }

        // DELETE: api/KhuVucs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteKhuVuc(string id)
        {
            if (_context.khuVucs == null)
            {
                return NotFound();
            }
            var khuVuc = await _context.khuVucs.FindAsync(id);
            if (khuVuc == null)
            {
                return NotFound();
            }

            _context.khuVucs.Remove(khuVuc);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool KhuVucExists(string id)
        {
            return (_context.khuVucs?.Any(e => e.idKhuVuc == id)).GetValueOrDefault();
        }
    }
}
