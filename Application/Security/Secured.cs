using System;
using Application.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Application.Security
{
    [AttributeUsage(AttributeTargets.Method)]
    public class Secured : ActionFilterAttribute
    {
        public string Roles { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Secured()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var userService = (IUserService) context.HttpContext.RequestServices.GetService(typeof(IUserService));
            Console.WriteLine(Roles);
            Console.WriteLine(userService.ToString());
            Console.WriteLine("Secured:OnActionExecuting");

            string username = userService.FindUsernameByToken("561f172e-3a69-4dfc-bac4-77bdb454be9d");
            Console.WriteLine(username);

            context.Result = context.Result = new ContentResult()
            {
                StatusCode = 403,
                Content = "Forbiddeno"
            };
        }
    }
}
