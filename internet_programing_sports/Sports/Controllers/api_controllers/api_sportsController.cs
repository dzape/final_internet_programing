﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sports.Data;
using Sports.Models;

namespace Sports.Controllers.api_controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class api_sportsController : ControllerBase
    {
        private readonly SportsContext _context;

        public api_sportsController(SportsContext context)
        {
            _context = context;
        }

        // GET: api/api_sports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sport>>> GetSport()
        {
            return await _context.Sport.ToListAsync();
        }

        // GET: api/api_sports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sport>> GetSport(int id)
        {
            var sport = await _context.Sport.FindAsync(id);

            if (sport == null)
            {
                return NotFound();
            }

            return sport;
        }

        // PUT: api/api_sports/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSport(int id, Sport sport)
        {
            if (id != sport.SportId)
            {
                return BadRequest();
            }

            _context.Entry(sport).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SportExists(id))
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

        // POST: api/api_sports
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Sport>> PostSport(Sport sport)
        {
            _context.Sport.Add(sport);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSport", new { id = sport.SportId }, sport);
        }

        // DELETE: api/api_sports/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Sport>> DeleteSport(int id)
        {
            var sport = await _context.Sport.FindAsync(id);
            if (sport == null)
            {
                return NotFound();
            }

            _context.Sport.Remove(sport);
            await _context.SaveChangesAsync();

            return sport;
        }

        private bool SportExists(int id)
        {
            return _context.Sport.Any(e => e.SportId == id);
        }
    }
}
