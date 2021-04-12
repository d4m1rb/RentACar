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

namespace RentACarApp.MobileUI.ViewModels.Profil
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class IzmjenaProfilaViewModel : BaseViewModel
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");

        

        public ICommand InitCommand { get; set; }
        public Command BackButtonCommand { get; set; }

        public async Task Init()
        {
           

        }

        #region Fields

        public int klijentId;
        public string ime;
        public string prezime;
        public string userName;
        public string email;
        public DateTime datumRodjenja;
        public DateTime datumRegistracije;
        public string telefon;
        public int gradId;
        public string adresa;
        public string lozinkaHash;
        public string lozinkaSalt;
        public bool status;
        public byte[] slika;
        public byte[] slikaThumb;
        public string slikaPath;
        public string gradString;
        public string imePrezime;
        public string statusString;
        public Grad selectedGrad;
        public ObservableCollection<Grad> gradovi;
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="IzmjenaProfilaViewModel" /> class.
        /// </summary>
        public IzmjenaProfilaViewModel()
        {
            InitCommand = new Command(async () => await Init());
            this.BackButtonCommand = new Command(BackButtonClicked);

        }

        #endregion

        #region Public properties

        public int KlijentId
        {
            get
            {
                return this.klijentId;
            }

            set
            {
                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Ime
        {
            get
            {
                return this.ime;
            }

            set
            {
                this.ime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Prezime
        {
            get
            {
                return this.prezime;
            }

            set
            {
                this.prezime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime DatumRodjenja
        {
            get
            {
                return this.datumRodjenja;
            }

            set
            {
                this.datumRodjenja = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime DatumRegistracije
        {
            get
            {
                return this.datumRegistracije;
            }

            set
            {
                this.datumRegistracije = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Telefon
        {
            get
            {
                return this.telefon;
            }

            set
            {
                this.telefon = value;
                this.NotifyPropertyChanged();
            }
        }

        public int GradId
        {
            get
            {
                return this.gradId;
            }

            set
            {
                this.gradId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string LozinkaHash
        {
            get
            {
                return this.lozinkaHash;
            }

            set
            {
                this.lozinkaHash = value;
                this.NotifyPropertyChanged();
            }
        }

        public string LozinkaSalt
        {
            get
            {
                return this.lozinkaSalt;
            }

            set
            {
                this.lozinkaSalt = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.NotifyPropertyChanged();
            }
        }

        public byte[] Slika
        {
            get
            {
                return this.slika;
            }

            set
            {
                this.slika = value;
                this.NotifyPropertyChanged();
            }
        }

        public byte[] SlikaThumb
        {
            get
            {
                return this.slikaThumb;
            }

            set
            {
                this.slikaThumb = value;
                this.NotifyPropertyChanged();
            }
        }

        public string SlikaPath
        {
            get
            {
                return this.slikaPath;
            }

            set
            {
                this.slikaPath = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Adresa
        {
            get
            {
                return this.adresa;
            }

            set
            {
                this.adresa = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
                this.NotifyPropertyChanged();
            }
        }

        public string ImePrezime
        {
            get
            {
                return this.imePrezime;
            }

            set
            {
                this.imePrezime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string GradString
        {
            get
            {
                return this.gradString;
            }

            set
            {
                this.gradString = value;
                this.NotifyPropertyChanged();
            }
        }

        public Grad SelectedGrad
        {
            get
            {
                return this.selectedGrad;
            }

            set
            {
                this.selectedGrad = value;
                this.NotifyPropertyChanged();
            }
        }

        public string StatusString
        {
            get
            {
                return this.statusString;
            }

            set
            {
                this.statusString = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Grad> Gradovi
        {
            get
            {
                return this.gradovi;
            }

            set
            {
                this.gradovi = value;
                this.NotifyPropertyChanged();
            }
        }

       

        #endregion

        #region Command

        
        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the items are sorted.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void SortClicked(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the filter button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void FilterClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddFavouriteClicked(object obj)
        {
            if (obj is Product product)
                product.IsFavourite = !product.IsFavourite;
        }

        /// <summary>
        /// Invoked when the cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddToCartClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private void CartClicked(object obj)
        {
            // Do something
        }
        private void BackButtonClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
        }
        #endregion
    }
}