using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleSearch.Data;
using PeopleSearch.Services;
using System.Text.RegularExpressions;

namespace PeopleSearch.Controllers
{
    public struct SearchResults
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public IEnumerable<object> Results { get; set; }
    }

    [Produces("application/json")]
    [Route("api/People")]
    public class PeopleController : Controller
    {
        private readonly PeopleSearchContext _context;
        private readonly IAvatarService _avatarService;
        private const int DefaultPageNumber = 0;
        private const int DefaultPageSize = 24;

        public PeopleController(PeopleSearchContext context, IAvatarService avatarService)
        {
            _context = context;
            _avatarService = avatarService;
        }

        private static readonly Regex NonAlpha = new Regex(@"[^a-z]", RegexOptions.IgnoreCase);
        [HttpGet]
        public SearchResults GetPeople(
            [FromQuery] string search = null, 
            [FromQuery] int page = DefaultPageNumber, 
            [FromQuery] int pageSize = DefaultPageSize)
        {
            //no search terms = list all
            if (string.IsNullOrWhiteSpace(search))
                return ToSearchResults(_context.People);

            //Normalize and sanitize input - only alpha characters allowed
            //we won't need any other characters to search first or last name
            var searchTerms = (search ?? string.Empty)
                .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                .Select(s => NonAlpha.Replace(s.Trim(), string.Empty).ToLower())
                .Where(s => !string.IsNullOrWhiteSpace(s))
                .ToArray();

            //search only contained invalid characters
            if (!searchTerms.Any())
                return ToSearchResults(Enumerable.Empty<Person>().AsQueryable());

            var matches = searchTerms.Aggregate(_context.People as IQueryable<Person>, 
                (query, term) => query.Where(p => p.GivenName.StartsWith(term) || p.Surname.StartsWith(term)));
            return ToSearchResults(matches);

            SearchResults ToSearchResults(IQueryable<Person> people)
            {
                var count = people.Count();
                return new SearchResults
                {
                    Page = page,
                    PageSize = pageSize,
                    TotalRecords = count,
                    Results = people
                        .Skip(page * pageSize)
                        .Take(pageSize)
                        .Select(p => new
                        {
                            p.Id,
                            p.GivenName,
                            p.Surname,
                            AvatarUrl = _avatarService.GetAvatarUrl(p)
                        })
                };
            }
        }

        // GET: api/People/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var person = await _context.People.SingleOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }
            person.AvatarUrl = _avatarService.GetAvatarUrl(person);

            return Ok(person);
        }

        // PUT: api/People/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerson([FromRoute] int id, [FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != person.Id)
            {
                return BadRequest();
            }

            _context.Entry(person).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PersonExists(id))
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

        // POST: api/People
        [HttpPost]
        public async Task<IActionResult> PostPerson([FromBody] Person person)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.People.Add(person);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPerson), new { id = person.Id }, person);
        }

        // DELETE: api/People/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePerson([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var people = await _context.People.SingleOrDefaultAsync(m => m.Id == id);
            if (people == null)
            {
                return NotFound();
            }

            _context.People.Remove(people);
            await _context.SaveChangesAsync();

            return Ok(people);
        }

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e.Id == id);
        }
    }
}