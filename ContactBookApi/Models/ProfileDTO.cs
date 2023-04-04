namespace ContactBookApi.Models
{
    public class ProfileDTO
    {
        public int UserId { get; set; }
        public string User_Name { get; set; } = string.Empty;
        public string Email { get; set; }

        public string Mobile { get; set; }

        public DateTime? birthdate { get; set; }

        public List<ContactItem>? contactItems { get; set; }
    }
}
