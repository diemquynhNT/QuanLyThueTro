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
    public class PhongTroesController : ControllerBase
    {
        private readonly MyDBContext _context;

        public PhongTroesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/PhongTroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PhongTro>>> GetphongTros()
        {
          if (_context.phongTros == null)
          {
              return NotFound();
          }
            return await _context.phongTros.ToListAsync();
        }

        // GET: api/PhongTroes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PhongTro>> GetPhongTro(string id)
        {
          if (_context.phongTros == null)
          {
              return NotFound();
          }
            var phongTro = await _context.phongTros.FindAsync(id);

            if (phongTro == null)
            {
                return NotFound();
            }

            return phongTro;
        }

        //// PUT: api/PhongTroes/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhongTro(string id, PhongTro phongTro)
        {
            if (id != phongTro.idTinDang)
            {
                return BadRequest();
            }

            _context.Entry(phongTro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhongTroExists(id))
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

        //// POST: api/PhongTroes
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<PhongTro>> PostPhongTro(PhongTro phongTro)
        {
            if (_context.phongTros == null)
            {
                return Problem("Entity set 'MyDBContext.phongTros'  is null.");
            }
            _context.phongTros.Add(phongTro);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PhongTroExists(phongTro.idTinDang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPhongTro", new { id = phongTro.idTinDang }, phongTro);
        }

        //// DELETE: api/PhongTroes/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeletePhongTro(string id)
        //{
        //    if (_context.phongTros == null)
        //    {
        //        return NotFound();
        //    }
        //    var phongTro = await _context.phongTros.FindAsync(id);
        //    if (phongTro == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.phongTros.Remove(phongTro);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        private bool PhongTroExists(string id)
        {
            return (_context.phongTros?.Any(e => e.idTinDang == id)).GetValueOrDefault();
        }
    }
}
