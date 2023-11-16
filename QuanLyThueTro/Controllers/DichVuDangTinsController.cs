using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using QuanLyThueTro.Services;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DichVuDangTinsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IExtensionService _extensionService;
        private GenerateAlphanumericId random;

        public DichVuDangTinsController(MyDBContext context, IMapper mapper, IExtensionService extensionService)
        {
            _context = context;
            _mapper = mapper;
            _extensionService = extensionService;
            random = new GenerateAlphanumericId();
        }

        // GET: api/DichVuDangTins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GoiTinDichVu>>> GetdichVuDangTins()
        {
          if (_context.dichVuDangTins == null)
          {
              return NotFound();
          }
            return await _context.dichVuDangTins.ToListAsync();
        }

        // GET: api/DichVuDangTins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GoiTinDichVu>> GetDichVuDangTin(string id)
        {
          if (_context.dichVuDangTins == null)
          {
              return NotFound();
          }
            var dichVuDangTin = await _context.dichVuDangTins.FindAsync(id);

            if (dichVuDangTin == null)
            {
                return NotFound();
            }

            return dichVuDangTin;
        }

        // PUT: api/DichVuDangTins/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDichVuDangTin(string id, [FromBody] DichVuDangTinDto dichVuDangTinDto)
        {
            GoiTinDichVu dichVuDangTin = _mapper.Map<GoiTinDichVu>(dichVuDangTinDto);
            dichVuDangTin.idDichVu = id;
            if (id != dichVuDangTin.idDichVu)
            {
                return BadRequest();
            }
            if (!_extensionService.IsNotExistNameDichVu(dichVuDangTin))
                return BadRequest("Loại dịch vụ này đã tồn tại!");

            _context.Entry(dichVuDangTin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DichVuDangTinExists(id))
                {
                    return NotFound("Không tìm thấy dịch vụ có mã này!");
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/DichVuDangTins
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GoiTinDichVu>> PostDichVuDangTin([FromBody] DichVuDangTinDto dichVuDangTinDto)
        {
            GoiTinDichVu dichVuDangTin = _mapper.Map<GoiTinDichVu>(dichVuDangTinDto);
          if (_context.dichVuDangTins == null)
          {
              return Problem("Entity set 'MyDBContext.dichVuDangTins'  is null.");
          }
            if (!_extensionService.IsNotExistNameDichVu(dichVuDangTin))
                return BadRequest("Loại dịch vụ đã tồn tại!");

            dichVuDangTin.idDichVu = "DV" + random.GenerateId(5);
            dichVuDangTin.trangThaiSuDung = true;
            _context.dichVuDangTins.Add(dichVuDangTin);
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (DichVuDangTinExists(dichVuDangTin.idDichVu))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetDichVuDangTin", new { id = dichVuDangTin.idDichVu }, dichVuDangTin);
        }

        // DELETE: api/DichVuDangTins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDichVuDangTin(string id)
        {
            if (_context.dichVuDangTins == null)
            {
                return NotFound();
            }
            var dichVuDangTin = await _context.dichVuDangTins.FindAsync(id);
            if (dichVuDangTin == null)
            {
                return NotFound();
            }

            _context.dichVuDangTins.Remove(dichVuDangTin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DichVuDangTinExists(string id)
        {
            return (_context.dichVuDangTins?.Any(e => e.idDichVu == id)).GetValueOrDefault();
        }

        [HttpGet("ValidateDichVu")]
        public async Task<IActionResult> ValidateDichVu(string name)
        {
            if (_context.dichVuDangTins == null)
            {
                return NotFound();
            }
            var dichVuDangTin = await _context.dichVuDangTins.FindAsync(name);
            if (dichVuDangTin == null)
                return Ok(true);
            return Ok(false); ;
        }
    }
}
