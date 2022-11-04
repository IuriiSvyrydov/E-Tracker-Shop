namespace E_Tracker.Domain.Entities.Common;

public class BaseEntity
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public virtual DateTime UpdateDate { get; set; }
}