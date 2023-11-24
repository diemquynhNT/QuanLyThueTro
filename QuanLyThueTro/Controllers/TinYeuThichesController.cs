using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinYeuThichesController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        public TinYeuThichesController(MyDBContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TinYeuThich>>> GettinYeuThiches()
        {
          if (_context.tinYeuThiches == null)
          {
              return NotFound();
          }
            return await _context.tinYeuThiches.ToListAsync();
        }
        [HttpGet("GettinYeuThichesByIdUser")]
        public async Task<ActionResult<IEnumerable<TinDangVM>>> GettinYeuThichesByIdUser(string iduser)
        {
            if (_context.tinYeuThiches == null)
            {
                return NotFound();
            }
            List<TinYeuThich> tinYeuThich= await _context.tinYeuThiches.Where(t=>t.idUser==iduser).ToListAsync();
            List<TinDangVM> listTin = new List<TinDangVM>();
            foreach (var item in tinYeuThich)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.tinDangs.Where(t => t.idTinDang == item.idTinDang).FirstOrDefault();
                var phong = _context.phongTros.Where(t => t.idTinDang == item.idTinDang).FirstOrDefault();
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                listTin.Add(tinvm);
            }
            return listTin;
        }
        // GET: api/TinYeuThiches/5
        [HttpGet("GetTin")]
        public async Task<ActionResult<TinYeuThich>> GetTinYeuThich(string id,string idUser)
        {
          if (_context.tinYeuThiches == null)
          {
              return NotFound();
          }
            var tinYeuThich =  _context.tinYeuThiches.Where(t=>t.idTinDang==id&&t.idUser==idUser).FirstOrDefault();

            if (tinYeuThich == null)
            {
                return NotFound();
            }

            return tinYeuThich;
        }

        // PUT: api/TinYeuThiches/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutTinYeuThich(string id, TinYeuThich tinYeuThich)
        //{
        //    if (id != tinYeuThich.idTinDang)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(tinYeuThich).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TinYeuThichExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

      

        // POST: api/TinYeuThiches
        [HttpPost]
        public async Task<ActionResult<TinYeuThich>> PostTinYeuThich(string idTin,string idUser)
        {
          if (_context.tinYeuThiches == null)
          {
              return Problem("Entity set 'MyDBContext.tinYeuThiches'  is null.");
          }
            TinYeuThich tin = new TinYeuThich();
            tin.idTinDang=idTin;
            tin.idUser = idUser;
            _context.tinYeuThiches.Add(tin);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TinYeuThichExists(tin.idTinDang,tin.idUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Ok(tin);
        }
       

        // DELETE: api/TinYeuThiches/5
        [HttpDelete("DeleteTinYeuThich")]
        public async Task<IActionResult> DeleteTinYeuThich(string idTin,string idUser)
        {
            if (_context.tinYeuThiches == null)
            {
                return NotFound();
            }
            var tinYeuThich =  _context.tinYeuThiches.Where(t=>t.idTinDang==idTin&&idUser==t.idUser).FirstOrDefault();
            if (tinYeuThich == null)
            {
                return NotFound();
            }

            _context.tinYeuThiches.Remove(tinYeuThich);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TinYeuThichExists(string id,string idUser)
        {
            return (_context.tinYeuThiches?.Any(e => e.idTinDang == id &&e.idUser==idUser)).GetValueOrDefault();
        }
        [HttpGet("TinYeuThichExists")]
        public async Task<IActionResult> Validate(string id, string idUser)
        {
            return Ok(TinYeuThichExists(id, idUser));
        }
    }
}
