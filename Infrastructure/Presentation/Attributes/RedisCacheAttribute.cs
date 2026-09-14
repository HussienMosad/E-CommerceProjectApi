using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Services.Abstraction.Contracts;
using System.Text;

namespace Presentation.Attributes
{
    internal class RedisCacheAttribute(int durationInSeconds = 120) : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var CacheService = context.HttpContext.RequestServices.GetRequiredService<IServiceManager>().CacheService;

            string Key = GetKey(context.HttpContext.Request);

            var result = await CacheService.GetCachedDataAsync(Key);
            if (result != null)
            {
                context.Result = new ContentResult
                {
                    Content = result ,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK 

                };
                return;
            }
            var resultcontext = await next.Invoke();
            if(resultcontext.Result is OkObjectResult objectResult)
            {
                await CacheService.SetCacheDataAsync(Key, objectResult, TimeSpan.FromSeconds(durationInSeconds));
            }
        }

        private string GetKey(HttpRequest request)
        {
            var Key = new StringBuilder();
            Key.Append(request.Path);
            foreach (var item in request.Query.OrderBy(k => k.Key))
            {
                Key.Append($"{item.Key}-{item.Value}");
            }
            return Key.ToString();
        }
    }
}
