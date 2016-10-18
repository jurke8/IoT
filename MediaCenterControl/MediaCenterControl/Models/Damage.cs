using System;
using System.ComponentModel.DataAnnotations;

namespace MediaCenterControl.Models
{
    public class Damage
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [Display(Name = "Marka automobila")]
        public string CarBrand { get; set; }

        [Display(Name = "Model automobila")]
        public string CarModel { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [Display(Name = "Opis kvara")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Ovo polje je obavezno.")]
        [Display(Name = "Servisni centar")]
        public string ServiceCenter { get; set; }

        [Display(Name = "Datum servisa")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d.M.yyyy.}", ApplyFormatInEditMode = true)]
        public DateTime? ServiceDate { get; set; }

        [Display(Name = "Datum unosa")]
        [DataType(DataType.Date)]
        public DateTime DateCreated { get; set; }

        public Guid UserCreated { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsArchived { get; set; }

        public Damage()
        {
            Id = Guid.NewGuid();
        }
    }
}