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

namespace RentACarApp.MobileUI.ViewModels.Rezervacije
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ListaRezervacijaViewModel : BaseViewModel
    {



        #region Fields

        private readonly APIService _rezervacijeService = new APIService("RezervacijaRentanja");
        public int KlijentID;
        private int ukupnoRezervacija;
        private int ukupnoRezervacijaUToku;
        private int ukupnoRezervacijaZavrsenih;
        private decimal ukupnoUtroseno;
        Command sortCommand;
        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ListaRezervacijaViewModel" /> class.
        /// </summary>
        public ListaRezervacijaViewModel(int Klijent)
        {
            KlijentID = Klijent;
            InitCommand = new Command(async () => await Init());
            this.MenuCommand = new Command(this.MenuClicked);           

        }

        #endregion

        #region Public properties

        public ObservableCollection<RezervacijaRentanja> RezervacijeRetanjaList { get; set; } = new ObservableCollection<RezervacijaRentanja>();
        public ObservableCollection<RezervacijaRentanja> RezervacijeRetanjaListZavrsene { get; set; } = new ObservableCollection<RezervacijaRentanja>();
        public SfListView uTokuRezervacijeList;
        public SfListView zavrseneRezervacijeList;
        public bool SortiranoUzlaznoUToku { get; set; } = false;
        public bool SortiranoUzlaznoZavrsene { get; set; } = false;
        public bool switchToggledZavrsene { get; set; } = false;
        public int UkupnoRezervacija
        {
            set
            {
                if (this.ukupnoRezervacija == value)
                {
                    return;
                }

                this.ukupnoRezervacija = value;
                this.NotifyPropertyChanged();
            }
            get { return ukupnoRezervacija; }
        }
        public int UkupnoRezervacijaUToku
        {
            set
            {
                if (this.ukupnoRezervacijaUToku == value)
                {
                    return;
                }

                this.ukupnoRezervacijaUToku = value;
                this.NotifyPropertyChanged();
            }
            get { return ukupnoRezervacijaUToku; }
        }
        public int UkupnoRezervacijaZavrsenih
        {
            set
            {
                if (this.ukupnoRezervacijaZavrsenih == value)
                {
                    return;
                }

                this.ukupnoRezervacijaZavrsenih = value;
                this.NotifyPropertyChanged();
            }
            get { return ukupnoRezervacijaZavrsenih; }
        }
        public decimal UkupnoUtroseno
        {
            set
            {
                if (this.ukupnoUtroseno == value)
                {
                    return;
                }

                this.ukupnoUtroseno = value;
                this.NotifyPropertyChanged();
            }
            get { return ukupnoUtroseno; }
        }

        #endregion

        #region Command

        public ICommand InitCommand { get; set; }
        public Command MenuCommand { get; set; }
        ///// <summary>
        ///// Gets or sets the command that will be executed when an item is selected.
        ///// </summary>
        //public Command ItemSelectedCommand
        //{
        //    //get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        //}

        ///// <summary>
        ///// Gets or sets the command that will be executed when the sort button is clicked.
        ///// </summary>
        public Command SortCommand
        {
            get { return this.sortCommand ?? (this.sortCommand = new Command(this.Sortiraj)); }
        }

        ///// <summary>
        ///// Gets or sets the command that will be executed when the filter button is clicked.
        ///// </summary>
        //public Command FilterCommand
        //{
        //    //get { return this.filterCommand ?? (this.filterCommand = new Command(this.FilterClicked)); }
        //}

        ///// <summary>
        ///// Gets or sets the command that will be executed when the Favourite button is clicked.
        ///// </summary>
        //public Command AddFavouriteCommand
        //{
        //    get
        //    {
        //        //return this.addFavouriteCommand ?? (this.addFavouriteCommand = new Command(this.AddFavouriteClicked));
        //    }
        //}

        ///// <summary>
        ///// Gets or sets the command that will be executed when the AddToCart button is clicked.
        ///// </summary>
        //public Command AddToCartCommand
        //{
        //    //get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        //}

        ///// <summary>
        ///// Gets or sets the command will be executed when the cart icon button has been clicked.
        ///// </summary>
        //public Command CardItemCommand
        //{
        //    //get { return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked)); }
        //}

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
        private void Sortiraj(object attachedObject)
        {
            List<RezervacijaRentanja> sortiranaLista;
            if(!switchToggledZavrsene)
            {
                if (!SortiranoUzlaznoUToku)
                {
                    sortiranaLista = RezervacijeRetanjaList.OrderBy(x => x.IznosSaPopustom).ToList();
                    SortiranoUzlaznoUToku = true;
                }
                else
                {
                    sortiranaLista = RezervacijeRetanjaList.OrderByDescending(x => x.IznosSaPopustom).ToList();
                    SortiranoUzlaznoUToku = false;
                }

                uTokuRezervacijeList.ItemsSource = sortiranaLista;
            }
            else
            {
                if (!SortiranoUzlaznoZavrsene)
                {
                    sortiranaLista = RezervacijeRetanjaListZavrsene.OrderBy(x => x.IznosSaPopustom).ToList();
                    SortiranoUzlaznoZavrsene = true;
                }
                else
                {
                    sortiranaLista = RezervacijeRetanjaListZavrsene.OrderByDescending(x => x.IznosSaPopustom).ToList();
                    SortiranoUzlaznoZavrsene = false;
                }

                zavrseneRezervacijeList.ItemsSource = sortiranaLista;
            }
           
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

        public async Task Init()
        {
            if (KlijentID > 0)
            {
                RezervacijaRentanjaSearchRequest searchRequest = new RezervacijaRentanjaSearchRequest();
                searchRequest.KlijentId = KlijentID;
                searchRequest.Otkazana = false;

                var list = await _rezervacijeService.Get<IEnumerable<RentACarApp.Model.Models.RezervacijaRentanja>>(searchRequest);
                list = list.OrderByDescending(x => x.DatumKreiranja);

                int brojRezervacija = 0, uToku = 0, Zavrsene = 0;
                decimal ukupno = 0;
                RezervacijeRetanjaList.Clear();
                RezervacijeRetanjaListZavrsene.Clear();
                foreach (var item in list)
                {
                    if (item.RezervacijaOd > DateTime.Now)
                    {
                        RezervacijeRetanjaList.Add(item);
                        uToku++;
                    }
                    else
                    {
                        RezervacijeRetanjaListZavrsene.Add(item);
                        Zavrsene++;
                    }
                    ukupno += item.IznosSaPopustom;
                    brojRezervacija++;
                }                

                UkupnoRezervacija = brojRezervacija;
                UkupnoRezervacijaUToku = uToku;
                UkupnoRezervacijaZavrsenih = Zavrsene;
                UkupnoUtroseno = ukupno;
            }

        }

        private void MenuClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.IsPresented = true;
        }
        #endregion
    }
}