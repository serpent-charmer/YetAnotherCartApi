using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Auth;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Orders.Context;
using YetAnotherCartApi.Orders.Dto;

namespace YetAnotherCartApi.Orders.Controllers
{
    public class OrderController : Controller
    {

        private UserContext userCtx;
        private OrderContext orderCtx;
        public OrderController(UserContext userCtx, OrderContext orderCtx) 
        { 
            this.userCtx = userCtx;
            this.orderCtx = orderCtx;
        }

        [HttpPost]
        [Route("/order/[action]")]
        public async Task<IActionResult> Cancel([FromBody] OrderId order)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return BadRequest();
            }

            var orderToChange = await orderCtx.Orders.FirstOrDefaultAsync(o => o.Uid == order.Uid && o.Buyer == usr.Uid);

            if (orderToChange == null)
            {
                return BadRequest();
            }

            orderToChange.Status = "canceled";
            orderCtx.Entry(orderToChange).State = EntityState.Modified;
            await orderCtx.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Route("/order/[action]")]
        public async Task<IActionResult> Transit([FromBody] OrderTransit order)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return BadRequest();
            }
            var cap = await userCtx.Capabilities.FirstOrDefaultAsync(c => c.Uid == usr.Uid);
            if (cap == null || cap.Capability != "transitService")
            {
                return BadRequest();
            }

            var orderToChange = await orderCtx.Orders.FirstOrDefaultAsync(o => o.Uid == order.Uid);
            if (orderToChange == null)
            {
                return BadRequest();
            }
            
            if(order.ArriveAt != null && orderToChange.Status != "arrived")
            {
                orderToChange.ArriveAt = order.ArriveAt.Value;
            }
           
            if(!string.IsNullOrEmpty(order.Status))
            {
                orderToChange.Status = order.Status;
            }

            orderCtx.Entry(orderToChange).State = EntityState.Modified;
            await orderCtx.SaveChangesAsync();

            return Ok();
        }



        [HttpPost]
        [Route("/order/[action]")]
        public async Task<IActionResult> Invoice([FromBody] OrderId order)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return BadRequest();
            }
            var cap = await userCtx.Capabilities.FirstOrDefaultAsync(c => c.Uid == usr.Uid);
            if (cap == null || cap.Capability != "paymentService")
            {
                return BadRequest();
            }

            var invoiceInfo = await orderCtx.InvoiceInfo.FirstOrDefaultAsync(i => i.OrderId == order.Uid);
            if(invoiceInfo == null)
            {
                return BadRequest();
            }

            await orderCtx.Invoices.AddAsync(new Entity.Invoice
            {
                OrderId = invoiceInfo.OrderId,
                Buyer = invoiceInfo.Buyer,
                Seller = invoiceInfo.Seller,
                Price = invoiceInfo.Price,
            });
            await orderCtx.SaveChangesAsync();

            return Ok();
        }


    }
}
