namespace Shop.API.ViewModels.Comments;

public class CreateCommentCommandViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> PositivePoints { get; set; }
    public List<string> NegativePoints { get; set; }
    public string Recommendation { get; set; }
}