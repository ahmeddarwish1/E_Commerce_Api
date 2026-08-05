using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using E_Commerce_Application.Common;
using E_Commerce_Application.Contracts;
using E_Commerce_Application.Dtos.Orders;
using E_Commerce_Application.Specifications;
using E_Commerce_Domain.Contract;
using E_Commerce_Domain.Entities.Orders;
using E_Commerce_Domain.Entities.Products;

namespace E_Commerce_Application.Service
{
    internal class OrderServices : IOrderService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork unitOfWork;
        private readonly IBasketRepository basketRepository;

        public OrderServices(IMapper mapper, IUnitOfWork unitOfWork, IBasketRepository basketRepository)
        {

            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.basketRepository = basketRepository;
        }

        public async Task<Result<OrderToReturnDto>> CreatOrderAsync(OrderDto orderDto, string email, CancellationToken cancellationToken)
        {

            //1- Validate Basket Found & items
            var basket = await basketRepository.GetBasketAsync(orderDto.BasketId, cancellationToken);
            if (basket is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound("Basket not found"));
            if (basket.Items.Count == 0)
                return Result<OrderToReturnDto>.Fail(Error.Validation("Basket is empty"));

            //2-Get items from Basket Validate as product
            //then get data from product == > make it as order item
            var productrepo = unitOfWork.GetRepository<Product, int>();
            //basket item id
            var productids = basket.Items.Select(i => i.Id).ToHashSet();
            var products = await productrepo.GetAllwithspecAsync(new ProductWithIdsSpecifications(productids), cancellationToken);

            var orderitem = new List<OrderItem>(basket.Items.Count);
            foreach (var item in basket.Items)
            {

                var product = products.FirstOrDefault(P => P.Id == item.Id);
                if (product is null)
                    return Result<OrderToReturnDto>.Fail(Error.NotFound($"Product with id {item.Id} not found"));
                orderitem.Add(new OrderItem()
                {

                    Price = product.Price,
                    Quantity = item.Quantity,
                    Product = new ProductItemOrdered()
                    {
                        ProductId = product.Id,
                        ProductName = product.Name,
                        PictureUrl = product.PictureUrl
                    }
                });
            }

            //3- store order address
            var orderaddress = mapper.Map<OrderAddress>(orderDto.ShippingAddress);

            //4- store delivery method
            var deliverymethod = await unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DeliveryMethodId, cancellationToken);
            if (deliverymethod is null)
                return Result<OrderToReturnDto>.Fail(Error.NotFound($"Delivery method with id {orderDto.DeliveryMethodId} not found"));
            //5-
            var subtotal = orderitem.Sum(i => i.Price * i.Quantity);
            //6- Generate order
            var order = new Order()
            {

                BuyerEmail = email,
                Items = orderitem,
                ShippingAdress = orderaddress,
                DeliveryMethod = deliverymethod,
                Subtotal = subtotal
            };

            unitOfWork.GetRepository<Order, Guid>().Add(order);
            var result = await unitOfWork.SaveChangesAsync(cancellationToken);
            //1- Return order
            if (result <= 0)
                return Result<OrderToReturnDto>.Fail(Error.Failure("Order creation failed"));
            await basketRepository.DeleteBasketAsync(orderDto.BasketId, cancellationToken);
            return Result<OrderToReturnDto>.Ok(mapper.Map<OrderToReturnDto>(order));

           


        }
    }
}
