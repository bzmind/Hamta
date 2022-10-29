using System.Text.Json;
using Common.Api;
using Shop.API.ViewModels.Transactions;

namespace Shop.UI.Services.Transactions;

public class TransactionService : BaseService, ITransactionService
{
    protected override string ApiEndpointName { get; set; } = "Transaction";

    public TransactionService(HttpClient client, JsonSerializerOptions jsonOptions) : base(client, jsonOptions) { }

    public async Task<ApiResult<string>> CreateTransaction(CreateTransactionViewModel model)
    {
        return await PostAsJsonAsync<string>("", model);
    }
}