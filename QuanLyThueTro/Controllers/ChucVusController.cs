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
    public class ChucVusController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IExtensionService _extensionService;

        public ChucVusController(MyDBContext context, IMapper mapper, IExtensionService extensionService)
        {
            _context = context;
            _mapper = mapper;
            _extensionService = extensionService;
        }

        // GET: api/ChucVus
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ChucVu>>> GetchucVus()
        {
          if (_context.chucVus == null)
          {
              return NotFound();
          }
            return await _context.chucVus.ToListAsync();
        }

        // GET: api/ChucVus/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ChucVu>> GetChucVu(string id)
        {
            
          if (_context.chucVus == null)
          {
              return NotFound();
          }
            var chucVu = await _context.chucVus.FindAsync(id);
            
            if (chucVu == null)
            {
                return NotFound();
            }
            ChucVu chucvuDto = _mapper.Map<ChucVu>(chucVu);
            return chucvuDto;
        }

        // PUT: api/ChucVus/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChucVu(string id, [FromForm] ChucVuDto chucVuDto)
        {
            ChucVu chucVu = _mapper.Map<ChucVu>(chucVuDto);
            chucVu.idChucVu = id;
            if (id != chucVu.idChucVu)
            {
                return BadRequest();
            }
            if (!_extensionService.IsNotExistNameChucVu(chucVu))
            {
                return BadRequest("Tên chức vụ đã tồn tại!");
            }
            _context.Entry(chucVu).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChucVuExists(id))
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

        // POST: api/ChucVus
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChucVu>> PostChucVu(ChucVuDto chucVuDto)
        {
            ChucVu chucVu = _mapper.Map<ChucVu>(chucVuDto);
            if (_context.chucVus == null)
          {
              return Problem("Entity set 'MyDBContext.chucVus'  is null.");
          }
            //_extensionService.AutoPK_ChucVu(chucVu);
            if(!_extensionService.IsNotExistNameChucVu(chucVu))
                return BadRequest("Tên chức vụ đã tồn tại!");
            _context.chucVus.Add(chucVu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ChucVuExists(chucVu.idChucVu))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetChucVu", new { id = chucVu.idChucVu }, chucVu);
        }

        // DELETE: api/ChucVus/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChucVu(string id)
        {
            if (_context.chucVus == null)
            {
                return NotFound();
            }
            var chucVu = await _context.chucVus.FindAsync(id);
            if (chucVu == null)
            {
                return NotFound();
            }

            _context.chucVus.Remove(chucVu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChucVuExists(string id)
        {
            return (_context.chucVus?.Any(e => e.idChucVu == id)).GetValueOrDefault();
        }
    }
}
