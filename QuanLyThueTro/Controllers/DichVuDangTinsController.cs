﻿using System;
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
    public class DichVuDangTinsController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IExtensionService _extensionService;

        public DichVuDangTinsController(MyDBContext context, IMapper mapper, IExtensionService extensionService)
        {
            _context = context;
            _mapper = mapper;
            _extensionService = extensionService;
        }

        // GET: api/DichVuDangTins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DichVuDangTin>>> GetdichVuDangTins()
        {
          if (_context.dichVuDangTins == null)
          {
              return NotFound();
          }
            return await _context.dichVuDangTins.ToListAsync();
        }

        // GET: api/DichVuDangTins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DichVuDangTin>> GetDichVuDangTin(string id)
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
        public async Task<IActionResult> PutDichVuDangTin(string id, [FromForm] DichVuDangTinDto dichVuDangTinDto)
        {
            DichVuDangTin dichVuDangTin = _mapper.Map<DichVuDangTin>(dichVuDangTinDto);
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
        public async Task<ActionResult<DichVuDangTin>> PostDichVuDangTin([FromForm] DichVuDangTinDto dichVuDangTinDto)
        {
            DichVuDangTin dichVuDangTin = _mapper.Map<DichVuDangTin>(dichVuDangTinDto);
          if (_context.dichVuDangTins == null)
          {
              return Problem("Entity set 'MyDBContext.dichVuDangTins'  is null.");
          }
            if (!_extensionService.IsNotExistNameDichVu(dichVuDangTin))
                return BadRequest("Loại dịch vụ đã tồn tại!");

            _extensionService.AutoPK_DichVu(dichVuDangTin);
            _context.dichVuDangTins.Add(dichVuDangTin);
            try
            {
                await _context.SaveChangesAsync();
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
    }
}