using Graduation_Project.Models;

namespace Graduation_Project.Repository.OrderRepository
{
    public interface IOrderRepository
    {
        Order CreateOrder(Order order);
        void DeleteOrder(int orderId);
        List<Order> GetAllOrders();
        Order GetOrderById(int orderId);
        List<OrderItem> GetOrderItemsByOrderId(int orderId);
        List<Order> GetOrdersByUserId(string userId);
        void UpdateOrderStatus(int orderId, bool isCancelled, bool isCompleted);
    }
}