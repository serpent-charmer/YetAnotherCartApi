using Microsoft.EntityFrameworkCore;
using YetAnotherCartApi.Auth;
using YetAnotherCartApi.Auth.Context;
using YetAnotherCartApi.Cart.Context;
using YetAnotherCartApi.Widgets.Context;

namespace YetAnotherCartApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();


            builder.Services.AddDbContext<UserContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Debug")));
            builder.Services.AddDbContext<CartContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Debug")));
            builder.Services.AddDbContext<WidgetContext>(options =>
                options.UseNpgsql(builder.Configuration.GetConnectionString("Debug")));


            new ShopAuthorization().Init(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
