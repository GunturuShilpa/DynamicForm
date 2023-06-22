namespace Infrastructure.Form.Entity
{
    public class TemplateForms
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public bool Status { get; set; }

        public int Ordinal { get; set; }    
        
        //public DateTime CreatedDate { get; set; }
        //public int CreatedBy { get; set; }
        //public DateTime ModifiedDate { get; set; }
        //public int ModifiedBy { get; set; }
    }
}
