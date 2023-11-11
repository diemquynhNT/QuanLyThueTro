using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using QuanLyThueTro.Services;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LichXemPhongsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IExtensionService _extension;

        public LichXemPhongsController(MyDBContext context, IExtensionService extension)
        {
            _context = context;
            _extension = extension;
        }

        // GET: api/LichXemPhongs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LichXemPhong>>> GetlichXemPhongs()
        {
          if (_context.lichXemPhongs == null)
          {
              return NotFound();
          }
            return await _context.lichXemPhongs.ToListAsync();
        }

        // GET: api/LichXemPhongs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LichXemPhong>> GetLichXemPhong(string id)
        {
          if (_context.lichXemPhongs == null)
          {
              return NotFound();
          }
            var lichXemPhong = await _context.lichXemPhongs.FindAsync(id);

            if (lichXemPhong == null)
            {
                return NotFound();
            }

            return lichXemPhong;
        }

        [HttpGet("GetByIdTinDang/{id}")]
        public async Task<ActionResult<List<LichXemPhong>>> GetLichXemPhongByIdTinDang(string idTinDang)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }
            var lichXemPhong = _context.lichXemPhongs.Where(s => s.idTinDang == idTinDang).ToList();

            if (lichXemPhong == null)
            {
                return NotFound();
            }

            return lichXemPhong;
        }

        // PUT: api/LichXemPhongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichXemPhong(string id, [FromBody] LichXemPhongDto lichXemPhong)
        {
            if (id != lichXemPhong.idLichXem)
            {
                return BadRequest();
            }
            
            _context.Entry(lichXemPhong).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LichXemPhongExists(id))
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

        // POST: api/LichXemPhongs
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LichXemPhong>> PostLichXemPhong([FromBody] LichXemPhong lichXemPhong)
        {
          if (_context.lichXemPhongs == null)
          {
              return Problem("Entity set 'MyDBContext.lichXemPhongs'  is null.");
          }
            lichXemPhong.idLichXem = _extension.AutoPK_Common();
            _context.lichXemPhongs.Add(lichXemPhong);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LichXemPhongExists(lichXemPhong.idLichXem))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLichXemPhong", new { id = lichXemPhong.idLichXem }, lichXemPhong);
        }

        // DELETE: api/LichXemPhongs/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLichXemPhong(string id)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }
            var lichXemPhong = await _context.lichXemPhongs.FindAsync(id);
            if (lichXemPhong == null)
            {
                return NotFound();
            }

            _context.lichXemPhongs.Remove(lichXemPhong);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LichXemPhongExists(string id)
        {
            return (_context.lichXemPhongs?.Any(e => e.idLichXem == id)).GetValueOrDefault();
        }
    }
}
