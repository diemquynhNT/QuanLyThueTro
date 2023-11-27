using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using QuanLyThueTro.Services;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinDangController : ControllerBase
    {

        private readonly ITinDang _context;
        private readonly IMapper _mapper;
        private readonly IPhotoService _photoService;
        private readonly MyDBContext myDBContext;



        public TinDangController(ITinDang context, IMapper mapper, IPhotoService photoService,MyDBContext _myDBContext)
        {
            _context = context;
            _mapper = mapper;
            _photoService = photoService;
            myDBContext= _myDBContext;

        }
   

        [HttpGet]
        public List<TinDang> GetAll()
        {
            return _context.GetAll();
        }
        [HttpGet("GetTinDangByIdUser")]
        public List<TinDang> GetTinDangByIdUser(string id)
        {
            return _context.GetTinDangByIdUser(id);
        }
        [HttpGet("GetTinDangByStatus")]
        public List<TinDang> GetTinDangByStatus(string id,bool status)
        {
            return _context.GetTinDangByIdUser(id).Where(t=>t.trangThaiTinDang==status).ToList();
        }
        [HttpGet("GetTinDangByService")]
        public List<TinDang> GetTinDangByService(string id, string idGoi)
        {
            return _context.GetTinDangByIdUser(id).Where(t => t.idDichVu == idGoi).ToList();
        }
        [HttpGet("GetTinDangByServiceAndStatus")]
        public List<TinDang> GetTinDangByServiceAndStatus(string id, string idGoi, bool status)
        {
            return _context.GetTinDangByIdUser(id).Where(t => t.idDichVu == idGoi && t.trangThaiTinDang==status).ToList();
        }
        [HttpGet("GetTinByIdTP")]
        public List<TinDangVM> GetTinByIdTP(string thanhpho)
        {
            List<PhongTro> listPhong = myDBContext.phongTros.Where(t => t.diaChi.Contains(thanhpho)).ToList();

            List<string> listIdTinDang = listPhong.Select(p => p.idTinDang).ToList();
            List<TinDang> tinDang = myDBContext.tinDangs.Where(t => listIdTinDang.Contains(t.idTinDang)).ToList();

            List<TinDangVM> listTinVM = new List<TinDangVM>();
            foreach (var item in tinDang)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(item.idTinDang);
                var phong = _context.GetPhongById(item.idTinDang);
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                listTinVM.Add(tinvm);
            }
            return listTinVM;
        }

        [HttpGet("GetTinDangByDiaDiem")]
        public List<TinDangVM> GetTinDangByDiaDiem(string diadiem)
        {
            List<TinDangVM> listTinVM = getListTinDangVMDiaDiem(diadiem);
            return listTinVM;
        }

        private List<TinDangVM> getListTinDangVMDiaDiem(string diadiem)
        {
            List<TinDang> tinDang = _context.GetAll();
            List<TinDangVM> listTinVM = new List<TinDangVM>();
            foreach (var item in tinDang)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(item.idTinDang);
                var phong = _context.GetPhongById(item.idTinDang);
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                if (tinvm.diaChi.Contains(diadiem))
                {
                    listTinVM.Add(tinvm);
                }
            }
            return listTinVM;
        }

        [HttpGet("GetTinDangVMByPrice")]
        public List<TinDangVM> GetTinDangVMByPrice(string diadiem, float giaMin, float giaMax)
        {
            List<PhongTro> listPhong = new List<PhongTro>();
            if (diadiem != "khong")
            {
                listPhong = myDBContext.phongTros.Where(t => t.giaPhong >= giaMin && t.giaPhong <= giaMax && t.diaChi.Contains(diadiem)).ToList();

            }
            else
            {
                listPhong = myDBContext.phongTros.Where(t => t.giaPhong >= giaMin && t.giaPhong <= giaMax).ToList();

            }
            List<string> listIdTinDang = listPhong.Select(p => p.idTinDang).ToList();
            List<TinDang> tinDang = myDBContext.tinDangs.Where(t => listIdTinDang.Contains(t.idTinDang)).ToList();

            List<TinDangVM> listTinVM = new List<TinDangVM>();
            foreach (var item in tinDang)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(item.idTinDang);
                var phong = _context.GetPhongById(item.idTinDang);
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                listTinVM.Add(tinvm);
            }
            return listTinVM;
        }
        [HttpGet("GetTinDangVMByDienTich")]
        public List<TinDangVM> GetTinDangVMByDienTich(string diadiem, double minDienTich, double maxDienTich)
        {
            List<PhongTro> listPhong = new List<PhongTro>();
            if(diadiem!="khong")
            {
                listPhong = myDBContext.phongTros.Where(t => t.dienTich >= minDienTich && t.dienTich <= maxDienTich && t.diaChi.Contains(diadiem)).ToList();

            }
            else
            {
                listPhong = myDBContext.phongTros.Where(t => t.dienTich >= minDienTich && t.dienTich <= maxDienTich).ToList();

            }
            List<string> listIdTinDang = listPhong.Select(p => p.idTinDang).ToList();      
            List<TinDang> tinDang = myDBContext.tinDangs.Where(t => listIdTinDang.Contains(t.idTinDang)).ToList();

            List<TinDangVM> listTinVM = new List<TinDangVM>();
            foreach (var item in tinDang)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(item.idTinDang);
                var phong = _context.GetPhongById(item.idTinDang);
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                listTinVM.Add(tinvm);
            }
            return listTinVM;
        }

        [HttpGet("GetTinDangVMByPriceDienTich")]
        public List<TinDangVM> GetTinDangVMByPriceDienTich(string diadiem, float giaMin, float giaMax, double minDienTich, double maxDienTich)
        {
            List<PhongTro> listPhong = new List<PhongTro>();
            if (diadiem != "khong")
            {
                listPhong = myDBContext.phongTros.Where(t => t.giaPhong >= giaMin && t.giaPhong <= giaMax && t.dienTich >= minDienTich && t.dienTich <= maxDienTich

                && t.diaChi.Contains(diadiem)).ToList();
            }
            else
            {
                listPhong = myDBContext.phongTros.Where(t => t.giaPhong >= giaMin && t.giaPhong <= giaMax && t.dienTich >= minDienTich && t.dienTich <= maxDienTich).ToList();
            }
            List<string> listIdTinDang = listPhong.Select(p => p.idTinDang).ToList();
            List<TinDang> tinDang = myDBContext.tinDangs.Where(t => listIdTinDang.Contains(t.idTinDang)).ToList();

            List<TinDangVM> listTinVM = new List<TinDangVM>();
            foreach (var item in tinDang)
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(item.idTinDang);
                var phong = _context.GetPhongById(item.idTinDang);
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                listTinVM.Add(tinvm);
            }
            return listTinVM;
        }

        [HttpGet("Filter")]
        public List<TinDang> Filter(int thang, bool status)
        {
            return _context.Filter(thang, status);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(id);
                var phong = _context.GetPhongById(id);
                if (tin == null || phong == null)
                    return NotFound();
                _mapper.Map(tin, tinvm);
                _mapper.Map(phong, tinvm);
                return Ok(tinvm);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]
        public async Task<ActionResult> AddTin([FromBody] TinDangModel tin)
        {
            try
            {
                var tinDang = _mapper.Map<TinDang>(tin);
                var phong = _mapper.Map<PhongTro>(tin);
                await _context.AddTinDang(tinDang, phong);
                var u = _context.GetTinDangById(tinDang.idTinDang);
                return Ok(u);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
       
        [HttpPost("AddImageAsync/{tinDangId}")]
        public async Task<ActionResult> AddImageAsync(string tinDangId, List<IFormFile>  files)
        {
            try
            {
                bool tinDang = _context.IsValidTinDang(tinDangId);

                if (!tinDang)
                    return NotFound("Không tìm thấy tin đăng có mã này");
                if (files == null)
                    return BadRequest("Hình null");
                if (files.Count == 0)
                    return BadRequest();

                var images = new Images();
                foreach (var file in files)
                {
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
                    _context.ThemHinh(images);
                }
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        [HttpPost("AddImageToTinDang")]
        public async Task<ActionResult> AddImageToTinDang(List<IFormFile> list,string id )
        {
            try
            {
                if (list.Count == 0)
                    return BadRequest();
                foreach (var f in list)
                {
                    _context.AddHinhanh(id, f);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TinDangModel tinvVM)
        {
            try
            {
                var tinFind =  _context.GetTinDangById(id);
                var phongFind = _context.GetPhongById(id);
                if (tinFind == null)
                    return NotFound();
                _mapper.Map(tinvVM, tinFind);
                _mapper.Map(tinvVM, phongFind);
                await _context.UpdateTinDang(tinFind,phongFind);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }
        [HttpPut("DuyetTin")]
        public async Task<ActionResult> DuyetTin(string id)
        {
            try
            {
                var tin=_context.GetTinDangById(id);
                if (tin == null)
                    return NotFound();
                _context.DuyetTin(tin);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
        [HttpPut("HuyDuyetTin")]
        public async Task<ActionResult> HuyDuyetTin(string id)
        {
            try
            {
                var tin = _context.GetTinDangById(id);
                if (tin == null)
                    return NotFound();
                _context.HuyDuyetTin(tin);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost("GetImage")]
        public async Task<IActionResult> GetImage(string tinId)
        {
            var tinDang =  _context.GetTinDangById(tinId);
            if (tinDang == null)
            {
                return NotFound();
            }
            

            return Ok("thanh cong");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTinDang(string id)
        {
            TinDang tinDang =  _context.GetTinDangById(id);
            if (tinDang == null)
                return BadRequest();
            foreach (var image in myDBContext.ImagesPhongTro)
            {
                if (image.idTinDang == id)
                {
                    await _photoService.DeleteImageAsync(image.publicId);
                }
            }
            _context.DeleteTinDang(id);

            return Ok();
        }

    }
}
