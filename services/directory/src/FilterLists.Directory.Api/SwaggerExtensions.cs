using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace FilterLists.Directory.Api
{
    internal static class SwaggerExtensions
    {
        public static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "FilterLists Directory API",
                    Description =
                        "FilterLists is the independent, comprehensive directory of filter and host lists for advertisements, trackers, malware, and annoyances.",
                    Version = "v1",
                    //TermsOfService = "",
                    Contact = new OpenApiContact
                    {
                        Name = "Collin M. Barrett",
                        Url = new Uri("https://collinmbarrett.com/contact/")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT License",
                        Url = new Uri("https://github.com/collinbarrett/FilterLists/blob/master/LICENSE")
                    }
                }));
        }

        public static void UseSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger(o =>
            {
                o.RouteTemplate = "{documentName}/swagger.json";
                o.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
                {
                    new OpenApiServer {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/api"}
                });
            });
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "FilterLists Directory API V1");
                c.DocumentTitle = "FilterLists Directory API";
                c.RoutePrefix = string.Empty;
            });
        }
    }
}