﻿using System.Data;
using Microsoft.Data.SqlClient;

namespace Shop.Infrastructure;

public class DapperContext
{
    public string Avatars => "[avatar].Avatars";
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
    public string ProductCategorySpecifications => "[product].CategorySpecifications";
    public string ProductSpecifications => "[product].Specifications";
    public string ProductGalleryImages => "[product].GalleryImages";
    public string Colors => "[color].Colors";
    public string Comments => "[comment].Comments";
    public string CommentPoints => "[comment].Points";
    public string Roles => "[role].Roles";
    public string Shippings => "[shipping].Shippings";

    private readonly string _connectionString;

    public DapperContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}