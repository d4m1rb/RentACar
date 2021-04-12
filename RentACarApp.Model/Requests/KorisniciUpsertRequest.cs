using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RentACarApp.Model.Requests
{
    public class KorisniciUpsertRequest
    {
        public int KorisnikId { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(50)]
        public string Prezime { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [EmailAddress(ErrorMessage = "{0} biti u formatu email adrese")]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Email { get; set; }
        [MaxLength(20, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        [Phone(ErrorMessage ="{0} biti u formatu broja telefona")]
        public string Telefon { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        public bool Status { get; set; }
        [Required(ErrorMessage = "{0} je obavezno polje")]
        public int GradId { get; set; }
        [MaxLength(100, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Adresa { get; set; }
        [DataType(DataType.Password)]
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        public string Password { get; set; }
        [MaxLength(50, ErrorMessage = "Polje {0} ne smije biti duže od {1} karaktra")]
        [Compare("Password", ErrorMessage = "Polje Password i PasswordPotvrda se moraju podudarati")]
        public string PasswordPotvrda { get; set; }
        public byte[] Slika{ get; set; }
        public byte[] SlikaThumb { get; set; }
        public DateTime? DatumRodjenja { get; set; }
        public DateTime DatumRegistracije { get; set; }
        public List<int> Uloge { get; set; } = new List<int>();
    }
}
