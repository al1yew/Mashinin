using FluentValidation;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.DTOs.ModelDTOs;
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
            services.AddScoped<IMakeService, MakeService>();
            services.AddScoped<IModelService, ModelService>();


            //logger
            services.AddSingleton<ILoggerManager, LoggerManager>();

            //fluent validation
            services.AddScoped<IValidator<MakeCreateDTO>, MakeCreateDTOValidator>();
            services.AddScoped<IValidator<MakeUpdateDTO>, MakeUpdateDTOValidator>();

            services.AddScoped<IValidator<ModelCreateDTO>, ModelCreateDTOValidator>();
            services.AddScoped<IValidator<ModelUpdateDTO>, ModelUpdateDTOValidator>();
        }
    }
}
