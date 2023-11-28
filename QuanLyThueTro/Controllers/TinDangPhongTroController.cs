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
        private readonly IPhotoService _photoService;
        public TinDangPhongTroController(MyDBContext context, IMapper mapper, IExtensionService extensionService, IPhotoService photoService)
        {
            _context = context;
            _mapper = mapper;
            _extensionService = extensionService;
            _photoService = photoService;
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
                              item.ngayBatDau,
                              item.ngayKetThuc,
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
                              tienDichVu: phongTro.tienDichVu,
                              item.luotTruyCap,
                              item.idUser,
                              item.trangThaiTinDang,
                              item.idDichVu);
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
            _extensionService.GoUpLuotTruyCap(tinDang);
            Thread.Sleep(200);
            var phongTro = _context.phongTros.Where(s => s.idTinDang == tinDang.idTinDang).FirstOrDefault();
            TinDangPhongTroVM dto = new TinDangPhongTroVM(tinDang.idTinDang,
                          tinDang.tieuDe,
                          tinDang.loaiTin,
                          tinDang.ngayBatDau,
                          tinDang.ngayKetThuc,
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
                          tienDichVu: phongTro.tienDichVu,
                          tinDang.luotTruyCap,
                          tinDang.idUser,
                          tinDang.trangThaiTinDang,
                          tinDang.idDichVu);
            return dto;
        }

        // GET: api/TinDangPhongTro/5
        //Images
        [HttpGet("GetByIdUser/{userId}")]
        public async Task<ActionResult<IEnumerable<TinDangPhongTroVM>>> GetTinDangPTByIdUser(string userId)
        {
            if (_context.tinDangs == null)
            {
                return NotFound();
            }
            var tinDang = _context.tinDangs.Where(t => t.idUser == userId).ToList();

            if (tinDang == null)
            {
                return NotFound();
            }
            List<TinDangPhongTroVM> listTinDangPT = new List<TinDangPhongTroVM>();
            foreach ( var t in tinDang )
            {
                var phongTro = _context.phongTros.Where(s => s.idTinDang == t.idTinDang).FirstOrDefault();
                TinDangPhongTroVM dto = new TinDangPhongTroVM(t.idTinDang,
                              t.tieuDe,
                              t.loaiTin,
                              t.ngayBatDau,
                              t.ngayKetThuc,
                              t.sdtNguoiLienHe,
                              t.nguoiLienHe,
                              t.doiTuongChoThue,
                              t.soLuongPhong,
                              diaChi: phongTro.diaChi,
                              giaPhong: phongTro.giaPhong,
                              dienTich: phongTro.dienTich,
                              moTa: phongTro.moTa,
                              tienDien: phongTro.tienDien,
                              tienNuoc: phongTro.tienNuoc,
                              tienDichVu: phongTro.tienDichVu,
                              t.luotTruyCap,
                              t.idUser,
                              t.trangThaiTinDang,
                              t.idDichVu);
                listTinDangPT.Add(dto);
            }
            
            return listTinDangPT;
        }

        // PUT: api/TinDangPhongTro/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<TinDangPhongTroWithoutId>> PutTinDangPhongTro(string id, [FromBody] TinDangPhongTroWithoutId tinDangPhongTro)
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
        public async Task<ActionResult<TinDang>> PostTinDangPhongTro([FromBody] TinDangPhongTroWithoutId tinDangPhongTro)
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
            return tinDang;
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
            var imagePT = _context.ImagesPhongTro.Where(img => img.idTinDang == id).ToList();
            if (tinDang == null || phongTro == null)
            {
                return NotFound();
            }

            _context.tinDangs.Remove(tinDang);
            _context.phongTros.Remove(phongTro);
            foreach(var image in imagePT)
            {
                await _photoService.DeleteImageAsync(image.publicId);
            }
            await _context.SaveChangesAsync();

            return Ok("Đã xóa tin đăng");
        }
        //Images
        [HttpPost("AddImages/{tinDangId}")]
        public async Task<ActionResult<Images>> AddImageAsync(string tinDangId, IFormFileCollection files)
        {
            bool tindangR = TinDangExists(tinDangId);
            
            if (!tindangR)
                return NotFound("Không tìm thấy tin đăng có mã này");
            if (files == null)
                return BadRequest("Hình null");
            var images = new Images();
            foreach(var file in files)
            {
                //upload on cloud
                var result = await _photoService.AddImageAsync(file);
                if (result.Error != null)
                {
                    return BadRequest("Không lưu lên cloud được: " + result.Error.Message);
                }
                images = new Images
                {
                    nameImage = result.SecureUrl.AbsoluteUri,
                    idTinDang = tinDangId,
                    publicId = result.PublicId
                };
                _context.ImagesPhongTro.Add(images);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    throw;
                }
            }
            return images;
        }

        [HttpGet("GetImages/{idTinDang}")]
        public async Task<ActionResult<List<string>>> GetImageTinDangById(string idTinDang)
        {
            var flag = TinDangExists(idTinDang);

            if (!flag)
            {
                return NotFound("Không tìm thấy tin đăng có mã này");
            }
            List<string> urls = new List<string>();
            foreach(var image in _context.ImagesPhongTro)
            {
                if(image.idTinDang == idTinDang)
                {
                    urls.Add(image.nameImage);
                }
            }
            return urls;
        }
        [HttpDelete("DeleteImages/{idTinDang}")]
        public async Task<IActionResult> DeleteImageAsync(string idTinDang)
        {
            var flag = TinDangExists(idTinDang);

            if (!flag)
            {
                return NotFound("Không tìm thấy tin đăng có mã này");
            }
            foreach (var image in _context.ImagesPhongTro)
            {
                if (image.idTinDang == idTinDang)
                {
                    _context.ImagesPhongTro.Remove(image);
                    await _photoService.DeleteImageAsync(image.publicId);
                }
            }
            await _context.SaveChangesAsync();

            return Ok("Đã xóa tin đăng");
        }
        private bool TinDangExists(string id)
        {
            return (_context.tinDangs?.Any(e => e.idTinDang == id)).GetValueOrDefault();
        }
    }
}
