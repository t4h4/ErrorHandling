using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandling
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) // services
        {
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // middleware
        {

            // Request <------[UseDeveloperExceptionPage()]------[ExceptionHandler("/Home/Error")]------[UseStatusCodePages()]------> Response

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // development ortamindaysa bu middleware yapýsý ekleniyor. developer kisilerin gordugu sayfa.
                // default hata sayfalari bu middleware ile otomatik olarak geliyor. 
                
                // 1. yol
                app.UseStatusCodePages("text/plain","Bir hata var. Durum kodu: {0}");
                // 2. yol
                app.UseStatusCodePages(async context => // StatusCodeContext'e girdik. 
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    await context.HttpContext.Response.WriteAsync($"Bir hata var xd.Durum kodu:{context.HttpContext.Response.StatusCode}");
                });
            }
            else
            {
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // her durumda calissin diye else'in disina aldik. (normalde yukarida production ortaminda calisiyor.)
            // bu kod hangi sayfada hata varsa o sayfada home/error iceriklirini gosteriyor. home/error'e gitmiyor, sadece iceriklerini gosteriyor.
            app.UseExceptionHandler("/Home/Error"); // hata sayfasina yonlendiren middleware. kullanici kisilerin gordugu sayfa.
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
