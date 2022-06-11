namespace Shop.UI.Models.Comments;

public class CreateCommentViewModel
{
    public int ProductId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public List<string> PositivePoints { get; set; }
    public List<string> NegativePoints { get; set; }
    public string Recommendation { get; set; }
}