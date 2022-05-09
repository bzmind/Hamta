﻿using Common.Api;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Create;
using Shop.Presentation.Facade.Products;

namespace Shop.API.Controllers
{
    public class ProductController : BaseApiController
    {
        private readonly IProductFacade _productFacade;

        public ProductController(IProductFacade productFacade)
        {
            _productFacade = productFacade;
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommand command)
        {
            var result = await _productFacade.Create(command);
            return Ok(result);
        }
    }
}
