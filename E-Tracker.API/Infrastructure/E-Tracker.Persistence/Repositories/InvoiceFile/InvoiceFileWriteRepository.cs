using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Tracker.Application.Repositories.InvoiceFile;
using E_Tracker.Persistence.Contexts;

namespace E_Tracker.Persistence.Repositories.InvoiceFile
{
    public class InvoiceFileWriteRepository: WriteRepository<Domain.Entities.InvoiceFile>, IInvoiceFileWriteRepository
    {
        public InvoiceFileWriteRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
