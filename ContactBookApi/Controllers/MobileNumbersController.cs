using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace ContactBookApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MobileNumbersController : ControllerBase
    {
        private readonly ContactBookContext _context;

        public MobileNumbersController(ContactBookContext context)
        {
            _context = context;
        }

        // GET: api/MobileNumbers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MobileNumber>>> GetMobileNumber()
        {
            return await _context.MobileNumber.ToListAsync();
        }

        // GET: api/MobileNumbers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MobileNumber>> GetMobileNumber(int id)
        {
            var mobileNumber = await _context.MobileNumber.FindAsync(id);

            if (mobileNumber == null)
            {
                return NotFound();
            }

            return mobileNumber;
        }

        // PUT: api/MobileNumbers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMobileNumber(int id, MobileNumber mobileNumber)
        {
            if (id != mobileNumber.Id)
            {
                return BadRequest();
            }

            _context.Entry(mobileNumber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MobileNumberExists(id))
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

        // POST: api/MobileNumbers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MobileNumber>> PostMobileNumber(MobileNumber mobileNumber)
        {
            _context.MobileNumber.Add(mobileNumber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMobileNumber", new { id = mobileNumber.Id }, mobileNumber);
        }

        // DELETE: api/MobileNumbers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMobileNumber(int id)
        {
            var mobileNumber = await _context.MobileNumber.FindAsync(id);
            if (mobileNumber == null)
            {
                return NotFound();
            }

            _context.MobileNumber.Remove(mobileNumber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MobileNumberExists(int id)
        {
            return _context.MobileNumber.Any(e => e.Id == id);
        }
    }
}
