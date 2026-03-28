namespace VolunteerHub.Models
{
    public class BaseModel
    {
        public long CreatedAt { get; set; }
        public long ModifiedAt { get; set; }
        public bool Deleted { get; set; }
        public string? CreatedBy { get; set; }
        public string? LastModifiedBy { get; set; }
    }
}
