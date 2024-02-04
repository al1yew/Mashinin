using System.Globalization;

namespace Mashinin.Middlewares
{
    public class LanguageMiddleware
    {
        private readonly RequestDelegate _next;

        public LanguageMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var lang = context.Request.Headers["Accept-Language"].ToString().Split(",")[0];

            if (String.IsNullOrEmpty(lang))
            {
                lang = "az";
            }

            var culture = CultureInfo.GetCultureInfo(lang);

            CultureInfo.CurrentCulture = culture;
            CultureInfo.CurrentUICulture = culture;

            await _next(context);
        }
    }
}
