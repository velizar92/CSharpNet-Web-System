using CSharpNet_Web_System.Data;
using Microsoft.AspNetCore.Mvc;

namespace CSharpNet_Web_System.Web.ViewComponents
{
    [ViewComponent(Name = "HeaderMenu")]
    public class HeaderMenuViewComponent : ViewComponent
    {
        private readonly CSharpNetWebDbContext _context;

        public HeaderMenuViewComponent(CSharpNetWebDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("Index", _context.Courses.ToList());
        }
    }
}
