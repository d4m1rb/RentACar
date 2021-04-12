using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentACarApp.Model.Requests
{
   public class AutomobiliUPSERTtRequest
    {
        public int AutomobilId { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int KategorijaId { get; set; }
        [Required]
        public int GodinaProizvodnje { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string SnagaMotora { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(20, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Kubikaza { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(20, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Transmisija { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(20, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string EmisioniStandard { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Gorivo { get; set; }
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Potrosnja { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(20, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Boja { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(10, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string BrojSjedista { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(10, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string BrojVrata { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        public bool Dostupan { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        public bool Novo { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public decimal CijenaIznajmljivanja { get; set; }
        public decimal CijenaKaskoOsiguranja { get; set; }

    }
}
