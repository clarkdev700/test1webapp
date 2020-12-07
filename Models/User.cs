using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationTest1.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Name required")]
        [RegularExpression("^([a-zA-Z]{2,})[a-zA-Z\\s*]+$", ErrorMessage = "Numeric character not allowed!")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Surname required")]
        [RegularExpression("^([a-zA-Z]{2,})[a-zA-Z\\s*]+$", ErrorMessage = "Numeric character not allowed!")]
        public string Usersurname { get; set; }
        [Required(ErrorMessage = "CellPhone required")]
        [MinLength(8, ErrorMessage = "8 digits minimum is required")]
        [RegularExpression("[0-9]{8,}$", ErrorMessage = "Invalid phone number!")]
        public string UserCellPhone { get; set; }
    }
}