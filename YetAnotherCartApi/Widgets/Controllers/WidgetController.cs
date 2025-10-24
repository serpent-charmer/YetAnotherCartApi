using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Auth;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Widgets.Context;
using YetAnotherCartApi.Widgets.Dto;
using YetAnotherCartApi.Widgets.Entity;

namespace YetAnotherCartApi.Widgets.Controllers
{
    public class WidgetController : Controller
    {
        private WidgetContext widgetCtx;
        private UserContext userCtx;

        public WidgetController(WidgetContext ctx, UserContext userContext) { 
            this.widgetCtx = ctx;
            this.userCtx = userContext;
        }

        [HttpPost]
        [Authorize]
        [Route("/widget/[action]")]
        public async Task<IActionResult> Add([FromBody] IEnumerable<WidgetCreate> widget)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return BadRequest();
            }
            
            var widgetConverted = widget.Select(w => new ShopWidget
            {
                Uid = Guid.NewGuid(),
                Name = w.Name,
                Description = w.Description,
                Price = w.Price,
                UserId = usr.Uid
            }).ToList();
            await widgetCtx.Widgets.AddRangeAsync(widgetConverted);
            await widgetCtx.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("/widget/[action]")]
        public async Task<IEnumerable<Dto.Widget>> List()
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return null;
            }
            var widgetList = await widgetCtx.Widgets.Where(w => w.UserId == usr.Uid).ToListAsync();
            return widgetList.Select(w => new Dto.Widget
            {
                Uid = w.Uid.ToString()
            });
        }
    }
}
