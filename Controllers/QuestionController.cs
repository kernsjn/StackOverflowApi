using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverflowApi.Models;

namespace StackOverflowApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public QuestionController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Question
        [HttpGet]
        public async Task<ActionResult<IEnumerable<QuestionPost>>> GetQuestionPosts()
        {
            return await _context.QuestionPosts.ToListAsync();
        }

        // GET: api/Question/5
        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionPost>> GetQuestionPost(int id)
        {
            var questionPost = await _context.QuestionPosts.FindAsync(id);

            if (questionPost == null)
            {
                return NotFound();
            }

            return questionPost;
        }

        // PUT: api/Question/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuestionPost(int id, QuestionPost questionPost)
        {
            if (id != questionPost.Id)
            {
                return BadRequest();
            }

            _context.Entry(questionPost).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuestionPostExists(id))
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

        // POST: api/Question
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<QuestionPost>> PostQuestionPost(QuestionPost questionPost)
        {
            _context.QuestionPosts.Add(questionPost);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetQuestionPost", new { id = questionPost.Id }, questionPost);
        }

        // DELETE: api/Question/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<QuestionPost>> DeleteQuestionPost(int id)
        {
            var questionPost = await _context.QuestionPosts.FindAsync(id);
            if (questionPost == null)
            {
                return NotFound();
            }

            _context.QuestionPosts.Remove(questionPost);
            await _context.SaveChangesAsync();

            return questionPost;
        }

        private bool QuestionPostExists(int id)
        {
            return _context.QuestionPosts.Any(e => e.Id == id);
        }
    }
}
