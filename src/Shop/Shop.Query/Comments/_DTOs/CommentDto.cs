using Common.Query.BaseClasses;

namespace Shop.Query.Comments._DTOs;

public class CommentDto : BaseDto
{
    public long CustomerId { get; set; }
    public long ProductId { get; set; }
    public string CustomerFullName { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<CommentHintDto> CommentHints { get; set; }
    public string Status { get; set; }
    public string Recommendation { get; set; }
    public int Likes { get; set; }
    public int Dislikes { get; set; }
}