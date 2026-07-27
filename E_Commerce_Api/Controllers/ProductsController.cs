using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Products;
using E_Commerce_Application.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce_Api.Controllers
{

    public class ProductsController : ApiBaseController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        //get all product
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDto>>> GetAllProducts(int?brandId,int?typeId,CancellationToken ct = default)
        {
            var result = await _productService.GetAllProductsAsync(brandId,typeId,ct);
            return ToActionResult(result);
        }
        //get product by id
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id, CancellationToken ct)
        {

            var result = await _productService.GetProductsByIdAsync(id, ct);
            return ToActionResult(result);
        }

        //get all types
        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<TypeDto>>> GetALLTypes(CancellationToken ct)
        {

            var result = await _productService.GetAllTypesAsync();
            return ToActionResult(result);

        }
        //get all brands
        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<BrandDto>>> GetALLBrands(CancellationToken ct)
        {

            var result = await _productService.GetAllBrandsAsync();
            return ToActionResult(result);
        }
    }
}
