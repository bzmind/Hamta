namespace Shop.API.CommandViewModels.Questions;

public class CreateQuestionCommandViewModel
{
    public long ProductId { get; init; }
    public string Description { get; init; }
}