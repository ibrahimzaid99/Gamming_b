using Graduation_Project.Models;

namespace Graduation_Project.Repository.OrderItemRepository
{
    public interface IOrderItemRepository
    {
        OrderItem CreateOrderItem(OrderItem orderItem);
        void DeleteOrderItemsByOrderId(int orderId);
        List<OrderItem> GetOrderItemsByOrderId(int orderId);
    }
}