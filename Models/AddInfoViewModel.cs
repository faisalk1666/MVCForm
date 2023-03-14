using System.ComponentModel.DataAnnotations;

namespace DemoMVC.Models
{
    public class AddInfoViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [StringLength(50, ErrorMessage = "Name must not exceed 50 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [Range(1000000000, 9999999999, ErrorMessage = "Phone number should be 10 digits")]
        public long Phone { get; set; }

        [Required(ErrorMessage = "Stored From is required.")]
        public string StoredFrom { get; set; }
    }
}
