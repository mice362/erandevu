using NArchitecture.Core.Persistence.Repositories;

namespace Domain.Entities.Base;
public class BaseEntity<T> : IEntity<T> where T : struct
{
    public T Id { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;

    public int CreatedBy { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? ModifiedBy { get; set; }

    public bool IsDeleted { get; set; } = false;

    public bool IsActive { get; set; } = true;
}
