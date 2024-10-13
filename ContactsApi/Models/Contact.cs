using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Models
{
    public class Contact
    {
        [Key]
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
}
