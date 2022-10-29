using Common.Api;
using Shop.API.ViewModels.Transactions;

namespace Shop.UI.Services.Transactions;

public interface ITransactionService
{
    Task<ApiResult<string>> CreateTransaction(CreateTransactionViewModel model);
}