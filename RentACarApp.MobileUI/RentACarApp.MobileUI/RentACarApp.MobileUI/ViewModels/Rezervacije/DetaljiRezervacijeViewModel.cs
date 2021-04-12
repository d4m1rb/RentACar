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
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.Rezervacije
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetaljiRezervacijeViewModel : BaseViewModel
    {
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
        public int AutomobilId
        {
            get
            {
                return this.automobilId;
            }

            set
            {
                if (this.automobilId == value)
                {
                    return;
                }

                this.automobilId = value;
                this.NotifyPropertyChanged();
            }
        }
        public int RacunId
        {
            get
            {
                return this.racunId;
            }

            set
            {
                if (this.racunId == value)
                {
                    return;
                }

                this.racunId = value;
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
        public DateTime DatumKreiranja
        {
            get
            {
                return this.datumKreiranja;
            }

            set
            {
                if (this.datumKreiranja == value)
                {
                    return;
                }

                this.datumKreiranja = value;
                this.NotifyPropertyChanged();
            }
        }
        public string LokacijaPreuzimanja
        {
            get
            {
                return this.lokacijaPreuzimanja;
            }

            set
            {
                if (this.lokacijaPreuzimanja == value)
                {
                    return;
                }

                this.lokacijaPreuzimanja = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool VracanjeUposlovnicu
        {
            get
            {
                return this.vracanjeUposlovnicu;
            }

            set
            {
                if (this.vracanjeUposlovnicu == value)
                {
                    return;
                }

                this.vracanjeUposlovnicu = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool PrikaziBtnOcijeni
        {
            get
            {
                return this.prikaziBtnOcijeni;
            }

            set
            {
                if (this.prikaziBtnOcijeni == value)
                {
                    return;
                }

                this.prikaziBtnOcijeni = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool Nezapoceta
        {
            get
            {
                return this.nezapoceta;
            }

            set
            {
                if (this.nezapoceta == value)
                {
                    return;
                }

                this.nezapoceta = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Opis
        {
            get
            {
                return this.opis;
            }

            set
            {
                if (this.opis == value)
                {
                    return;
                }

                this.opis = value;
                this.NotifyPropertyChanged();
            }
        }
        public DateTime RezervacijaOd
        {
            get
            {
                return this.rezervacijaOd;
            }

            set
            {
                if (this.rezervacijaOd == value)
                {
                    return;
                }

                this.rezervacijaOd = value;
                this.NotifyPropertyChanged();
            }
        }
        public DateTime RezervacijaDo
        {
            get
            {
                return this.rezervacijaDo;
            }

            set
            {
                if (this.rezervacijaDo == value)
                {
                    return;
                }

                this.rezervacijaDo = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool KaskoOsiguranje
        {
            get
            {
                return this.kaskoOsiguranje;
            }

            set
            {
                if (this.kaskoOsiguranje == value)
                {
                    return;
                }

                this.kaskoOsiguranje = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool Otkazana
        {
            get
            {
                return this.otkazana;
            }

            set
            {
                if (this.otkazana == value)
                {
                    return;
                }

                this.otkazana = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool IsOcjenjena
        {
            get
            {
                return this.isOcjenjena;
            }

            set
            {
                if (this.isOcjenjena == value)
                {
                    return;
                }

                this.isOcjenjena = value;
                this.NotifyPropertyChanged();
            }
        }
        public float Ocjena
        {
            get
            {
                return this.ocjena;
            }

            set
            {
                if (this.ocjena == value)
                {
                    return;
                }

                this.ocjena = value;
                this.NotifyPropertyChanged();
            }
        }
        public double Popust
        {
            get
            {
                return this.popust;
            }

            set
            {
                if (this.popust == value)
                {
                    return;
                }

                this.popust = value;
                this.NotifyPropertyChanged();
            }
        }
        public decimal CijenaIznajmljivanja
        {
            get
            {
                return this.cijenaIznajmljivanja;
            }

            set
            {
                if (this.cijenaIznajmljivanja == value)
                {
                    return;
                }

                this.cijenaIznajmljivanja = value;
                this.NotifyPropertyChanged();
            }
        }
        public decimal Iznos
        {
            get
            {
                return this.iznos;
            }

            set
            {
                if (this.iznos == value)
                {
                    return;
                }

                this.iznos = value;
                this.NotifyPropertyChanged();
            }
        }
        public decimal IznosSaPopustom
        {
            get
            {
                return this.iznosSaPopustom;
            }

            set
            {
                if (this.iznosSaPopustom == value)
                {
                    return;
                }

                this.iznosSaPopustom = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Klijent {
            get
            {
                return this.klijent;
            }

            set
            {
                if (this.klijent == value)
                {
                    return;
                }

                this.klijent = value;
                this.NotifyPropertyChanged();
            }
        }
        public string RezervacijaInformacije
        {
            get
            {
                return this.rezervacijaInformacije;
            }

            set
            {
                if (this.rezervacijaInformacije == value)
                {
                    return;
                }

                this.rezervacijaInformacije = value;
                this.NotifyPropertyChanged();
            }
        }
        public string VoziloInformacije
        {
            get
            {
                return this.voziloInformacije;
            }

            set
            {
                if (this.voziloInformacije == value)
                {
                    return;
                }

                this.voziloInformacije = value;
                this.NotifyPropertyChanged();
            }
        }
        public string VoziloProizvodjacModel
        {
            get
            {
                return this.voziloProizvodjacModel;
            }

            set
            {
                if (this.voziloProizvodjacModel == value)
                {
                    return;
                }

                this.voziloProizvodjacModel = value;
                this.NotifyPropertyChanged();
            }
        }
        public string RezervacijaOdDo
        {
            get
            {
                return this.rezervacijaOdDo;
            }

            set
            {
                if (this.rezervacijaOdDo == value)
                {
                    return;
                }

                this.rezervacijaOdDo = value;
                this.NotifyPropertyChanged();
            }
        }
        public string RezervacijaBrojDana
        {
            get
            {
                return this.rezervacijaBrojDana;
            }

            set
            {
                if (this.rezervacijaBrojDana == value)
                {
                    return;
                }

                this.rezervacijaBrojDana = value;
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
                if (this.slikaThumb == value)
                {
                    return;
                }

                this.slikaThumb = value;
                this.NotifyPropertyChanged();
            }
        }
        public string VracanjeUposlovnicuString
        {
            get
            {
                return this.vracanjeUposlovnicuString;
            }

            set
            {
                if (this.vracanjeUposlovnicuString == value)
                {
                    return;
                }

                this.vracanjeUposlovnicuString = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool prikaziBtnOcijeni;
        public bool nezapoceta;

        public ICommand InitCommand { get; set; }

        #region Fields
        public int rezervacijaRentanjaId;
        public int racunId;
        public int automobilId;
        public int klijentId;
        public DateTime datumKreiranja;
        public string lokacijaPreuzimanja;
        public bool vracanjeUposlovnicu;
        public string opis;
        public DateTime rezervacijaOd;
        public DateTime rezervacijaDo;
        public bool kaskoOsiguranje;
        public bool otkazana;
        public bool isOcjenjena;
        public float ocjena;
        public double popust;
        public decimal cijenaIznajmljivanja;
        public decimal iznos;
        public decimal iznosSaPopustom;
        public string klijent;
        public string rezervacijaInformacije;
        public string voziloInformacije;
        public string voziloProizvodjacModel;
        public string rezervacijaOdDo;
        public string rezervacijaBrojDana;
        public byte[] slikaThumb;
        public string vracanjeUposlovnicuString { get; set; }



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
        /// Initializes a new instance for the <see cref="DetaljiRezervacijeViewModel" /> class.
        /// </summary>
        public DetaljiRezervacijeViewModel()
        {
            InitCommand = new Command(async () => await InitMethod());

            this.reviews = new ObservableCollection<Review>
            {
                new Review
                {
                    CustomerImage = "ProfileImage10.png",
                    CustomerName = "Serina Williams",
                    Comment = "Greatest purchase I have ever made in my life.",
                    ReviewedDate = "29 Dec, 2019",
                    Rating = 5,
                    Images = new List<string>
                    {
                        "Image1.png",
                        "Image1.png",
                        "Image1.png",
                        "Image1.png"
                    }
                },
                new Review
                {
                    CustomerImage = "ProfileImage11.png",
                    CustomerName = "Alise Valasquez",
                    Comment = "Absolutely love them! Can't stop wearing!",
                    ReviewedDate = "29 Dec, 2019",
                    Rating = 3,
                    Images = new List<string>
                    {
                       "Image1.png",
                       "Image1.png",
                       "Image1.png"
                    }
                }
            };

            this.ProductDetail = new Product
            {
                Name = "Full-Length Skirt",
                Summary = "This plaid, cotton skirt will keep you warm in the air-conditioned office or outside on cooler days.",
                ActualPrice = 245,
                DiscountPercent = 30,
                Description = "Ankle-length skirt with high waistband. Lightweight, comfortable cotton construction with side zipper.",
                PreviewImages = new List<string>
                {
                    "Image1.png",
                    "Image1.png",
                    "Image1.png",
                    "Image1.png",
                    "Image1.png",
                },

                Reviews = new ObservableCollection<Review>(reviews)
            };

            if (this.ProductDetail.Reviews == null || this.ProductDetail.Reviews.Count == 0)
            {
                this.IsReviewVisible = true;
            }
            else
            {
                foreach (var review in this.ProductDetail.Reviews)
                {
                    this.productRating += review.Rating;
                }
            }

            if (this.productRating > 0)
                this.ProductDetail.OverallRating = this.productRating / this.ProductDetail.Reviews.Count;

            this.Categories = new ObservableCollection<Category>
            {
                new Category
                {
                    Name = "Electronics",
                    SubCategories = new List<string>
                    {
                        "Laptops", "Mobiles", "Tablets", "Televisions", "Printers and Monitors"
                    }
                },
                new Category
                {
                    Name = "Fashion",
                    SubCategories = new List<string>
                    {
                        "Shirts", "Skirts", "Casual Wear", "Jeans", "Kurtis"
                    }
                },
                new Category
                {
                    Name = "Home and Furniture",
                    SubCategories = new List<string>
                    {
                        "Diwans", "Sofas", "Curtains"
                    }
                },
                new Category
                {
                    Name = "Personal Care",
                    SubCategories = new List<string>
                    {
                        "Laptops", "Mobiles", "Tablets", "Televisions", "Printers and Monitors"
                    }
                },
                new Category
                {
                    Name = "Sports and Books",
                    SubCategories = new List<string>
                    {
                        "Laptops", "Mobiles", "Tablets", "Televisions", "Printers and Monitors"
                    }
                },
                new Category
                {
                    Name = "Grocery",
                    SubCategories = new List<string>
                    {
                        "Laptops", "Mobiles", "Tablets", "Televisions", "Printers and Monitors"
                    }
                },
                new Category
                {
                    Name = "Toys and Baby",
                    SubCategories = new List<string>
                    {
                        "Laptops", "Mobiles", "Tablets", "Televisions", "Printers and Monitors"
                    }
                },
            };

            this.AddFavouriteCommand = new Command(this.AddFavouriteClicked);
            this.OKCommand = new Command(this.OKCommandClicked);
            this.OcijeniCommand = new Command(this.OcijeniCommandClicked);
            this.ShareCommand = new Command(this.ShareClicked);
            this.VariantCommand = new Command(this.VariantClicked);
            this.ItemSelectedCommand = new Command(this.ItemSelected);
            this.CardItemCommand = new Command(this.CartClicked);
            this.LoadMoreCommand = new Command(this.LoadMoreClicked);
            this.BackButtonCommand = new Command(BackButtonClicked);
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with SfRotator and labels, which display the product images and details.
        /// </summary>
        public Product ProductDetail
        {
            get
            {
                return this.productDetail;
            }

            set
            {
                if (this.productDetail == value)
                {
                    return;
                }

                this.productDetail = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with StackLayout, which displays the categories using ComboBox.
        /// </summary>
        public ObservableCollection<Category> Categories
        {
            get
            {
                return this.categories;
            }

            set
            {
                if (this.categories == value)
                {
                    return;
                }

                this.categories = value;
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

        /// <summary>
        /// Gets or sets the command that will be executed when the AddToCart button is clicked.
        /// </summary>
        public Command OKCommand { get; set; }
        public Command OcijeniCommand { get; set; }

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
            if (obj is DetaljiRezervacijeViewModel model)
            {
                model.ProductDetail.IsFavourite = !model.ProductDetail.IsFavourite;
            }
        }

        /// <summary>
        /// Invoked when the Cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void OKCommandClicked(object obj)
        {
           await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
            //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Settings.PostavkePage());
        }

        private async void OcijeniCommandClicked(object obj)
        {
            OcijeniRezervacijuViewModel model = new OcijeniRezervacijuViewModel();
            model.rezervacijaRentanjaId = RezervacijaRentanjaId;
            model.SelectedOcjena = 0;
            model.ocjenaString = "Odaberite ocjenu";

            for (int i = 1; i < 6; i++)
            {
                model.ListaOcjena.Add(i);
            }

            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.OcijeniRezervacijuPage(model));
            //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Settings.PostavkePage());
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
            HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
        }

        private readonly APIService _rezervacijaService = new APIService("RezervacijaRentanja");
        private readonly APIService _ocjenaService = new APIService("Ocjena");

        public async Task InitMethod()
        {
            
            RezervacijaRentanja x = await _rezervacijaService.GetById<RezervacijaRentanja>(RezervacijaRentanjaId);
            
            AutomobilId = x.AutomobilId;
            CijenaIznajmljivanja=x.CijenaIznajmljivanja;
            DatumKreiranja=x.DatumKreiranja;
            IsOcjenjena=x.IsOcjenjena;
            Iznos=x.Iznos;
            IznosSaPopustom=x.IznosSaPopustom;
            KaskoOsiguranje=x.KaskoOsiguranje;
            Klijent=x.Klijent;
            KlijentId=x.KlijentId;
            LokacijaPreuzimanja = x.LokacijaPreuzimanja;
            Ocjena = x.Ocjena;
            Opis = x.Opis;
            Otkazana = x.Otkazana;
            Popust = x.Popust;
            RacunId = x.RacunId;
            RezervacijaBrojDana = x.RezervacijaBrojDana;
            RezervacijaDo = x.RezervacijaDo;
            RezervacijaInformacije = x.RezervacijaInformacije;
            RezervacijaOd = x.RezervacijaOd;
            RezervacijaOdDo = x.RezervacijaOdDo;
            RezervacijaRentanjaId = x.RezervacijaRentanjaId;
            SlikaThumb = x.SlikaThumb;
            VoziloInformacije = x.VoziloInformacije;
            VoziloProizvodjacModel = x.VoziloProizvodjacModel;
            VracanjeUposlovnicu = x.VracanjeUposlovnicu;

            var ocjene = await _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { RezervacijaRentanjaId = x.RezervacijaRentanjaId });
            var ocjena = ocjene.FirstOrDefault();


            if (DateTime.Today >= RezervacijaOd)
            {
                PrikaziBtnOcijeni = true;
                Nezapoceta = false;
            }
            else
            {
                PrikaziBtnOcijeni = false;
                Nezapoceta = true;
            }

            if (ocjena != null)
            {
                Ocjena = ocjena.Ocjena1;
                IsOcjenjena = true;
                PrikaziBtnOcijeni = false;
            }
            else
            {
                Ocjena = 0;
                IsOcjenjena = false;
            }

            if (VracanjeUposlovnicu)
            {
                VracanjeUposlovnicuString = "Poslovnica";
            }
            else
            {
                VracanjeUposlovnicuString = LokacijaPreuzimanja;
            }
        }

       

        #endregion
    }
}
