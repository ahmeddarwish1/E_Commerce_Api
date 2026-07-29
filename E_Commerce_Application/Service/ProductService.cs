using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Products;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Products;
using E_Commerce_Application.Common;
using E_Commerce_Application.Specifications;
using E_Commerce_Application.Params;

namespace E_Commerce_Application.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitWork, IMapper mapper)
        {
            _unitOfWork = unitWork;
            _mapper = mapper;
        }


        public async Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct = default)
        {
            var brands = await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync(ct);
            //mapping
            var data = _mapper.Map<IReadOnlyList<BrandDto>>(brands);
            return Result<IReadOnlyList<BrandDto>>.Ok(data); 
        }

        public async Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams, CancellationToken ct = default)
        {
            var spec = new ProductSpecifications(queryParams);
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllwithspecAsync(spec, ct);
            var mappeddata = _mapper.Map<IReadOnlyList<ProductDto>>(products);
            var countspec = new ProductCountSpecification(queryParams);
            var totalcount= await _unitOfWork.GetRepository<Product,int>().GetProductCountwithspecAsync(countspec, ct);


            return Result<PaginatedResult<ProductDto>>.Ok(new PaginatedResult<ProductDto>
            (mappeddata, queryParams.PageIndex, products.Count, totalcount));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var Types = _mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDto>>.Ok(Types);
        }

        public async Task<Result<ProductDto>> GetProductsByIdAsync(int id, CancellationToken ct = default)
        {
            var spec=new ProductSpecifications(id);
            var product = await _unitOfWork
            .GetRepository<Product, int>()
            .GetByIdwithspecAsync(spec, ct);

            if (product == null)
                return Result<ProductDto>.Fail(
                Error.NotFound("Product. NotFound", $"Product with {id} is not found"));

            var productDto = _mapper.Map<ProductDto>(product);

            return Result<ProductDto>.Ok(productDto);
        }
    }
}
