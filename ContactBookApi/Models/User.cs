namespace ContactBookApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string User_Name { get; set; } = string.Empty;

        public string Email { get; set;}

        public byte[] PasswordHash { get; set; } = Array.Empty<byte>();

        public byte[] PasswordSalt { get; set; } = Array.Empty<byte>();

        public string Mobile { get; set;}    

        public DateTime? birthdate { get; set;}

        public List<ContactItem>? contactItems { get; set;}
    }
}
