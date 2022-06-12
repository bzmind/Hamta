using Shop.UI.Models._Filters;

namespace Shop.UI.Models.Comments;

public class CommentFilterParamsViewModel : BaseFilterParamsViewModel
{
    public long? UserId { get; set; }
    public long? ProductId { get; set; }
    public string? Status { get; set; }
}