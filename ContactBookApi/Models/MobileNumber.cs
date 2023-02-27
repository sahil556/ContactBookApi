using System.ComponentModel.DataAnnotations.Schema;

namespace ContactBookApi.Models
{
    public class MobileNumber
    {
        public int Id { get; set; }
        public long Mobile { get; set; }

        public int? CountryCode { get; set; }

        [ForeignKey("ContactItem")]
        public long ContactItemId { get; set; }
    }
}
