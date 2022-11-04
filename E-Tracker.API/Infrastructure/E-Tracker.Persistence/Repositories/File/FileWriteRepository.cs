using E_Tracker.Application.Repositories.File;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.File
{
    public class FileWriteRepository: WriteRepository<Domain.Entities.File>,IFileWriteRepository
    {
        public FileWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
