using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Course
{
    public static class CorsExtenions
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
           //// services.AddCors(options =>
           // {
           //     options.AddDefaultPolicy(
           //          builder =>
           //          builder
           //         .AllowAnyOrigin()
           //         .AllowAnyMethod()
           //         .AllowAnyHeader()
           //         .AllowCredentials());
           // });
        }
    }
}
