namespace Shop.API.ViewModels;

public record CreateCommentCommandViewModel(long ProductId, string Title, string Description,
    List<string> PositivePoints, List<string> NegativePoints, string Recommendation);