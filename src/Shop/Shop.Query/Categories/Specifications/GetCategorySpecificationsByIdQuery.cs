using Common.Query.BaseClasses;
using Dapper;
using Shop.Query.Categories._DTOs;

namespace Shop.Query.Categories.Specifications;

public record GetCategorySpecificationsByIdQuery(long CategoryId) : IBaseQuery<List<QueryCategorySpecificationDto>>;

public class GetCategorySpecificationsByIdQueryHandler
    : IBaseQueryHandler<GetCategorySpecificationsByIdQuery, List<QueryCategorySpecificationDto>>
{
    private readonly DapperContext _dapperContext;

    public GetCategorySpecificationsByIdQueryHandler(DapperContext dapperContext)
    {
        _dapperContext = dapperContext;
    }

    public async Task<List<QueryCategorySpecificationDto>> Handle(GetCategorySpecificationsByIdQuery request,
        CancellationToken cancellationToken)
    {
        using var connection = _dapperContext.CreateConnection();
        var sql = $@"WITH parent_ids AS
                    (
                       SELECT Id, ParentId, Title
                       FROM {_dapperContext.Categories}
                       WHERE Id = @Id
                       UNION ALL
                       SELECT c.Id, c.ParentId, c.Title
                       FROM {_dapperContext.Categories} c
                       JOIN parent_ids p
                    		ON p.ParentId = c.Id
                    ) 
                    SELECT
                    	cs.Id, cs.Title, cs.CategoryId, cs.IsImportant, cs.IsOptional, cs.CreationDate
                    FROM parent_ids [pi]
                    JOIN {_dapperContext.CategorySpecifications} cs
                    	ON cs.CategoryId = [pi].Id
                    ORDER BY [pi].Id ASC";
        var result = await connection.QueryAsync<QueryCategorySpecificationDto>(sql, new { Id = request.CategoryId });
        return result.ToList();
    }
}