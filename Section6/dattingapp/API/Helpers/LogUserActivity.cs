using System.Threading.Tasks;
using API.interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Section6.dattingapp.API.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Section6.dattingapp.API.data;

namespace Section6.dattingapp.API.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public  async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            
            var resultContext = await next();
            if(!resultContext.HttpContext.User.Identity.IsAuthenticated) return;
            
            var userId = resultContext.HttpContext.User.GetUserId();
            var repo = resultContext.HttpContext.RequestServices.GetService<IUserRepository>();
            var user =  await  repo.GetUserByIdAsync(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAllAsync();
        }
    }
}