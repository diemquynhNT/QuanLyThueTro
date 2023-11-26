using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
        private readonly IMapper _mapper;

        public LichXemPhongsController(MyDBContext context, IExtensionService extension, IMapper mapper)
        {
            _context = context;
            _extension = extension;
            _mapper = mapper;
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
        [HttpGet("GetLichXemByIdTin")]
        public async Task<ActionResult<IEnumerable<LichXemPhong>>> GetLichXemByIdTin(string id)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }
            return await _context.lichXemPhongs.Where(t=>t.idTinDang==id).ToListAsync();
        }

        [HttpGet("GetLichXemByIdUser/{sdt}")]
        public async Task<ActionResult<IEnumerable<LichXemPhong>>> GetLichXemByIdUser(string sdt)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }
            return await _context.lichXemPhongs.Where(t => t.sdtNguoiXem == sdt).ToListAsync();
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
        [HttpGet("Count/{idTin}")]
        public async Task<ActionResult<int>> CountLichXem(string idTin)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }
            List<LichXemPhong> list=_context.lichXemPhongs.Where(t=>t.idTinDang == idTin).ToList();

            return list.Count();
        }

        [HttpGet("GetByIdTinDang/{idTinDang}")]
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

        [HttpGet("GetByDay/{ngayXem}")]
        public async Task<ActionResult<List<LichXemPhong>>> GetLichXemPhongByDay(DateTime ngayXem)
        {
            if (_context.lichXemPhongs == null)
            {
                return NotFound();
            }

            var lichXemPhong = _context.lichXemPhongs.Where(s => s.ngayXem.Date == ngayXem.Date).ToList();

            if (lichXemPhong == null)
            {
                return NotFound();
            }

            return lichXemPhong;
        }

        // PUT: api/LichXemPhongs/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLichXemPhong(string id, [FromBody] LichXemPhongDto lichXemPhongdto)
        {
            LichXemPhong lichXemPhong = _mapper.Map<LichXemPhong>(lichXemPhongdto);
            lichXemPhong.idLichXem = id;
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
        public async Task<ActionResult<LichXemPhong>> PostLichXemPhong([FromBody] LichXemPhongDto lichXemPhongdto)
        {
          if (_context.lichXemPhongs == null)
          {
              return Problem("Entity set 'MyDBContext.lichXemPhongs'  is null.");
          }
            LichXemPhong lichXemPhong = _mapper.Map<LichXemPhong>(lichXemPhongdto);
            lichXemPhong.idLichXem = _extension.AutoPK_Common() + _extension.AutoPK_IntCommon();
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
        [HttpPut("DuyetLich/{id}")]
        public async Task<ActionResult<LichXemPhong>> DuyetLich(string id)
        {
            if (_context.lichXemPhongs == null)
            {
                return Problem("Entity set 'MyDBContext.lichXemPhongs'  is null.");
            }
            LichXemPhong lich = _context.lichXemPhongs.Find(id);
            if (lich == null)
                return NotFound();
            lich.trangThai = true;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LichXemPhongExists(lich.idLichXem))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLichXemPhong", new { id = lich.idLichXem }, lich);
        }
        [HttpPut("HuyLich/{id}")]
        public async Task<ActionResult<LichXemPhong>> HuyLich([FromForm] string lydo, string id)
        {
            if (_context.lichXemPhongs == null)
            {
                return Problem("Entity set 'MyDBContext.lichXemPhongs'  is null.");
            }
            LichXemPhong lich = _context.lichXemPhongs.Find(id);
            if (lich == null)
                return BadRequest();
            lich.LyDo = lydo;
            lich.trangThai = false;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LichXemPhongExists(lich.idLichXem))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLichXemPhong", new { id = lich.idLichXem }, lich);
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
            try
            {
                _context.lichXemPhongs.Remove(lichXemPhong);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, ex.Message);
            }
            

            return NoContent();
        }

        private bool LichXemPhongExists(string id)
        {
            return (_context.lichXemPhongs?.Any(e => e.idLichXem == id)).GetValueOrDefault();
        }
    }
}
