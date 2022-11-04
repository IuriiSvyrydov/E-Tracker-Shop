using E_Tracker.Application.Repositories.File;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.File
{
    public class FileReadRepository: ReadRepository<Domain.Entities.File>,IFileReadRepository
    {
        public FileReadRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
