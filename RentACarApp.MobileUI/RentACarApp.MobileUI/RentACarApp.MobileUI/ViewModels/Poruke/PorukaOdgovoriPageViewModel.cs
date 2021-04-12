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

namespace RentACarApp.MobileUI.ViewModels.Poruke
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class PorukaOdgovoriViewModel : BaseViewModel
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");
            
      
        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
        }

        #region Fields

        public int porukaId;
        public int klijentId;
        public int uposlenikId;
        public int rezervacijaRentanjaId;

        public string sadrzaj;
        public string naslov;
        public string posiljaocInfo;
        public string primaocInfo;
        public bool procitano;

        private Command addToCartCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="RezervacijaDatumViewModel" /> class.
        /// </summary>
        public PorukaOdgovoriViewModel()
        {
            InitCommand = new Command(async () => await Init());
            this.BackButtonCommand = new Command(BackButtonClicked);

        }

        #endregion

        #region Public properties

       
        public int PorukaId
        {
            get
            {
                return this.porukaId;
            }

            set
            {
                if (this.porukaId == value)
                {
                    return;
                }

                this.porukaId = value;
                this.NotifyPropertyChanged();
            }
        }

        public int KlijentId
        {
            get
            {
                return this.klijentId;
            }

            set
            {
                if (this.klijentId == value)
                {
                    return;
                }

                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }

        public int UposlenikId
        {
            get
            {
                return this.uposlenikId;
            }

            set
            {
                if (this.uposlenikId == value)
                {
                    return;
                }

                this.uposlenikId = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RezervacijaRentanjaId
        {
            get
            {
                return this.rezervacijaRentanjaId;
            }

            set
            {
                if (this.rezervacijaRentanjaId == value)
                {
                    return;
                }

                this.rezervacijaRentanjaId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Sadrzaj
        {
            get
            {
                return this.sadrzaj;
            }

            set
            {
                if (this.sadrzaj == value)
                {
                    return;
                }

                this.sadrzaj = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Naslov
        {
            get
            {
                return this.naslov;
            }

            set
            {
                if (this.naslov == value)
                {
                    return;
                }

                this.naslov = value;
                this.NotifyPropertyChanged();
            }
        }

        public string PosiljaocInfo
        {
            get
            {
                return this.posiljaocInfo;
            }

            set
            {
                if (this.posiljaocInfo == value)
                {
                    return;
                }

                this.posiljaocInfo = value;
                this.NotifyPropertyChanged();
            }
        }

        public string PrimaocInfo
        {
            get
            {
                return this.primaocInfo;
            }

            set
            {
                if (this.primaocInfo == value)
                {
                    return;
                }

                this.primaocInfo = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Procitano
        {
            get
            {
                return this.procitano;
            }

            set
            {
                if (this.procitano == value)
                {
                    return;
                }

                this.procitano = value;
                this.NotifyPropertyChanged();
            }
        }




        #endregion

        #region Command

        
        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command AddToCartCommand
        {
            get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        }

        /// <summary>
        /// Gets or sets the command will be executed when the cart icon button has been clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        #endregion

        #region Methods


        private void AddToCartClicked(object obj)
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