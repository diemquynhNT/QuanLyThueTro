using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
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
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUsers _iusers;
        private GenerateAlphanumericId random;
        public static IWebHostEnvironment _webHostEnvironment;

        public UsersController(MyDBContext context,IMapper mapper,IUsers iuser, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _mapper = mapper;
            _iusers = iuser;
            random=new GenerateAlphanumericId();
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Users>>> Getusers()
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            return await _context.users.ToListAsync();
        }
        // GET: api/Users
        [HttpGet("GetEmployee")]
        public async Task<ActionResult<IEnumerable<Users>>> GetEmployee()
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            return await _context.users.Where(t=>t.idLoaiTK=="NV").ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Users>> GetUsers(string id)
        {
          if (_context.users == null)
          {
              return NotFound();
          }
            var users = await _context.users.FindAsync(id);

            if (users == null)
            {
                return NotFound();
            }

            return users;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsers(string id,[FromBody] UserModel userModel)
        {
            Users u = _context.users.Where(t => t.idUser == id).FirstOrDefault();
            if (u==null)
            {
                return NotFound();
            }

            _mapper.Map(userModel, u);
            _context.users.Update(u);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UsersExists(id))
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

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Users>> PostUsers([FromForm]UserModel userModel)
        {
            
            var users = _mapper.Map<Users>(userModel);

            if (_context.users == null)
              {
                  return Problem("Entity set 'MyDBContext.users'  is null.");
              }
            users.trangThai = true;
            users.idUser = "NV" + random.GenerateId(5);
            if (userModel.imge.Length > 0)
            {
                string path = _webHostEnvironment.WebRootPath + "\\img\\";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);

                }
                using (FileStream fileStream = System.IO.File.Create(path + userModel.imge.FileName))
                {
                    userModel.imge.CopyTo(fileStream);
                    fileStream.Flush();
                    users.hinhAnh = userModel.imge.FileName;

                }
            }
            _context.users.Add(users);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UsersExists(users.idUser))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUsers", new { id = users.idUser }, users);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsers(string id)
        {
            if (_context.users == null)
            {
                return NotFound();
            }
            var users = await _context.users.FindAsync(id);
            if (users == null)
            {
                return NotFound();
            }

            _context.users.Remove(users);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsersExists(string id)
        {
            return (_context.users?.Any(e => e.idUser == id)).GetValueOrDefault();
        }

        [HttpGet("GetImage")]
        public IActionResult GetImageDemo([FromQuery] string id)
        {
            var fileStream = _iusers.GetImageById(id);
            if (fileStream != null)
                return File(fileStream, "image/png");
            return NotFound();
        }




    }
}
