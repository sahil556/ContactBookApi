namespace ContactBookApi.Models
{
    public class ContactItemDTO
    {
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string Surname { get; set; }

        public string? Email { get; set; }

        public long Mobile { get; set; }

        public long? Mobile2 { get; set; }

        public DateTime? BirthDate { get; set; }

        public string Gender { get; set; }

        public bool? IsFavourite { get; set; }

        public int? House_no { get; set; }

        public string? Street_Name { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string District { get; set; }

        public string? LinkedinUrl { get; set; }

        public string? Country { get; set; }
    }
}
