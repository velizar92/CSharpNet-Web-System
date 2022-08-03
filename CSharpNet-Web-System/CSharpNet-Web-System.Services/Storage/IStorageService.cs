namespace CSharpNet_Web_System.Services.Storage
{
    using Microsoft.AspNetCore.Http;

    public interface IStorageService
    {
        Task SaveFile(string destinationFolderPath, IFormFile file);
        Task SaveFiles(string destinationFolderPath, IEnumerable<IFormFile> files);
    }
}
