﻿using FluentValidation;
using Mashinin.Implementations;
using Mashinin.Interfaces;
using Mashinin.Logger;

namespace Mashinin.Extensions
{
    public static class ServicesKeeper
    {
        public static void ServicesBuilder(this IServiceCollection services)
        {
            //services
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IHomeService, HomeService>();


            //logger
            services.AddSingleton<ILoggerManager, LoggerManager>();

            //fluent validation
        }
    }
}