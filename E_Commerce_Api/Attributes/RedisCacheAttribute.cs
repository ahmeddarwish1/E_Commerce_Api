using System.Text;
using E_Commerce_Application.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace E_Commerce_Api.Attributes
{
    public class RedisCacheAttribute : ActionFilterAttribute
    {
        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            //Get cache service from Di Contrainer
            var cashedservice = context.HttpContext.RequestServices.GetRequiredService<ICacheService>();
            var cashekey = CreateCasheKey(context.HttpContext.Request);
            var cashed = await cashedservice.GetAsync(cashekey);
            //check if cached data exist
            //if exist , return cashed data ,skip excecute end poin
            if (!string.IsNullOrEmpty(cashed))
            {

                context.Result = new ContentResult()
                {

                    Content = cashed,
                    ContentType = "application/json",
                    StatusCode = StatusCodes.Status200OK
                };
                return;
            }
            //if not exist , excute endpoint , and store result in cash if response 200 ok

            var Exceute = await next.Invoke();
            if (Exceute.Result is OkObjectResult { Value: not null } ok)
                await cashedservice.SetAsync(cashekey, ok.Value, TimeSpan.FromSeconds(1000));
            return;
        }



        private static string CreateCasheKey(HttpRequest request)
        {
            //path
            //api/ Product ?
            var key = new StringBuilder();
            key.Append(request.Path).Append("?");

            //brandid=10 & typeid=20
            foreach (var (k, value) in request.Query.OrderBy(x => x.Key))

                key.Append(key).Append("=").Append(value).Append("&");

            //api/product?brandid=10&typeid=20
            return key.ToString();
        }
    }
}
