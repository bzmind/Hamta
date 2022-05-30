using Common.Query.BaseClasses;

namespace Shop.Query.Questions._DTOs;

public class ReplyDto : BaseDto
{
    public long QuestionId { get; set; }
    public long ProductId { get; set; }
    public long CustomerId { get; set; }
    public string? CustomerFullName { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
}