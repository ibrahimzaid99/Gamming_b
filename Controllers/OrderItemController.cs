using Graduation_Project.Models;
using Graduation_Project.Repository.OrderItemRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Graduation_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderItemController : ControllerBase
    {
        private readonly IOrderItemRepository orderItemRepository;

        public OrderItemController(IOrderItemRepository orderItemRepository)
        {
           this.orderItemRepository = orderItemRepository;
        }

        //[HttpPost("CreateOrderItem")]
        //public IActionResult CreateOrderItem([FromBody] OrderItem orderItem)
        //{
        //    // Validate orderItem data using Data Annotations
        //    var validationContext = new ValidationContext(orderItem, null, null);
        //    var validationResults = new List<ValidationResult>();
        //    if (!Validator.TryValidateObject(orderItem, validationContext, validationResults, true))
        //    {
        //        foreach (var validationResult in validationResults)
        //        {
        //            ModelState.AddModelError(validationResult.MemberNames.First(), validationResult.ErrorMessage);
        //        }
        //        return BadRequest(ModelState);
        //    }

        //    // Perform necessary operations if validation passes
        //    OrderItem createdOrderItem = _orderItemRepository.CreateOrderItem(orderItem);
        //    return Ok(createdOrderItem);
        //}

        [HttpPost]
        public IActionResult CreateOrderItem([FromBody] OrderItem orderItem)
        {
            // Validate orderItem data and perform necessary operations
            // ...

            OrderItem createdOrderItem = orderItemRepository.CreateOrderItem(orderItem);
            return Ok(createdOrderItem);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetOrderItems(int id)
        {
            List<OrderItem> orderItems = orderItemRepository.GetOrderItemsByOrderId(id);
            return Ok(orderItems);
        }
    }
}

