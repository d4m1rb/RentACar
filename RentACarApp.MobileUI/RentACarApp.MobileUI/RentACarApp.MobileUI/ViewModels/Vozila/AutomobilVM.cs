using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Syncfusion.ListView.XForms;
using RentACarApp.MobileUI.Models;
using RentACarApp.Model.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using RentACarApp.Model.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using RentACarApp.MobileUI.Helpers;
using System.Linq;

namespace RentACarApp.MobileUI.ViewModels.Vozila
{  
    public class AutomobilVM : BaseViewModel
    {
        public int AutomobilId { get; set; }
        public int ModelId { get; set; }
        public int KategorijaId { get; set; }
        public int GodinaProizvodnje { get; set; }
        public string SnagaMotora { get; set; }
        public string Kubikaza { get; set; }
        public string Transmisija { get; set; }
        public string EmisioniStandard { get; set; }
        public string Gorivo { get; set; }
        public string Potrosnja { get; set; }
        public string Boja { get; set; }
        public string BrojSjedista { get; set; }
        public string BrojVrata { get; set; }
        public bool Dostupan { get; set; }
        public bool Novo { get; set; }
        public byte[] Slika { get; set; }
        public byte[] SlikaThumb { get; set; }
        public DateTime? RegistrovanDo { get; set; }
        public string RegistarskaOznaka { get; set; }
        public string ProizvodjacModel { get; set; }
        public string DostupanTekst { get; set; }
        public decimal CijenaIznajmljivanja { get; set; }
        public decimal CijenaKaskoOsiguranja { get; set; }
        public decimal ProsjecnaOcjena { get; set; }
        public bool ImaProsjecnuOcjenu { get; set; }
        public bool NemaProsjecnuOcjenu { get; set; }
    }
}