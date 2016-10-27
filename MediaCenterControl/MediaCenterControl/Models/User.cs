using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Resources;

namespace MediaCenterControl.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }

        [Display(Name = "UserName", ResourceType = typeof(Localization))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        public string Username { get; set; }

        [NotMapped]
        [Display(Name = "Password", ResourceType = typeof(Localization))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [DataType(DataType.Password)]
        public string PasswordView { get; set; }

        public byte[] Password { get; set; }

        [NotMapped]
        [Display(Name = "ConfirmPassword", ResourceType = typeof(Localization))]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Compare("PasswordView", ErrorMessageResourceName ="PasswordField", ErrorMessageResourceType = typeof(Localization))]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
        [NotMapped]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "IpAddress", ResourceType = typeof(Localization))]
        public string IpAddress { get; set; }
        [NotMapped]
        [Required(ErrorMessageResourceName = "RequiredField", ErrorMessageResourceType = typeof(Localization))]
        [Display(Name = "Port", ResourceType = typeof(Localization))]
        public string Port { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}