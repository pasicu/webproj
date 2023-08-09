using Common.Models.Database;
using Common.Models.Inbound;
using Common.Models.Outbound;

namespace BusinessLogicLayer.Contracts
{
    public interface IOrderService
    {
        Task<Order> CreateNewOrder(NewOrder order);
        Task<List<OrderView>> GetAllByBuyerId(string userId);
        Task<bool> CancelOrder(CancelOrder order);
        Task<OrderDetailsView> OrderDetails(OrderDetailsInbound orderDetailsInbound);
        Task<OrderDetailsView> SellerOrderDetails(OrderDetailsInbound orderDetailsInbound);
        Task<List<OrderView>> GetNewOrdersForSeller(string userId);
        Task<List<OrderView>> GetMyOrdersForSeller(string userId);
        Task<List<OrderView>> GetAllOrders();
    }
}
