using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Vozila;
using Syncfusion.XForms.Buttons;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.Poruke
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class PorukaDetaljiViewModel : BaseViewModel
    {
       
        public Poruka porukaKlijent { get; set; }
        public Poruka porukaUposlenik { get; set; }

        public ICommand InitCommand { get; set; }

        #region Fields
        private string dostupanTekst;
        public int automobilId;
        public int modelId;
        public int kategorijaId;
        public int godinaProizvodnje;
        public string snagaMotora;
        public string kubikaza;
        public string transmisija;
        public string emisioniStandard;
        public string gorivo;
        public string potrosnja;
        public string boja;
        public string brojSjedista;
        public string brojVrata;
        public bool dostupan;
        public bool novo;
        public byte[] slika;
        public byte[] slikaThumb;
        public DateTime? registrovanDo;
        public string registarskaOznaka;
        public string proizvodjacModel;
        public decimal cijenaIznajmljivanja;
        public decimal cijenaKaskoOsiguranja;
        public InputModel inputModel;

        public int rezervacijaId;
        public int klijentId;


        private readonly double productRating;

        private Product productDetail;

        private ObservableCollection<Category> categories;

        private ObservableCollection<Review> reviews;

        private bool isFavourite;

        private bool isReviewVisible;

        private int? cartItemCount;

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance for the <see cref="DetaljiVozilaViewModel" /> class.
        /// </summary>
        public PorukaDetaljiViewModel()
        {
            InitCommand = new Command(async () => await InitMethod());
           
            this.CardItemCommand = new Command(this.CartClicked);

            this.BackButtonCommand = new Command(BackButtonClicked);
            this.OpenPorukePageCommand = new Command(OKButtonClicked);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with SfRotator and labels, which display the product images and details.
        /// </summary>
        /// 

        public Poruka PorukaKlijent
        {
            get
            {
                return this.porukaKlijent;
            }

            set
            {
                if (this.porukaKlijent == value)
                {
                    return;
                }

                this.porukaKlijent = value;
                this.NotifyPropertyChanged();
            }
        }

        public Poruka PorukaUposlenik
        {
            get
            {
                return this.porukaUposlenik;
            }

            set
            {
                if (this.porukaUposlenik == value)
                {
                    return;
                }

                this.porukaUposlenik = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RezervacijaId
        {
            get
            {
                return this.rezervacijaId;
            }

            set
            {
                if (this.rezervacijaId == value)
                {
                    return;
                }

                this.rezervacijaId = value;
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


        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the Favourite.
        /// </summary>
        public bool IsFavourite
        {
            get
            {
                return this.isFavourite;
            }
            set
            {
                this.isFavourite = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the empty message.
        /// </summary>
        public bool IsReviewVisible
        {
            get
            {
                if (productDetail.Reviews.Count == 0)
                {
                    this.isReviewVisible = true;
                }

                return this.isReviewVisible;
            }
            set
            {
                this.isReviewVisible = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public int? CartItemCount
        {
            get
            {
                return this.cartItemCount;
            }
            set
            {
                this.cartItemCount = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public Command AddFavouriteCommand { get; set; }

        public Command OpenPorukePageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command OpenReservationPageCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Share button is clicked.
        /// </summary>
        public Command ShareCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the size button is clicked.
        /// </summary>
        public Command VariantCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when cart button is clicked.
        /// </summary>
        public Command CardItemCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that will be executed when the Show All button is clicked.
        /// </summary>
        public Command LoadMoreCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the back button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the Favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddFavouriteClicked(object obj)
        {
            if (obj is DetaljiVozilaViewModel model)
            {
                model.ProductDetail.IsFavourite = !model.ProductDetail.IsFavourite;
            }
        }

        private async void OKButtonClicked(object obj)
        {
            

            
        }

        /// <summary>
        /// Invoked when the Cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void OtvoriRezervacijskuStranicu(object obj)
        {
            
            // Do something

            
        }

        /// <summary>
        /// Invoked when the Share button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void ShareClicked(object obj)
        {
            // Do something.
        }

        /// <summary>
        /// Invoked when the variant button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void VariantClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
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

        /// <summary>
        /// Invoked when Load more button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void LoadMoreClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when an back button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void BackButtonClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
        }

        private readonly APIService _vozilaService = new APIService("Automobil");
        public async Task InitMethod()
        {
           
        }

        #endregion
    }
}
