using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverFlow.Models;
using StackOverflowApi.Models;

namespace StackOverflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public AnswersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnswersPost>>> GetAnswersPost()
        {
            return await _context.AnswersPost.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AnswersPost>> GetAnswersPost(int id)
        {
            var answersPost = await _context.AnswersPost.FindAsync(id);

            if (answersPost == null)
            {
                return NotFound();
            }

            return answersPost;
        }

        // PUT: api/Answers/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnswersPost(int id, AnswersPost answersPost)
        {
            if (id != answersPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(answersPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnswersPostExists(id))
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

        // POST: api/Answers
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<AnswersPost>> PostAnswersPost(AnswersPost answersPost)
        {
            _context.AnswersPost.Add(answersPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAnswersPost", new { id = answersPost.Id }, answersPost);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<AnswersPost>> DeleteAnswersPost(int id)
        {
            var answersPost = await _context.AnswersPost.FindAsync(id);
            if (answersPost == null)
            {
                return NotFound();
            }

            _context.AnswersPost.Remove(answersPost);
            await _context.SaveChangesAsync();

            return answersPost;
        }

        private bool AnswersPostExists(int id)
        {
            return _context.AnswersPost.Any(e => e.Id == id);
        }
    }
}
