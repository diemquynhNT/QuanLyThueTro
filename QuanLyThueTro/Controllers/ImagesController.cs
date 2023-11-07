using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLyThueTro.Data;
using QuanLyThueTro.Model;

namespace QuanLyThueTro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly MyDBContext _context;

        public ImagesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: api/Images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Images>>> GetImagesPhongTro()
        {
          if (_context.ImagesPhongTro == null)
          {
              return NotFound();
          }
            return await _context.ImagesPhongTro.ToListAsync();
        }

        // GET: api/Images/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Images>> GetImages(int id)
        {
          if (_context.ImagesPhongTro == null)
          {
              return NotFound();
          }
            var images = await _context.ImagesPhongTro.FindAsync(id);

            if (images == null)
            {
                return NotFound();
            }

            return images;
        }
        [HttpGet("GetListByIdTin")]
        public async Task<ActionResult<IEnumerable<Images>>> GetListByIdTin(string id)
        {
            if (_context.ImagesPhongTro == null)
            {
                return NotFound();
            }
            return await _context.ImagesPhongTro.Where(t=>t.idTinDang==id).ToListAsync();
        }

        [HttpGet("GetImageDisplay")]
        public async Task<ActionResult> GetImageDisplay(int id)
        {
            if (_context.ImagesPhongTro == null)
            {
                return NotFound();
            }
            Images hinh = _context.ImagesPhongTro.SingleOrDefault(t => t.idImage == id);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            var filePath = Path.Combine(path, hinh.nameImage);
            if (System.IO.File.Exists(filePath))
            {
                var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
                return File(fileStream, "image/png");
            }
            return null;
        }

        // PUT: api/Images/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImages(int id, Images images)
        {
            if (id != images.idImage)
            {
                return BadRequest();
            }

            _context.Entry(images).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagesExists(id))
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

        // POST: api/Images
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Images>> PostImages(Images images)
        {
          if (_context.ImagesPhongTro == null)
          {
              return Problem("Entity set 'MyDBContext.ImagesPhongTro'  is null.");
          }
            _context.ImagesPhongTro.Add(images);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImages", new { id = images.idImage }, images);
        }

        // DELETE: api/Images/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImages(int id)
        {
            if (_context.ImagesPhongTro == null)
            {
                return NotFound();
            }
            var images = await _context.ImagesPhongTro.FindAsync(id);
            if (images == null)
            {
                return NotFound();
            }
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", images.nameImage);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            _context.ImagesPhongTro.Remove(images);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagesExists(int id)
        {
            return (_context.ImagesPhongTro?.Any(e => e.idImage == id)).GetValueOrDefault();
        }
    }
}
