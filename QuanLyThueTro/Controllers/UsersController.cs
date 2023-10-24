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
    public class UsersController : ControllerBase
    {
        private readonly MyDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUsers _iusers;
        private GenerateAlphanumericId random;

        public UsersController(MyDBContext context,IMapper mapper,IUsers iuser)
        {
            _context = context;
            _mapper = mapper;
            _iusers = iuser;
            random=new GenerateAlphanumericId();
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
        public async Task<IActionResult> PutUsers(string id, Users users)
        {
            if (id != users.idUser)
            {
                return BadRequest();
            }
            _context.Entry(users).State = EntityState.Modified;

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
        public async Task<ActionResult<Users>> PostUsers(UserModel userModel)
        {
            bool checkPassword = _iusers.ValidatePassword(userModel.passwordUser);
            if (!checkPassword)
                return BadRequest("pass khong du yeu cau");
            bool checkMail = _iusers.ValidateEmail(userModel.emailUser);
            if (!checkMail)
                return BadRequest("email da co nguoi su dung");
            bool checkPhone = _iusers.ValidatePhone(userModel.sdtUsers);
            if (!checkPhone)
                return BadRequest("sdt da co nguoi su dung");
            var users = _mapper.Map<Users>(userModel);

            if (_context.users == null)
              {
                  return Problem("Entity set 'MyDBContext.users'  is null.");
              }
            users.trangThai = true;
            users.idUser = "NV" + random.GenerateId(5);
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
    }
}
