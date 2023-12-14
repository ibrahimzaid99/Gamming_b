using Graduation_Project.Models;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;

namespace Graduation_Project.Repository.OrderRepository
{
    public class OrderRepository :IOrderRepository
    {
        private readonly GraduationProject_Context context;

        public OrderRepository(GraduationProject_Context context)
        {
            this.context = context;
        }


        public List<Order> GetAllOrders()
        {
            List<Order> AllOrders = context.Orders.ToList();
            return AllOrders;
        }

        public Order CreateOrder(Order order)
        {
            context.Orders.Add(order);
            context.SaveChanges();
            return order;
        }

        public List<Order> GetOrdersByUserId(string userId)
        {
            return context.Orders
                .Where(order => order.UserId == userId)
                .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                .ToList();
        }

        public Order GetOrderById(int orderId)
        {
            return context.Orders
                .Where(order => order.Id == orderId)
                .Include(order => order.OrderItems)
                    .ThenInclude(orderItem => orderItem.Product)
                .FirstOrDefault();
        }

        public void UpdateOrderStatus(int orderId, bool isCancelled, bool isCompleted)
        {
            var order = context.Orders.Find(orderId);
            if (order != null)
            {
                order.IsCancelled = isCancelled;
                order.IsCompleted = isCompleted;
                context.SaveChanges();
            }
        }

        public List<OrderItem> GetOrderItemsByOrderId(int orderId)
        {
            return context.OrderItems
                .Where(orderItem => orderItem.OrderId == orderId)
                .Include(orderItem => orderItem.Product)
                .ToList();
        }
        public void DeleteOrder(int orderId)
        {
            var orderToDelete = context.Orders.FirstOrDefault(o => o.Id == orderId);

            if (orderToDelete != null)
            {
                context.Orders.Remove(orderToDelete);
                context.SaveChanges();
            }
        }
    }

}
