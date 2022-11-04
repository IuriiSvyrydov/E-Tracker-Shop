using E_Tracker.Application.Abstractions;


namespace E_Tracker.Infrastructure.Services.Storage
{
    public class StorageService: IStorageService
    {
        private readonly IStorage _storage;

        public StorageService(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string path, IFormFileCollection files)
        {
           return await _storage.UploadAsync(path, files);
        }

        public async Task DeleteAsync(string path, string fileName)
        
           => await _storage.DeleteAsync(path, fileName);
        

        public List<string> GetFiles(string path)
        {
            return _storage.GetFiles(path);
        }

        public  bool HasFiles(string path, string fileName)
        {
            return _storage.HasFiles(path, fileName);
        }

        public string StorageName
        {
            get => _storage.GetType().Name;
        }
    }
}
