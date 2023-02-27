using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBookApi.Models
{
    public class ContactItem
    {
        public long Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public bool? IsFavourite { get; set; }

        public string? LinkedinUrl { get; set; }

        public string? NickName { get; set; }

        public string? Relationship { get; set; }

        public List<Address>? Addresses { get; set; }

        public List<MobileNumber>? mobileNumbers { get; set; }   

        

    }
}
