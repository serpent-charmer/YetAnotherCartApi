using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YetAnotherCartApi.Auth;
using YetAnotherCartApi.Cart.Context;
using YetAnotherCartApi.Cart.Dto;

namespace YetAnotherCartApi.Carts.Controllers
{
    public class CartController : Controller
    {
        private CartContext ctx;
        public CartController(CartContext ctx)
        {
            this.ctx = ctx;
        }


        [Authorize]
        [HttpPost]
        [Route("/cart/[action]")]
        public async Task<IActionResult> Add(IEnumerable<Widget> widget)
        {
            //ctx.AddRangeAsync(widget);
            return Ok();
        }

        [Authorize]
        [HttpPost]
        [Route("/cart/[action]")]
        public IActionResult Remove(IEnumerable<Widget> widget)
        {
            return View();
        }

        [Authorize]
        [Route("/cart/[action]")]
        public IActionResult List()
        {
            var usr = new ShopAuthorization().GetIdFromClaims(User.Claims);
            
            return Ok();
        }
    }
}
