using AutoMapper;
using Core.Ioc;
using DynamicForm.Helpers;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace DynamicForm
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            // Add services to the container.
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutomapperWebProfile());
            });
            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddControllersWithViews();
            services.RegisterInfrastructureServices();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(1200);
                options.Cookie.HttpOnly = true;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                //options.LoginPath = "/Home/Login";
                //options.LogoutPath = "/Home/Logout";
            });

        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Login}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "IRT Flexicare API V2");
            });
        }
    }
}

