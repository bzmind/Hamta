﻿using Common.Domain.Repository;

namespace Shop.Domain.CategoryAggregate.Repository;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<bool> RemoveCategory(long categoryId);
}