

using E_Tracker.Infrastructure.StaticServices;

namespace E_Tracker.Infrastructure.Services
{
    public class FileService 
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }


         
    }
}
