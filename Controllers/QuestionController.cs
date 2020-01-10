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

    [HttpGet("searchterm/{description}")]
    public async Task<ActionResult<QuestionPost>> SearchQuestionPost(string description)
    {
      var questionPost = await _context.QuestionPosts.FirstOrDefaultAsync(f=> f.Description == description);

      if (questionPost == null)
      {
        return NotFound();
      }

      return questionPost;
    }

    [HttpGet("AllAnswersJoin/{Id}")]
    public ActionResult<IEnumerable<Object>> GetQuestionAnswers(int Id)
    {
      var answers = _context.AnswersPosts.Where(w => w.QuestionPostId == Id);
      return answers.ToList();
    }
    [HttpPost("CreateQuestion")]
    public ActionResult<QuestionPost> CreatePost([FromBody]QuestionPost entry)
    {
      _context.QuestionPosts.Add(entry);
      _context.SaveChanges();
      return entry;
    }

    [HttpPut("upvote/{Id}")]
    public ActionResult<Int32> upVoteQuestion(int Id)
    {
      var question = _context.QuestionPosts.FirstOrDefault(f => f.Id == Id);
      question.NumberOfViews += 1;
      _context.SaveChanges();
      return question.NumberOfViews;
    }

    [HttpPut("downvote/{Id}")]
    public ActionResult<Int32> downVoteQuestion(int Id)
    {
      var question = _context.QuestionPosts.FirstOrDefault(f => f.Id == Id);
      question.NumberOfViews += 1;
      _context.SaveChanges();
      return question.NumberOfViews;
    }

    [HttpPost]
    public async Task<ActionResult<QuestionPost>> PostQuestionPost(QuestionPost questionPost)
    {
      _context.QuestionPosts.Add(questionPost);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetQuestionPost", new { id = questionPost.Id }, questionPost);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<QuestionPost>> DeleteQuestion(int id)
    {
      var question = await _context.QuestionPosts.FindAsync(id);
      if (question == null)
      {
        return NotFound();
      }

      _context.QuestionPosts.Remove(question);
      await _context.SaveChangesAsync();

      return question;
    }

    private bool QuestionPostExists(int id)
    {
      return _context.QuestionPosts.Any(e => e.Id == id);
    }
  }
}
