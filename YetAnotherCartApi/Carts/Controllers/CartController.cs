using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YetAnotherCartApi.Auth;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Carts.Context;
using YetAnotherCartApi.Carts.Dto;
using YetAnotherCartApi.Carts.Entity;
using YetAnotherCartApi.Orders.Context;
using YetAnotherCartApi.Orders.Entity;
using YetAnotherCartApi.Widgets.Context;

namespace YetAnotherCartApi.Carts.Controllers
{
    public class CartController : Controller
    {
        private CartContext cartCtx;
        private WidgetContext widgetCtx;
        private UserContext userCtx;
        private OrderContext orderCtx;
        public CartController(CartContext cartCtx, UserContext userCtx, WidgetContext widgetCtx, OrderContext orderCtx)
        {
            this.cartCtx = cartCtx;
            this.widgetCtx = widgetCtx;
            this.userCtx = userCtx;
            this.orderCtx = orderCtx;
        }


        [Authorize]
        [HttpPost]
        [Route("/cart/[action]")]
        public async Task<IActionResult> Add(IEnumerable<Widget> widget)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);

            if(usr == null)
            {
                return BadRequest();
            }

            var widgetIds = widget.Select(w => w.Uid).ToList();
            var actualWidgets = widgetCtx.Widgets.Where(w => widgetIds.Contains(w.Uid)).ToList();

            var cartContent = actualWidgets.Select(w => new Cart
            {
                UserId = usr.Uid,
                WidgetId = w.Uid,
            }).ToList();

            await cartCtx.Carts.AddRangeAsync(cartContent);
            await cartCtx.SaveChangesAsync();
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("/cart/[action]")]
        public async Task<IActionResult> Remove(IEnumerable<Widget> widget)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);

            var widgetIds = widget.Select(w => w.Uid).ToList();
            var actualWidgets = widgetCtx.Widgets.Where(w => widgetIds.Contains(w.Uid)).ToList();

            var cartContent = cartCtx.Carts.Where(c => c.UserId == usr.Uid && widgetIds.Contains(c.WidgetId)).ToList();
            cartCtx.RemoveRange(cartContent);
            await cartCtx.SaveChangesAsync();
            return View();
        }

        [Authorize]
        [Route("/cart/[action]")]
        public async Task<IEnumerable<CartListing>> List()
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            var listing = cartCtx.CartListings.Where(c => c.UserId == usr.Uid).ToList();
            return listing;
        }


        [Authorize]
        [Route("/cart/[action]")]
        public async Task<IActionResult> Checkout(IEnumerable<Widget>? widgets)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            var listing = cartCtx.CartListings.Where(c => c.UserId == usr.Uid).ToList();
            if (widgets != null)
            {
                var widgetId = widgets.Select(w => w.Uid).ToList();
                listing = listing.Where(c => widgetId.Contains(c.widgetId)).ToList();
            }

            var widgetsInCart = listing.Select(w => w.widgetId).ToList();
            var actualWidgets = widgetCtx.Widgets.Where(w => widgetsInCart.Contains(w.Uid)).ToList();
            var orders = actualWidgets.Select(w => new Order
            {
                WidgetId = w.Uid,
                Buyer = usr.Uid
            }).ToList();

            await orderCtx.AddRangeAsync(orders);

            return Ok();
        }
    }
}
