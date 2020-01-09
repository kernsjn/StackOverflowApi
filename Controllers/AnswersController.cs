using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackOverFlow.Models;
using StackOverFlow.ViewModels;
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

    [HttpPost("CreateAnswer/other")]
    public ActionResult<AnswersPost> nCreateAnswer([Bind("answersContent,questionPostId")] AnswersPost entry)
    {
      _context.AnswersPosts.Add(entry);
      _context.SaveChanges();
      return entry;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AnswersPost>>> GetAnswersPost()
    {
      return await _context.AnswersPost.ToListAsync();
    }
    [HttpPost("CreateAnswer")]
    public ActionResult<AnswersPost> CreateAnswer([FromBody]AnswersVM entry)
    {
      if (_context.QuestionPosts.Any(a => a.Id == entry.QuestionPostId))
      {
        var nAnswer = new AnswersPost
        {
          AnswerContent = entry.AnswerContent,
          QuestionPostId = entry.QuestionPostId
        };
        _context.AnswersPosts.Add(nAnswer);
        _context.SaveChanges();
        return nAnswer;
      }
      else
      {
        return BadRequest();
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<AnswersPost>> DeleteAnswer(int id)
    {
      var answer = await _context.AnswersPost.FindAsync(id);
      if (answer == null)
      {
        return NotFound();
      }

      _context.AnswersPost.Remove(answer);
      await _context.SaveChangesAsync();

      return answer;
    }

    private bool AnswersPostExists(int id)
    {
      return _context.AnswersPost.Any(e => e.Id == id);
    }
  }
}
