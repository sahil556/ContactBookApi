using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using AutoMapper;

namespace ContactBookApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ContactBookContext _context;
        private readonly IMapper _mapper;

        public UsersController(ContactBookContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet, Route("onlyprofile")]
        public async Task<ActionResult<IEnumerable<ProfileDTO>>> GetOnlyUser()
        {
            int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(id);
            Console.WriteLine("User id is :", id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProfileDTO>((User)await _context.Users
                   .FindAsync(id)));
        }

        // GET:         
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        { 
            int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(id);
            Console.WriteLine("User id is :", id);
            if (user == null)
            {
                return NotFound();
            }
          

            return _context.Users
                     .Where(a => a.UserId == id)
                     .Include(user => user.contactItems)
                     .ThenInclude(address => address.Addresses)
                     .Include(user => user.contactItems)
                     .ThenInclude(mobileno => mobileno.mobileNumbers)
                     .ToList();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutUser(ProfileDTO profile)
        {
            int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            
            if (id != profile.UserId)
            {
                return BadRequest();
            }
            User user = await _context.Users.FindAsync(id);

            _mapper.Map<ProfileDTO, User>(profile, user);   

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete]
        public async Task<IActionResult> DeleteUser()
        {
            int id = Int32.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
