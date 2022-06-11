namespace Shop.API.ViewModels.Comments;

public class CreateCommentCommandViewModel
{
    public long ProductId { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public List<string> PositivePoints { get; init; }
    public List<string> NegativePoints { get; init; }
    public string Recommendation { get; init; }
}