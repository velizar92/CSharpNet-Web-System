namespace CSharpNet_Web_System.Services.Resources
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    public interface IResourceService
    {
        Task<ResultServiceModel> DeleteResource(Guid resourceId);

        bool CheckIfResourceTypeExists(int resourceTypeId);

        Task<IEnumerable<string>> GetAllResourceTypes();
    }
}
