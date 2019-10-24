using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace eCom.Web.ViewModels
{
    public class UserViewModel
    {
        [Required(ErrorMessage = "Name is required.")]
        [MinLength(10,ErrorMessage = "Name must be longer.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone is required.")]
        [DataType(DataType.PhoneNumber)]
        [MaxLength(50)]
        [RegularExpression(@"^(\(?[0-9]{3}\)?)?\-?[0-9]{3}\-?[0-9]{4}$", ErrorMessage = "Invalid phone number.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}