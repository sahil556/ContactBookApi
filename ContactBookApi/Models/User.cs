namespace ContactBookApi.Models
{
    public class User
    {
        public int UserId { get; set; }

        public string User_Name { get; set; }

        public string Email { get; set;}

        public string Password { get; set;}

        public string Mobile { get; set;}    

        public string gender { get; set;}

        public string img_link { get; set;}

        public DateTime birthdate { get; set;}
    }
}
