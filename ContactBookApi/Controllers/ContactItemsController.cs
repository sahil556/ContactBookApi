using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContactBookApi.Models;

namespace ContactBookApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactItemsController : ControllerBase
    {
        private readonly ContactBookContext _context;

        public ContactItemsController(ContactBookContext context)
        {
            _context = context;
        }

        // GET: api/ContactItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactItem>>> GetContacts()
        {
            return await _context.Contacts.ToListAsync();
        }

        // GET: api/ContactItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactItem>> GetContactItem(long id)
        {
            var contactItem = await _context.Contacts.FindAsync(id);

            if (contactItem == null)
            {
                return NotFound();
            }

            return contactItem;
        }

        // PUT: api/ContactItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactItem(long id, ContactItem contactItem)
        {
            if (id != contactItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactItemExists(id))
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

        // POST: api/ContactItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactItem>> PostContactItem(ContactItem contactItem)
        {
            _context.Contacts.Add(contactItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetContactItem), new { id = contactItem.Id }, contactItem);
        }

        // DELETE: api/ContactItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactItem(long id)
        {
            var contactItem = await _context.Contacts.FindAsync(id);
            if (contactItem == null)
            {
                return NotFound();
            }

            _context.Contacts.Remove(contactItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactItemExists(long id)
        {
            return _context.Contacts.Any(e => e.Id == id);
        }
    }
}
