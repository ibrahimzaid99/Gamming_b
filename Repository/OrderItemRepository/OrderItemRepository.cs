using Graduation_Project.Models;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.OrderItemRepository
{
    public class OrderItemRepository : IOrderItemRepository
    {

        private readonly GraduationProject_Context context;

        public OrderItemRepository(GraduationProject_Context context)
        {
            this.context = context;
        }

        public OrderItem CreateOrderItem(OrderItem orderItem)
        {
            context.OrderItems.Add(orderItem);
            context.SaveChanges();
            return orderItem;
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return context.OrderItems
                .Where(orderItem => orderItem.OrderId == orderId)
                .Include(orderItem => orderItem.Product)
                .ToList();
        }

        public void DeleteOrderItemsByOrderId(int orderId)
        {
            var orderItemsToDelete = context.OrderItems.Where(orderItem => orderItem.OrderId == orderId).ToList();

            if (orderItemsToDelete != null)
            {
                context.OrderItems.RemoveRange(orderItemsToDelete);
                context.SaveChanges();
            }
        }
    }
}

