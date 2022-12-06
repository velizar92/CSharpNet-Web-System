namespace CSharpNet_Web_System.Services.Storage
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    
    public class FileStorageService : IStorageService
    {
        private readonly IHostingEnvironment _webHostEnvironment;

        public FileStorageService(IHostingEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task SaveFile(string destinationFolderPath, IFormFile file)
        {
            // TODO Check documentation for FileMode --> Create and CreateNew .. maybe we should think about the behaviour we want upon saving files. - CSWS-108
            string detailPath = Path.Combine(destinationFolderPath, file.FileName);
            using (var stream = new FileStream(_webHostEnvironment.WebRootPath + detailPath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
        }

        public async Task SaveFiles(string destinationFolderPath, IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                string detailPath = Path.Combine(destinationFolderPath, file.FileName);
                using (var stream = new FileStream(_webHostEnvironment.WebRootPath + detailPath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
        }
    }
}
