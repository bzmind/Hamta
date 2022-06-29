namespace Shop.API.CommandViewModels.Questions;

public class AddReplyCommandViewModel
{
    public long QuestionId { get; init; }
    public string Description { get; init; }
}