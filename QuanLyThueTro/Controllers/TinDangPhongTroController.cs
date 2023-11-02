using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using QuanLyThueTro.Services;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinDangPhongTroController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IExtensionService _extensionService;
        public TinDangPhongTroController(MyDBContext context, IMapper mapper, IExtensionService extensionService)
        {
            _context = context;
            _mapper = mapper;
            _extensionService = extensionService;
        }

        // GET: api/TinDangPhongTro
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TinDangPhongTroVM>>> GetTinDangPhongTros()
        {
            if (_context.tinDangs == null)
            {
                return NotFound();
            }
            List<TinDangPhongTroVM> mergeList = new List<TinDangPhongTroVM>();
            var tinDang = _context.tinDangs.ToList();
            foreach (var item in tinDang)
            {
                var phongTro = _context.phongTros.Where(s => s.idTinDang == item.idTinDang).FirstOrDefault();
                TinDangPhongTroVM dto = new TinDangPhongTroVM(item.idTinDang,
                              item.tieuDe,
                              item.loaiTin,
                              item.sdtNguoiLienHe,
                              item.nguoiLienHe,
                              item.doiTuongChoThue,
                              item.soLuongPhong,
                              diaChi: phongTro.diaChi,
                              giaPhong: phongTro.giaPhong,
                              dienTich: phongTro.dienTich,
                              moTa: phongTro.moTa,
                              tienDien: phongTro.tienDien,
                              tienNuoc: phongTro.tienNuoc,
                              tienDichVu: phongTro.tienDichVu);
                mergeList.Add(dto);
            }
            await _context.DisposeAsync();
            return mergeList.ToList();
        }

        // GET: api/TinDangPhongTro/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TinDangPhongTroVM>> GetTinDangPhongTroById(string id)
        {
            if (_context.tinDangs == null)
            {
                return NotFound();
            }
            var tinDang = await _context.tinDangs.FindAsync(id);

            if (tinDang == null)
            {
                return NotFound();
            }
            var phongTro = _context.phongTros.Where(s => s.idTinDang == tinDang.idTinDang).FirstOrDefault();
            TinDangPhongTroVM dto = new TinDangPhongTroVM(tinDang.idTinDang,
                          tinDang.tieuDe,
                          tinDang.loaiTin,
                          tinDang.sdtNguoiLienHe,
                          tinDang.nguoiLienHe,
                          tinDang.doiTuongChoThue,
                          tinDang.soLuongPhong,
                          diaChi: phongTro.diaChi,
                          giaPhong: phongTro.giaPhong,
                          dienTich: phongTro.dienTich,
                          moTa: phongTro.moTa,
                          tienDien: phongTro.tienDien,
                          tienNuoc: phongTro.tienNuoc,
                          tienDichVu: phongTro.tienDichVu);
            return dto;
        }

        // PUT: api/TinDangPhongTro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TinDangPhongTroWithoutId>> PutTinDangPhongTro(string id, [FromForm] TinDangPhongTroWithoutId tinDangPhongTro)
        {
            var tinDang = await _context.tinDangs.Where(s => s.idTinDang == id).FirstOrDefaultAsync();
            _mapper.Map(tinDangPhongTro, tinDang);
            var phongTro = await _context.phongTros.Where(s => s.idTinDang == id).FirstOrDefaultAsync();
            _mapper.Map(tinDangPhongTro, phongTro);

            _context.Entry(tinDang).State = EntityState.Modified;
            _context.Entry(phongTro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TinDangExists(id))
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

        // POST: api/TinDangPhongTro
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<string>> PostTinDangPhongTro([FromForm] TinDangPhongTroWithoutId tinDangPhongTro)
        {
            TinDang tinDang = _mapper.Map<TinDang>(tinDangPhongTro);
            PhongTro phongTro = _mapper.Map<PhongTro>(tinDangPhongTro);

            if (_context.tinDangs == null || _context.phongTros == null)
            {
                return Problem("Entity set 'MyDBContext.tinDangs' or 'MyDBContext.phongTroes' is null.");
            }
            tinDang.idTinDang = "TD" + _extensionService.AutoPK_Common();
            DateTime today = DateTime.Now;
            tinDang.ngayTaoTin = today; // ngay tao tin
            tinDang.ngayBatDau = today;
            TimeSpan congNgay = new TimeSpan(30,0,0,0);
            tinDang.ngayKetThuc = today.Add(congNgay);
            _context.tinDangs.Add(tinDang);
            try
            {
                await _context.SaveChangesAsync();
                // tới phòng trọ
                phongTro.idTinDang = tinDang.idTinDang;
                _context.phongTros.Add(phongTro);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    throw;
                }

            }
            catch (DbUpdateException)
            {
                if (TinDangExists(tinDang.idTinDang))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
            return tinDang.idTinDang;
        }

        // DELETE: api/TinDangPhongTro/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinDang(string id)
        {
            if (_context.tinDangs == null)
            {
                return NotFound();
            }
            var tinDang = await _context.tinDangs.FindAsync(id);
            var phongTro = await _context.phongTros.FindAsync(id);
            if (tinDang == null || phongTro == null)
            {
                return NotFound();
            }

            _context.tinDangs.Remove(tinDang);
            _context.phongTros.Remove(phongTro);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TinDangExists(string id)
        {
            return (_context.tinDangs?.Any(e => e.idTinDang == id)).GetValueOrDefault();
        }
    }
}
