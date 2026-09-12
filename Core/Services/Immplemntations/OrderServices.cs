using AutoMapper;
using Domain.Contracts;
using Domain.Entities.BasketModule;
using Domain.Entities.OrderModule;
using Domain.Entities.ProductModule;
using Domain.Exceptions;
using Services.Abstraction.Contracts;
using Services.Specifications;
using Shared.Dtos.OrderDtos;
namespace Services.Immplemntations
{
    public class OrderServices : IOrderServices
    {
        #region DI
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IBasketRepository _basketRepository;

        public OrderServices(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            IBasketRepository basketRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _basketRepository = basketRepository;
        }
        #endregion

        #region Create Order Async
        public async Task<OrderResult> CreateOrderAsync(OrderRequest orderRequest, string UserEmail)
        {
            // [1] Map addressDto to address
            var Address = _mapper.Map<Address>(orderRequest.ShippingAddress);
            // [2] GetOrderItems ==> BasketId ==> Basket ==> BasketItems [Id]
            var Basket = await _basketRepository.GetBasketByIdAsync(orderRequest.BasketId)
                ?? throw new BasketNotFoundException(orderRequest.BasketId);

            var OrderItems = new List<OrderItem>();
            foreach (var item in Basket.BasketItems)
            {
                var Product =await _unitOfWork.GetRepository<Product, int>().GetByIdAsync(item.Id)
                    ?? throw new ProductNotFoundException(item.Id);

                OrderItems.Add(CreateOrderItem(Product , item));
                
            }
            var OrderRepo = _unitOfWork.GetRepository<Order, Guid>();
            // [3] GetDeliveryMethod ==> DeliveryMethodId ==> DB
            var deliveryMethod = await _unitOfWork
                .GetRepository<DeliveryMethod, int>()
                .GetByIdAsync(orderRequest.DeliveryMethodId)
                ?? throw new DeliveryMethodNotFoundException(
                    orderRequest.DeliveryMethodId);

            var OrderExcist = await OrderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecfications(Basket.PaymentIntentId));
            if (OrderExcist is not null) OrderRepo.Delete(OrderExcist);

            // [4] Calculate SubTotal ==> OrderItem.Q * OrderItem.Price
            var subTotal = OrderItems.Sum(
                item => item.Quantity * item.Price);

            // [5] Create obj from order ==> params, Add DB, Save Changes
            var order = new Order(
                UserEmail,
                Address,
                OrderItems,
                deliveryMethod,
                subTotal,Basket.PaymentIntentId);

            await OrderRepo.AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            // [6] Map <Order, OrderResult>
            return _mapper.Map<OrderResult>(order);
        }
        private OrderItem CreateOrderItem(Product product, BasketItem item)
        {
            var productInOrderItem = new ProductInOrderItem(
                product.Id,
                product.Name,
                product.PictureUrl);

            return new OrderItem(
                productInOrderItem,
                item.Quantity
                ,product.Price);
        }

        #endregion

        public async Task<IEnumerable<OrderResult>> GetAllOrdersByEmailAsync(string UserEmail)
        {
            var Orders = await _unitOfWork.GetRepository<Order, Guid>().GetAllAsync(new OrderWithIncludeSpecifications(UserEmail));
            return _mapper.Map<IEnumerable<OrderResult>>(Orders);
        }



        public async Task<IEnumerable<DeliveryMethodResult>> GetDeliveryMethodsAsync()
        {
            var DeliveryMethods = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                .GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodResult>>(DeliveryMethods);
        }




        public async Task<OrderResult> GetOrderByIdAsync(Guid OrderId)
        {
            var Order = await _unitOfWork .GetRepository<Order, Guid>().GetByIdAsync(new OrderWithIncludeSpecifications(OrderId))
                ?? throw new OrderNotFoundException(OrderId);

            return _mapper.Map<OrderResult>(Order);
        }
    }
}
