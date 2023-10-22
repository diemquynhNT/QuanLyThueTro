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
        [HttpGet("{id}")]
        public Task<TinDang> GetById(string id)
        {
            return _context.GetTinDangById(id);
        }

        [HttpPost]
        public async Task<ActionResult> AddTin([FromBody] TinDangVM tin)
        {
            try
            {
                var tinDang = _mapper.Map<TinDang>(tin);
                await _context.AddTinDang(tinDang);
                return Ok("tao thanh cong");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] TinDangVM tinvVM)
        {
            try
            {
                var tinFind = await _context.GetTinDangById(id);
                if (tinFind == null)
                    return BadRequest("khong tim thay");
                _mapper.Map(tinvVM, tinFind);
                await _context.UpdateTinDang(tinFind);
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
