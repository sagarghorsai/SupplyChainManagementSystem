using CapstoneProject.Components;
using DataAccessLibrary;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http.Connections;
using MudBlazor.Services;


namespace CapstoneProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddRazorComponents()
                .AddInteractiveServerComponents();

            builder.Services.AddMudServices();

            //User Authentication
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.Cookie.Name = "auth_token";
                    options.LoginPath = "/login";
                    options.Cookie.MaxAge = TimeSpan.FromMinutes(30);
                    options.AccessDeniedPath = "/access-denied";

                });
            builder.Services.AddAuthorization();
            builder.Services.AddCascadingAuthenticationState();


            builder.Services.AddTransient<ISqlDataAccess, SqlDataAccess>();
            builder.Services.AddTransient<IProductData, ProductData>();
            builder.Services.AddTransient<IUserData, UserData>();
            builder.Services.AddTransient<IStoreData, StoreData>();
            builder.Services.AddTransient<ISupplierData, SupplierData>();
            builder.Services.AddTransient<IShipmentData,   ShipmentData>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseAntiforgery();

            app.MapRazorComponents<App>()
                .AddInteractiveServerRenderMode();

            app.Run();
        }
    }
}
