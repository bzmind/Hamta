using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Query;

public class DapperContext
{
    public string Categories => "[category].Categories";
    public string CategorySpecifications => "[category].Specifications";
    public string Sellers => "[seller].Sellers";
    public string SellerInventories => "[seller].Inventories";
    public string Users => "[user].Users";
    public string UserAddresses => "[user].Addresses";
    public string UserFavoriteItems => "[user].FavoriteItems";
    public string UserTokens => "[user].Tokens";
    public string Orders => "[order].Orders";
    public string OrderAddresses => "[order].Addresses";
    public string OrderItems => "[order].Items";
    public string Products => "[product].Products";
    public string ProductImages => "[product].Images";
    public string ProductScores => "[product].Scores";
    public string Colors => "[color].Colors";
    public string Comments => "[comment].Comments";
    public string CommentHints => "[comment].Hints";
    public string Roles => "[role].Roles";
    public string Shippings => "[shipping].Shippings";

    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}