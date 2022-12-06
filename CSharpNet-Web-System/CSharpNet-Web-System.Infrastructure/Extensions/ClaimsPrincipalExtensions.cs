namespace CSharpNet_Web_System.Infrastructure.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        // TODO: Add is null check to resolve warning.
        // Action to be done in this issue - CSWS-100
        public static string Id(this ClaimsPrincipal user)
        {          
            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole("Admin");
        }
    }
}
