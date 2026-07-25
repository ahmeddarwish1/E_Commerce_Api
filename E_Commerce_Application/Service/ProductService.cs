using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dros.Products;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Products;
using E_Commerce_Application.Common;

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

        public async Task<Result<IReadOnlyList<ProductDto>>> GetAllProductsAsync(CancellationToken ct = default)
        {
            var products = await _unitOfWork.GetRepository<Product, int>().GetAllAsync(ct);
            return Result<IReadOnlyList<ProductDto>>.Ok(_mapper.Map<IReadOnlyList<ProductDto>>(products));
        }

        public async Task<Result<IReadOnlyList<TypeDto>>> GetAllTypesAsync(CancellationToken ct = default)
        {
            var Types = _mapper.Map<IReadOnlyList<TypeDto>>(await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync(ct));
            return Result<IReadOnlyList<TypeDto>>.Ok(Types);
        }

        public async Task<Result<ProductDto>> GetProductsByIdAsync(int id, CancellationToken ct = default)
        {
            var product = await _unitOfWork
            .GetRepository<Product, int>()
            .GetByIdAsync(id, ct);

            if (product == null)
                return Result<ProductDto>.Fail(
                Error.NotFound("Product. NotFound", $"Product with {id} is not found"));

            var productDto = _mapper.Map<ProductDto>(product);

            return Result<ProductDto>.Ok(productDto);
        }
    }
}
