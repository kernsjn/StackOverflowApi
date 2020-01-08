using System;
using System.Collections.Generic;

namespace StackOverflowApi.Models
{
  public class QuestionPost
  {
    public int Id { get; set; }
    public string Description { get; set; }
    public int NumberOfViews { get; set; }
    public string Content { get; set; }
    public int UpDownVoteQuestion { get; set; }
    public DateTime DateOfPost { get; set; } = DateTime.UtcNow;
    public List<AnswersPost> AnswersPosts { get; set; } = new List<AnswersPost>();
  }
}