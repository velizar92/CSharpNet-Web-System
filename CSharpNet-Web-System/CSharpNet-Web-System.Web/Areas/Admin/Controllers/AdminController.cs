namespace CSharpNet_Web_System.Web.Areas.Admin.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    using static CSharpNet_Web_System.Infrastructure.Constants.IdentityConstants;

    [Authorize(Roles = AdminRole)]
    [Area(AdminRole)]
    public class AdminController : Controller
    {
    }
}
