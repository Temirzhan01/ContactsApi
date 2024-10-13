using System.ComponentModel.DataAnnotations;

namespace ContactsApi.Dto
{
    public class ContactDto
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be netwwen 2 and 50 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Lastname must be netwwen 2 and 50 characters")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        [StringLength(12, MinimumLength = 11, ErrorMessage = "Lastname must be netwwen 2 and 50 characters")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; }
    }
}
