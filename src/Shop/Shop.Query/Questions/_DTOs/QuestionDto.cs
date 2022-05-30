using Common.Query.BaseClasses;

namespace Shop.Query.Questions._DTOs;

public class QuestionDto : BaseDto
{
    public long ProductId { get; set; }
    public long CustomerId { get; set; }
    public string CustomerFullName { get; set; }
    public string Description { get; set; }
    public string Status { get; set; }
    public List<ReplyDto> Replies { get; set; }
}