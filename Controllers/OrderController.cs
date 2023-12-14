using Graduation_Project.Models;
using Graduation_Project.Repository.CartRepository;
using Graduation_Project.Repository.OrderItemRepository;
using Graduation_Project.Repository.OrderRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Graduation_Project.DTO;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IOrderItemRepository orderItemRepository;

        public OrderController(IOrderRepository orderRepository, ICartItemRepository cartItemRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.cartItemRepository = cartItemRepository;
            this.orderItemRepository = orderItemRepository;
        }
        //==============================================


        [HttpGet ("getallorders")]
        public IActionResult GetAllOrders()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Order> orders = orderRepository.GetAllOrders();
            return Ok(orders);
        }
        //==============================================

        [HttpGet("getalluserorders")]
        public IActionResult GetAllOrdersByUserId()
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            List<Order> orders = orderRepository.GetOrdersByUserId(userId).ToList();
            return Ok(orders);
        }

        //==============================================

        [HttpPost]
        public IActionResult CreateOrder([FromBody] OrderDto orderDto)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (ModelState.IsValid)
            {
                List<CartItem> cartItems = cartItemRepository.GetUserCarts(userId);

                decimal totalPrice = 0;

                foreach (var cartItem in cartItems)
                {
                    totalPrice += cartItem.Amount * cartItem.Product.Price;
                }

                Order order = new Order
                {
                    Government = orderDto.Government,
                    IsCancelled = false,
                    IsCompleted = false,
                    OrderDate = DateTime.UtcNow,
                    TotalPrice = totalPrice,
                    BulidingNumber = orderDto.BulidingNumber,
                    City = orderDto.City,
                    PostalCode = orderDto.PostalCode,
                    PhoneNumber = orderDto.PhoneNumber,
                    Street = orderDto.Street,
                    UserId = userId 
                };

                Order createdOrder = orderRepository.CreateOrder(order);

                foreach (var cartItem in cartItems)
                {
                    OrderItem orderItem = new OrderItem
                    {
                        Amount = cartItem.Amount,
                        Price = cartItem.Product.Price,
                        ProductId = cartItem.ProductId,
                        OrderId = createdOrder.Id
                    };
                    orderItemRepository.CreateOrderItem(orderItem);
                }

                foreach (var cartItem in cartItems)
                {
                    cartItemRepository.RemoveFromCart(cartItem.Id);
                }

                return Ok(createdOrder);
            }
            else
            {
                return BadRequest("No Order Created");
            }
        }

        //==============================================

        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(int id)
        {
            string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Check if the order belongs to the current user
            Order orderToDelete = orderRepository.GetOrderById(id);
            if (orderToDelete == null || orderToDelete.UserId != userId)
            {
                return NotFound("Order not found or you do not have permission to delete this order.");
            }

            try
            {
                // Delete the order and its associated order items
                orderItemRepository.DeleteOrderItemsByOrderId(id);
                orderRepository.DeleteOrder(id);

                return Ok("Order deleted successfully");
            }
            catch (Exception ex)
            {
                // Handle exceptions (log or return an error response)
                return BadRequest("Failed to delete order: " + ex.Message);
            }
        }

    }

}

