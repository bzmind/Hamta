using Common.Query.BaseClasses;

namespace Shop.Query.Questions._DTOs;

public class ReplyDto : BaseDto
{
    public long QuestionId { get; set; }
    public long ProductId { get; set; }
    public long UserId { get; set; }
    public string? UserFullName { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}