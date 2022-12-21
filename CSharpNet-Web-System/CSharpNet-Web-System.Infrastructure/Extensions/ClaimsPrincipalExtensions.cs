namespace CSharpNet_Web_System.Infrastructure.Extensions
{
    using System.Security.Claims;
    using CSharpNet_Web_System.Infrastructure;

    public static class ClaimsPrincipalExtensions
    {
        public static string? Id(this ClaimsPrincipal user)
        {    
            if (user == null)
            {
                throw new ArgumentNullException("The user information should not be null");
            }

            return user.FindFirst(ClaimTypes.NameIdentifier).Value;
        }

        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(InfrastructureConstants.AdminRole);
        }
    }
}
