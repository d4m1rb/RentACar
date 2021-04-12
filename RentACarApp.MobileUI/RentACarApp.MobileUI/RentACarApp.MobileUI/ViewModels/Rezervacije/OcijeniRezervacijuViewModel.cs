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

namespace RentACarApp.MobileUI.ViewModels.Rezervacije
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class OcijeniRezervacijuViewModel : BaseViewModel
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");

        

        public ICommand InitCommand { get; set; }
        public Command BackButtonCommand { get; set; }

        public async Task Init()
        {
            
        }

        #region Fields

        public int ocjenaId;
        public int rezervacijaRentanjaId;
        public string napomena;
        public DateTime datumEvidentiranja;
        public int ocjena;
        public int selectedOcjena;
        public string ocjenaString;
        public ObservableCollection<int> listaOcjena = new ObservableCollection<int>();



        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="IzmjenaProfilaViewModel" /> class.
        /// </summary>
        public OcijeniRezervacijuViewModel()
        {
            InitCommand = new Command(async () => await Init());
            this.BackButtonCommand = new Command(BackButtonClicked);

        }

        #endregion

        #region Public properties

        public int OcjenaId
        {
            get
            {
                return this.ocjenaId;
            }

            set
            {
                this.ocjenaId = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<int> ListaOcjena
        {
            get
            {
                return this.listaOcjena;
            }

            set
            {
                this.listaOcjena = value;
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
                this.rezervacijaRentanjaId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Napomena
        {
            get
            {
                return this.napomena;
            }

            set
            {
                this.napomena = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime DatumEvidentiranja
        {
            get
            {
                return this.datumEvidentiranja;
            }

            set
            {
                this.datumEvidentiranja = value;
                this.NotifyPropertyChanged();
            }
        }

        public int Ocjena
        {
            get
            {
                return this.ocjena;
            }

            set
            {
                this.ocjena = value;
                this.NotifyPropertyChanged();
            }
        }


        public string OcjenaString
        {
            get
            {
                return this.ocjenaString;
            }

            set
            {
                this.ocjenaString = value;
                this.NotifyPropertyChanged();
            }
        }

        public int SelectedOcjena
        {
            get
            {
                return this.selectedOcjena;
            }

            set
            {
                this.selectedOcjena = value;
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