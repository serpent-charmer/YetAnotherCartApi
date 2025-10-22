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
            var userId = new ShopAuthorization().GetIdFromClaims(User.Claims);
            var usr = userCtx.Users.FirstOrDefault(u => u.Uid == Guid.Parse(userId));
            if(usr == null)
            {
                return BadRequest();
            }

            await using var transaction = await widgetCtx.Database.BeginTransactionAsync();
            try
            {
                var widgetConverted = widget.Select(w => new ShopWidget
                {
                    Uid = Guid.NewGuid(),
                    Name = w.Name,
                    Description = w.Description,
                    Price = w.Price
                }).ToList();
                await widgetCtx.Widgets.AddRangeAsync(widgetConverted);
                var widgetShop = widgetConverted.Select(w => new UserShop
                {
                    UserId = Guid.Parse(userId),
                    WidgetId = w.Uid,
                });
                await widgetCtx.UserShop.AddRangeAsync(widgetShop);
                await widgetCtx.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        [Route("/widget/[action]")]
        public async Task<IActionResult> Status([FromBody] WidgetStatus widgetStatus)
        {
            var usr = new ShopAuthorization().GetUser(User.Claims, userCtx.Users);
            if (usr == null)
            {
                return BadRequest();
            }
            var transaction = widgetCtx.Database.BeginTransaction();
            try
            {
                var widgetIds = widgetStatus.widget.Select(w => w.Uid).ToList();
                var widgets = widgetCtx.UserShop.Where(w => w.UserId == usr.Uid && widgetIds.Contains(w.WidgetId)).ToList();
                widgets.ForEach(w => 
                { 
                    w.IsPublic = widgetStatus.status;
                    widgetCtx.Entry(w).State = EntityState.Modified;
                });
                await widgetCtx.SaveChangesAsync();
                await transaction.CommitAsync();
            } catch(Exception e)
            {
                await transaction.RollbackAsync();
            }
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
            var widgets = widgetCtx.UserShop.Where(w => w.UserId == usr.Uid).ToList();
            var widgetIds = widgets.Select(w => w.WidgetId).ToList();
            var widgetList = widgetCtx.Widgets.Where(w => widgetIds.Contains(w.Uid)).ToList();
            return widgetList.Select(w => new Dto.Widget
            {
                Uid = w.Uid.ToString()
            });
        }
    }
}
