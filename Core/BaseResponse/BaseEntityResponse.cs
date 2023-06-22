namespace Core.BaseResponse
{
    public abstract class BaseEntityResponse
    {
        public int Id { get; set; }
        public bool Status { get; set; }

        //public DateTime CreatedDate { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public int ModifiedBy { get; set; }
    }
}
