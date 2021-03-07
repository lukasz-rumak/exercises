using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoardGame.Interfaces;
using BoardGame.Managers;
using BoardGame.Models;
using BoardGameApi.Interfaces;
using BoardGameApi.Managers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using EventHandler = BoardGame.Managers.EventHandler;

namespace BoardGameApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSingleton<IGameHolder, GameHolder>();
            services.AddSingleton<IBoardBuilderHolder, BoardBuilderHolder>();
            services.AddTransient<IPlayer, Player>();
            services.AddTransient<IPresentation, ConsoleOutput>();
            services.AddTransient<IValidator, Validator>();
            services.AddTransient<IValidatorWall, Validator>();
            services.AddTransient<IEvent, EventHandler>();
            services.AddTransient<IGameBoard, GameBoard>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}