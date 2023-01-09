using AngularEshop.DataLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AngularEshop.Core.Utilities.Extensions.Connection
{
    public static class ConnectionExtention
    {
        public static IServiceCollection AddApplicationDbContext(this IServiceCollection service, IConfiguration configuration)
        {
            service.AddDbContext<AngularEshopDbContext>(options =>
            {
                var connectionString = "ConnectionStrings:AngularEshopConnection:Development";
                options.UseSqlServer(configuration[connectionString]);
            });

            return service;
        }
    }
}
