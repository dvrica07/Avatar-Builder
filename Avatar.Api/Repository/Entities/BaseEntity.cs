namespace Avatar.Api.Repository.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int ChangedBy { get; set; }  
        public DateTime ChangedOn { get; set; }

    }
}
