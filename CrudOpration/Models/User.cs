using System.ComponentModel.DataAnnotations;

namespace CrudOpration.Models
{
    public class User
    {
        public int? Id { get; set; }

        [RegularExpression(@"^[a-zA-Z.]+$", ErrorMessage = "Please Enter Only characters and dots")]
        [Required(ErrorMessage = "Please enter login userId")]
        public string LoginUserId { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only characters")]
        [Required(ErrorMessage = "Please enter user name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter your address")]
        public string Address { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter correct contact number")]
        [Required(ErrorMessage = "Please enter Contact number")]
        public string ContactNumber { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "Please enter correct Mobile number")]
        [Required(ErrorMessage = "Please enter Mobile number")]
        public string MobileNumber { get; set; }

        [EmailAddress(ErrorMessage = "Please enter valid email")]
        [Required(ErrorMessage = "Please enter email")]
        public string EmailId { get; set; }
        public bool OTPVerify { get; set; }
        public bool Tab { get; set; }

        [Required(ErrorMessage = "Please Select user Layer")]
        public string UserLayer { get; set; }

        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Please Enter Only characters")]
        [Required(ErrorMessage = "Please enter remark")]
        public string Remark { get; set; }
        public bool IsActive { get; set; }
    }
}
