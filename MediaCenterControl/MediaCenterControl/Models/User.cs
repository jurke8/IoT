using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MediaCenterControl.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "Korisničko ime")]
        [Required(ErrorMessage = "Ovo polje je obvezno.")]
        public string Username { get; set; }

        [Display(Name = "Lozinka")]
        [Required(ErrorMessage = "Ovo polje je obvezno.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [NotMapped]
        [Display(Name = "Potvrda lozinke")]
        [Required(ErrorMessage = "Ovo polje je obvezno.")]
        [Compare("Password", ErrorMessage = "Lozinke se ne podudaraju.")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}