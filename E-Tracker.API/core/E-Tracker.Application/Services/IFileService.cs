using Microsoft.AspNetCore.Http;

namespace E_Tracker.Application.Services;

public interface IFileService
    {
        Task<List<(string filename,string path) >> UploadAsync(string path, IFormFileCollection files);
        Task<bool> CopyFileAsync(string path, IFormFile file);
    }

