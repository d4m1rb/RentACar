using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAplikacija.ViewModels
{
    public class KontaktPoruka
    {
        public string Email { get; set; }
        public string Telefon { get; set; }
        public string Drzava { get; set; }

        public byte[] slikaThumb { get; set; }
    }
}
