using System;
using System.Collections.Generic;

namespace StackOverFlow.Models
{
  public class AnswersPost
  {
    public int Id { get; set; }

    public string AnswerContent { get; set; }

    public int UpDownVoteAnswer { get; set; }

    public DateTime DateOfPost { get; set; } = DateTime.UtcNow;

    public int? QuestionPostId { get; set; }

    public QuestionPost QuestionPost { get; set; }
  }
}