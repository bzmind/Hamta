using Common.Application;
using Common.Application.BaseClasses;
using Shop.Domain.ColorAggregate.Repository;

namespace Shop.Application.Colors.Remove;

public record RemoveColorCommand(long ColorId) : IBaseCommand;

public class RemoveColorCommandHandler : IBaseCommandHandler<RemoveColorCommand>
{
    private readonly IColorRepository _colorRepository;

    public RemoveColorCommandHandler(IColorRepository colorRepository)
    {
        _colorRepository = colorRepository;
    }

    public async Task<OperationResult> Handle(RemoveColorCommand request, CancellationToken cancellationToken)
    {
        var color = await _colorRepository.GetAsync(request.ColorId);

        if (color == null)
            return OperationResult.NotFound();

        _colorRepository.Delete(color);

        await _colorRepository.SaveAsync();
        return OperationResult.Success();
    }
}