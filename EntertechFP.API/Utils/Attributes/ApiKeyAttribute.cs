using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EntertechFP.API.Attributes
{
    [AttributeUsage(validOn: AttributeTargets.Class)]
    public class ApiKeyAttribute : Attribute, IAsyncActionFilter
    {
        private const string API_KEY_NAME = "ApiKey";
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if(!context.HttpContext.Request.Headers.TryGetValue(API_KEY_NAME,out var extractedApiKey))
            {
                context.Result = new ContentResult() { Content = "Api Key girilmedi.", StatusCode = 401 };
                return;
            }
            var appSettings = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var apiKey = appSettings.GetValue<string>(API_KEY_NAME);
            if (!apiKey.Equals(extractedApiKey))
            {
                context.Result = new ContentResult() { Content = "Api Key yetkisi geçerli değil.", StatusCode = 401 };
                return;
            }
            await next();
        }
    }
}
