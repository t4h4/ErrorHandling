using ErrorHandling.Filter;
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

            services.AddMvc(options =>
            {
                options.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage="hata2" }); // tum uygulamada bi' hata oldugunda hata1 sayfasi gozuksun.
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) // middleware
        {

            // Request <------[UseDeveloperExceptionPage()]------[ExceptionHandler("/Home/Error")]------[UseStatusCodePages()]------[UseDatabaseErrorPage()]------> Response

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage(); // development ortamindaysa bu middleware yap?s? ekleniyor. developer kisilerin gordugu sayfa.
                // default hata sayfalari bu middleware ile otomatik olarak geliyor. 
                
                // 1. yol
                //app.UseStatusCodePages("text/plain","Bir hata var. Durum kodu: {0}");
                // 2. yol
                app.UseStatusCodePages(async context => // StatusCodeContext'e girdik. 
                {
                    context.HttpContext.Response.ContentType = "text/plain";
                    await context.HttpContext.Response.WriteAsync($"Bir hata var xd.Durum kodu:{context.HttpContext.Response.StatusCode}");
                });

                app.UseDatabaseErrorPage();
            }
            else
            {
                
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // her durumda calissin diye else'in disina aldik. (normalde yukarida production ortaminda calisiyor.)
            // bu kod hangi sayfada hata varsa o sayfada home/error iceriklirini gosteriyor. home/error'e gitmiyor, sadece iceriklerini gosteriyor.
            //app.UseExceptionHandler(context =>
            //{
            //    context.Run(async page =>
            //    {
            //        page.Response.StatusCode = 500; //server tarafinda hata kodu. client hatalar? 400 ile baslar. basarili durumlar 200, yonlendirmeler 300
            //        page.Response.ContentType = "text/html";
            //        await page.Response.WriteAsync($"<html><head></head><h1>Hata var {page.Response.StatusCode}</h1></html>");
            //    });
            //});


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


// spesifik durumlarda filtre hata yakalay?c?lar kullan?lmal?. normal durumlada middleware yeterlidir. 