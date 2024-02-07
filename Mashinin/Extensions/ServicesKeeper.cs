using FluentValidation;
using Mashinin.DTOs.CityDTOs;
using Mashinin.DTOs.ColorDTOs;
using Mashinin.DTOs.MakeDTOs;
using Mashinin.DTOs.ModelDTOs;
using Mashinin.DTOs.NumberPlateDTOs;
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
            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IColorService, ColorService>();
            services.AddScoped<INumberPlateService, NumberPlateService>();


            //logger
            services.AddSingleton<ILoggerManager, LoggerManager>();

            //fluent validation
            services.AddScoped<IValidator<MakeCreateDTO>, MakeCreateDTOValidator>();
            services.AddScoped<IValidator<MakeUpdateDTO>, MakeUpdateDTOValidator>();

            services.AddScoped<IValidator<ModelCreateDTO>, ModelCreateDTOValidator>();
            services.AddScoped<IValidator<ModelUpdateDTO>, ModelUpdateDTOValidator>();

            services.AddScoped<IValidator<CityCreateDTO>, CityCreateDTOValidator>();
            services.AddScoped<IValidator<CityUpdateDTO>, CityUpdateDTOValidator>();

            services.AddScoped<IValidator<ColorCreateDTO>, ColorCreateDTOValidator>();
            services.AddScoped<IValidator<ColorUpdateDTO>, ColorUpdateDTOValidator>();

            services.AddScoped<IValidator<NumberPlateCreateDTO>, NumberPlateCreateDTOValidator>();
            services.AddScoped<IValidator<NumberPlateUpdateDTO>, NumberPlateUpdateDTOValidator>();
        }
    }
}
