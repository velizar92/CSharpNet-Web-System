namespace CSharpNet_Web_System.Infrastructure.Seed
{
    using Microsoft.AspNetCore.Builder;
    public interface IDbInitializer
    {
        Task InitializeDatabase(IApplicationBuilder applicationBuilder);
    }
}

