namespace Softqu.Domain.Shared
{
    public abstract class AggregateRoot : AuditableEntity
    {
        public bool IsDeleted { get; protected set; }
        public DateTime? DeletionTime { get; protected set; }

        public virtual void Delete()
        {
            IsDeleted = true;
            DeletionTime = DateTime.UtcNow;
        }
    }
}
