using System;
using System.IO;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using ProjectWombat.Contracts;
using ProjectWombat.Hubs;
using ProjectWombat.Models;
using ProjectWombat.Services;

namespace ProjectWombat {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services) {
            services
                .AddMvc()
                    .AddJsonOptions(o => {
                        o.SerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                    }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSignalR()
                .AddJsonProtocol(options => {
                    options.PayloadSerializerSettings.Converters.Add(new StringEnumConverter { CamelCaseText = true });
                });

            services
                .AddSingleton<IProductService, ProductService>()
                .AddSingleton<IOrderRepository, InMemoryOrderRepository>()
                .AddSingleton<IOrderService, OrderService>()
                .AddSingleton<ICashierRepository, InMemoryCashierRepository>()
                .AddSingleton<ICashierService, CashierService>()
                .AddSingleton<IQrCodeGenerator, QrCodeGenerator>();

            services
                .AddHttpClient("swish-api", client => {
                    client.BaseAddress = new Uri("https://mpc.getswish.net/qrg-swish/api/v1/");
                    client.DefaultRequestHeaders.UserAgent.ParseAdd("ProjectWombat/1.0 (https://github.com/karl-sjogren/project-wombat)");
                });

            services.Configure<ApplicationConfiguration>(options => {
                var json = File.ReadAllText("configuration.json");
                JsonConvert.PopulateObject(json, options);
            });

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "ClientApp/build";
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env) {
            if(env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            app.UseSignalR(routes => {
                routes.MapHub<CashierHub>("/hubs/cashier");
            });

            app.UseMvc(routes => {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa => {
                spa.Options.SourcePath = "ClientApp/dist";

                if(env.IsDevelopment()) {
                    spa.UseProxyToSpaDevelopmentServer("http://localhost:4200/");
                }
            });
        }
    }
}
