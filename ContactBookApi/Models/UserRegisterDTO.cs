namespace ContactBookApi.Models
{
    public class UserRegisterDTO
    { 
        public string User_Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty; 

        public string Password { get; set; } = string.Empty; 
        public DateTime? birthdate { get; set; }

    }
}
