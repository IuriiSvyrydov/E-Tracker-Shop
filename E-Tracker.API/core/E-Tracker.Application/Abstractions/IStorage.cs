using Microsoft.AspNetCore.Http;

namespace E_Tracker.Application.Abstractions
{
    public interface IStorage
    {
        Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string pathOrContainerName,
            IFormFileCollection files);

        Task DeleteAsync(string path, string fileName);
        List<string> GetFiles(string path);
        bool HasFiles(string path,string fileName);

    }
}
