namespace Shop.API.ViewModels.Comments;

public record CreateCommentCommandViewModel(long ProductId, string Title, string Description,
    List<string> PositivePoints, List<string> NegativePoints, string Recommendation);