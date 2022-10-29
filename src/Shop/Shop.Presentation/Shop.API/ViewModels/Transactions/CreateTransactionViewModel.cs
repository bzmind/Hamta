namespace Shop.API.ViewModels.Transactions;

public class CreateTransactionViewModel
{
    public long OrderId { get; set; }
    public string SuccessCallbackUrl { get; set; }
    public string ErrorCallbackUrl { get; set; }
}