using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
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
    public class LoaiTaiKhoansController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IExtensionService _extensionService;
        private readonly IMapper _mapper;

        public LoaiTaiKhoansController(MyDBContext context, IExtensionService extensionService, IMapper mapper)
        {
            _context = context;
            _extensionService = extensionService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoaiTaiKhoan>>> GetloaiTaiKhoans()
        {
          if (_context.loaiTaiKhoans == null)
          {
              return NotFound();
          }
            return await _context.loaiTaiKhoans.ToListAsync();
        }

        // GET: api/LoaiTaiKhoans/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LoaiTaiKhoan>> GetLoaiTaiKhoan(string id)
        {
          if (_context.loaiTaiKhoans == null)
          {
              return NotFound();
          }
            var loaiTaiKhoan = await _context.loaiTaiKhoans.FindAsync(id);

            if (loaiTaiKhoan == null)
            {
                return NotFound();
            }

            return loaiTaiKhoan;
        }

        // PUT: api/LoaiTaiKhoans/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLoaiTaiKhoan(string id, [FromBody] LoaiTaiKhoanDto loaiTaiKhoanDto)
        {
            LoaiTaiKhoan loaiTaiKhoan = _mapper.Map<LoaiTaiKhoan>(loaiTaiKhoanDto);
            loaiTaiKhoan.idLoaiTK = id;
            if (id != loaiTaiKhoan.idLoaiTK)
            {
                return BadRequest();
            }
            if (!_extensionService.IsNotExistNameLoaiTK(loaiTaiKhoan))
                return BadRequest("Tên loại tài khoản này đã tồn tại!");
            
            _context.Entry(loaiTaiKhoan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoaiTaiKhoanExists(id))
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

        // POST: api/LoaiTaiKhoans
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LoaiTaiKhoan>> PostLoaiTaiKhoan([FromBody] LoaiTaiKhoanDto loaiTaiKhoanDto)
        {
            LoaiTaiKhoan loaiTaiKhoan = _mapper.Map<LoaiTaiKhoan>(loaiTaiKhoanDto);
          if (_context.loaiTaiKhoans == null)
          {
              return Problem("Entity set 'MyDBContext.loaiTaiKhoans'  is null.");
          }
            //if (!_extensionService.IsNotExistNameLoaiTK(loaiTaiKhoan))
            //    return BadRequest("Tên loại tài khoản đã tồn tại!");
            _extensionService.AutoPK_LoaiTK(loaiTaiKhoan) ;
            loaiTaiKhoan.trangThaiSuDung = true;
            _context.loaiTaiKhoans.Add(loaiTaiKhoan);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (LoaiTaiKhoanExists(loaiTaiKhoan.idLoaiTK))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetLoaiTaiKhoan", new { id = loaiTaiKhoan.idLoaiTK }, loaiTaiKhoan);
        }

        // DELETE: api/LoaiTaiKhoans/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLoaiTaiKhoan(string id)
        {
            if (_context.loaiTaiKhoans == null)
            {
                return NotFound();
            }
            var loaiTaiKhoan = await _context.loaiTaiKhoans.FindAsync(id);
            if (loaiTaiKhoan == null)
            {
                return NotFound();
            }

            _context.loaiTaiKhoans.Remove(loaiTaiKhoan);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoaiTaiKhoanExists(string id)
        {
            return (_context.loaiTaiKhoans?.Any(e => e.idLoaiTK == id)).GetValueOrDefault();
        }
    }
}
