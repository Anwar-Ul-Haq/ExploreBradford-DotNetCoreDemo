﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExploreBradford;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExploreBradford
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<FeatureToggles>(x => new FeatureToggles
            {
                DeveloperExceptions = _configuration.GetValue<bool>("FeatureToggles:DeveloperExceptions")
            });
        }

       
        public void Configure(IApplicationBuilder app,IHostingEnvironment env,FeatureToggles features)
        {
            app.UseExceptionHandler("/error.html");

            if (features.DeveloperExceptions)
            {
                app.UseDeveloperExceptionPage();
            }

            app.Use(async (context, next) =>
            {
                if (context.Request.Path.Value.Contains("invalid"))
                    throw new Exception("ERROR!");

                await next();
            });


            app.UseFileServer();
        }
    }
}