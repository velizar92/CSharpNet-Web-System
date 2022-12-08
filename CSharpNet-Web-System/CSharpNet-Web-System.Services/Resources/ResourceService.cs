namespace CSharpNet_Web_System.Services.Resources
{  
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CSharpNet_Web_System.Data;
    using Microsoft.EntityFrameworkCore;

    // TODO: Research on where and how to store website resources. - CSWS-108
    public class ResourceService : IResourceService
    {
        private readonly CSharpNetWebDbContext _dbContext;

        public ResourceService(CSharpNetWebDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public bool CheckIfResourceTypeExists(int resourceTypeId)
        {
            if (_dbContext.ResourceTypes.Any(r => r.Id == resourceTypeId) == false)
            {
                return false;
            }

            return true;
        }

        public async Task<ResultServiceModel> DeleteResource(Guid resourceId)
        {
            var resource = await _dbContext.Resources.FirstOrDefaultAsync(r => r.Id == resourceId);

            if (resource == null)
            {
                return new ResultServiceModel(false, "Invalid resource id.");
            }

            _dbContext.Resources.Remove(resource);
            await _dbContext.SaveChangesAsync();

            return new ResultServiceModel(true, "OK");
        }


        public async Task<IEnumerable<string>> GetAllResourceTypes()
        {
           var allResourceTypes = await _dbContext.ResourceTypes
                                             .Select(c => c.Name)
                                             .Distinct()
                                             .ToListAsync();

            return allResourceTypes;
        }
    }
}
