using System.ComponentModel.DataAnnotations.Schema;
using E_Tracker.Domain.Entities.Common;

namespace E_Tracker.Domain.Entities
{
    public class File: BaseEntity
    {
        public string FileName { get; set; }
        public string Path { get; set; }
        public string Srorage { get; set; }
        [NotMapped]
        public override DateTime UpdateDate { get; set; }
    }
}
