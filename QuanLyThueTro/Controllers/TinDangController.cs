using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using QuanLyThueTro.Data;
using QuanLyThueTro.Dto;
using QuanLyThueTro.Model;
using QuanLyThueTro.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TinDangController : ControllerBase
    {

        private readonly ITinDang _context;
        private readonly IMapper _mapper;

        public TinDangController(ITinDang context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public List<TinDang> GetAll()
        {
            return _context.GetAll();
        }

        // GET api/<TinDangController>/5
        //Detail
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            try
            {
                TinDangVM tinvm = new TinDangVM();
                var tin = _context.GetTinDangById(id);
                var phong = _context.GetPhongById(id);
                if(tin==null || phong ==null)
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
                await _context.AddTinDang(tinDang,phong);
                return Ok("tao thanh cong");
            }
            catch(Exception ex)
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
                    return NotFound("khong tim thay");
                _mapper.Map(tinvVM, tinFind);
                _mapper.Map(tinvVM, phongFind);
                await _context.UpdateTinDang(tinFind,phongFind);
                return Ok(" cap nhat thanh cong");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
        }

        // DELETE api/<TinDangController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTin(string id)
        {
            try
            {
                bool tin = await _context.DeleteTinDang(id);
                if (tin == true)
                    return Ok("xoa thanh cong");
                return BadRequest("loi");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
