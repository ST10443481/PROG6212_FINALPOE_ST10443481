using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using CMCS.Web.Helpers;
using CMCS.Web.Models;

namespace CMCS.Web.Attributes
{
    public class RoleAuthorizeAttribute : ActionFilterAttribute
    {
        private readonly string[] _roles;

        public RoleAuthorizeAttribute(params string[] roles)
        {
            _roles = roles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var user = session.GetObject<User>("User");

            if (user == null)
            {
                context.Result = new RedirectToActionResult("Login", "Auth", null);
                return;
            }

            if (!_roles.Contains(user.Role))
            {
                context.Result = new ContentResult
                {
                    Content = "Unauthorized: You do not have permission to access this page."
                };
            }
        }
    }
}
