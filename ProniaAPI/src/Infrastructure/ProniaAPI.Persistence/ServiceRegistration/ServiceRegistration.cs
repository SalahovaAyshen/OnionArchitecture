﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProniaAPI.Application.Abstractions.Repositories;
using ProniaAPI.Application.Abstractions.Services;
using ProniaAPI.Persistence.Contexts;
using ProniaAPI.Persistence.Implementations.Repositories;
using ProniaAPI.Persistence.Implementations.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProniaAPI.Persistence.ServiceRegistration
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(option => option.UseSqlServer(configuration.GetConnectionString("Default")));
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ICategoryService, CategoryService>();
            return services;
        }
    }
}