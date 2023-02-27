using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBookApi.Models
{
    public class Address
    {
        public int Id { get; set; }

        [ForeignKey("ContactItem")]
        public long ContactItemId { get; set; }

        public int? House_no { get; set; }

        public string? Street_Name { get; set; }

        public string City { get; set; }

        public int PostalCode { get; set; }

        public string District { get; set; }

        public string? Country { get; set; }

    }
}
