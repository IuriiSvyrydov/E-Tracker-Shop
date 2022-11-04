

namespace E_Tracker.Infrastructure.Services.Storage.Azure
{
    public class AzureStorage:Storage, IAzureStorage
    {
        private readonly BlobServiceClient _blobServiceClient;
        private  BlobContainerClient _blobContainerClient;
        public AzureStorage(IConfiguration configuration)
        {
            _blobServiceClient = new(configuration["Storage:Azure"]);
        }

        public async Task<List<(string fileName, string pathOrContainerName)>> UploadAsync(string containerName,
            IFormFileCollection files)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await _blobContainerClient.CreateIfNotExistsAsync();
            await _blobContainerClient.SetAccessPolicyAsync(PublicAccessType.BlobContainer);
            List<(string fileName, string pathContainerName)> datas = new List<(string fileName, string pathContainerName)>();
            foreach (IFormFile file in files)
            {
                string fileName = await FileReNameAsync(containerName, file.Name, HasFiles);
                BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
                await blobClient.UploadAsync(file.OpenReadStream());
                datas.Add((fileName,$"{containerName}/{fileName}"));
            }

            return datas;
        }
        public async Task DeleteAsync(string containerName, string fileName)
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            BlobClient blobClient = _blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }

        public List<string> GetFiles(string containerName )
        {
            _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Select(b => b.Name).ToList();
        }

        public bool HasFiles(string containerName, string fileName)
        {
            _blobServiceClient.GetBlobContainerClient(containerName);
            return _blobContainerClient.GetBlobs().Any(b=>b.Name==fileName);
        }
    }
}
