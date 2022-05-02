using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Query;

public class DapperContext
{
    public string Categories => "[category].Categories";
    public string CategorySpecifications => "[category].Specifications";
    public string Inventories => "[inventory].Inventories";
    public string Customers => "[customer].Customers";
    public string CustomerAddresses => "[customer].Addresses";
    public string CustomerFavoriteItems => "[customer].FavoriteItems";
    public string Orders => "[order].Orders";
    public string OrderItems => "[order].Items";
    public string Products => "[product].Products";
    public string Colors => "[color].Colors";
    public string Comments => "[comment].Comments";
    public string CommentHints => "[comment].CommentHints";

    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}