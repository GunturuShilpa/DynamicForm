namespace Infrastructure.Base.Entity
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        public virtual int Status { get; set; }
    }

    public abstract class TableEntity : BaseEntity
    {
        public virtual DateTime? CreatedDate { get; set; }

        public virtual int CreatedBy { get; set; }

        public virtual DateTime? ModifiedDate { get; set; }

        public virtual int ModifiedBy { get; set; }
    }
}
