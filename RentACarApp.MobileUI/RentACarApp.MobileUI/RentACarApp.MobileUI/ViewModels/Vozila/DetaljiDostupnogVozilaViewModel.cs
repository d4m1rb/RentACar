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

namespace RentACarApp.MobileUI.ViewModels.Vozila
{
    /// <summary>
    /// ViewModel for detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class DetaljiDostupnogVozilaViewModel : BaseViewModel
    {
        public int AutomobilId {
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
        public int ModelId {
            get
            {
                return this.modelId;
            }

            set
            {
                if (this.modelId == value)
                {
                    return;
                }

                this.modelId = value;
                this.NotifyPropertyChanged();
            }
        }
        public int KategorijaId {
            get
            {
                return this.kategorijaId;
            }

            set
            {
                if (this.kategorijaId == value)
                {
                    return;
                }

                this.kategorijaId = value;
                this.NotifyPropertyChanged();
            }
        }
        public int GodinaProizvodnje {
            get
            {
                return this.godinaProizvodnje;
            }

            set
            {
                if (this.godinaProizvodnje == value)
                {
                    return;
                }

                this.godinaProizvodnje = value;
                this.NotifyPropertyChanged();
            }
        }
        public string SnagaMotora {
            get
            {
                return this.snagaMotora;
            }

            set
            {
                if (this.snagaMotora == value)
                {
                    return;
                }

                this.snagaMotora = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Kubikaza {
            get
            {
                return this.kubikaza;
            }

            set
            {
                if (this.kubikaza == value)
                {
                    return;
                }

                this.kubikaza = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Transmisija {
            get
            {
                return this.transmisija;
            }

            set
            {
                if (this.transmisija == value)
                {
                    return;
                }

                this.transmisija = value;
                this.NotifyPropertyChanged();
            }
        }
        public string EmisioniStandard {
            get
            {
                return this.emisioniStandard;
            }

            set
            {
                if (this.emisioniStandard == value)
                {
                    return;
                }

                this.emisioniStandard = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Gorivo {
            get
            {
                return this.gorivo;
            }

            set
            {
                if (this.gorivo == value)
                {
                    return;
                }

                this.gorivo = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Potrosnja {
            get
            {
                return this.potrosnja;
            }

            set
            {
                if (this.potrosnja == value)
                {
                    return;
                }

                this.potrosnja = value;
                this.NotifyPropertyChanged();
            }
        }
        public string Boja {
            get
            {
                return this.boja;
            }

            set
            {
                if (this.boja == value)
                {
                    return;
                }

                this.boja = value;
                this.NotifyPropertyChanged();
            }
        }
        public string BrojSjedista {
            get
            {
                return this.brojSjedista;
            }

            set
            {
                if (this.brojSjedista == value)
                {
                    return;
                }

                this.brojSjedista = value;
                this.NotifyPropertyChanged();
            }
        }
        public string BrojVrata {
            get
            {
                return this.brojVrata;
            }

            set
            {
                if (this.brojVrata == value)
                {
                    return;
                }

                this.brojVrata = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool Dostupan {
            get
            {
                return this.dostupan;
            }

            set
            {
                if (this.dostupan == value)
                {
                    return;
                }

                this.dostupan = value;
                this.NotifyPropertyChanged();
            }
        }
        public bool Novo {
            get
            {
                return this.novo;
            }

            set
            {
                if (this.novo == value)
                {
                    return;
                }

                this.novo = value;
                this.NotifyPropertyChanged();
            }
        }
        public byte[] Slika {
            get
            {
                return this.slika;
            }

            set
            {
                if (this.slika == value)
                {
                    return;
                }

                this.slika = value;
                this.NotifyPropertyChanged();
            }
        }
        public byte[] SlikaThumb {
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
        public DateTime? RegistrovanDo {
            get
            {
                return this.registrovanDo;
            }

            set
            {
                if (this.registrovanDo == value)
                {
                    return;
                }

                this.registrovanDo = value;
                this.NotifyPropertyChanged();
            }
        }
        public string RegistarskaOznaka {
            get
            {
                return this.registarskaOznaka;
            }

            set
            {
                if (this.registarskaOznaka == value)
                {
                    return;
                }

                this.registarskaOznaka = value;
                this.NotifyPropertyChanged();
            }
        }
        public string ProizvodjacModel {
            get
            {
                return this.proizvodjacModel;
            }

            set
            {
                if (this.proizvodjacModel == value)
                {
                    return;
                }

                this.proizvodjacModel = value;
                this.NotifyPropertyChanged();
            }
        }
        public string DostupanTekst {
            get
            {
                return this.dostupanTekst;
            }

            set
            {
                if (this.dostupanTekst == value)
                {
                    return;
                }

                this.dostupanTekst = value;
                this.NotifyPropertyChanged();
            }
        }
        public decimal CijenaIznajmljivanja {
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
        public decimal CijenaKaskoOsiguranja {
            get
            {
                return this.cijenaKaskoOsiguranja;
            }

            set
            {
                if (this.cijenaKaskoOsiguranja == value)
                {
                    return;
                }

                this.cijenaKaskoOsiguranja = value;
                this.NotifyPropertyChanged();
            }
        }

        public InputModel InputMod
        {
            get
            {
                return this.inputModel;
            }

            set
            {
                if (this.inputModel == value)
                {
                    return;
                }

                this.inputModel = value;
                this.NotifyPropertyChanged();
            }
        }

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
        public DetaljiDostupnogVozilaViewModel()
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
            this.OpenReservationPageCommand = new Command(this.OtvoriRezervacijskuStranicu);
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
            if (obj is DetaljiDostupnogVozilaViewModel model)
            {
                model.ProductDetail.IsFavourite = !model.ProductDetail.IsFavourite;
            }
        }

        /// <summary>
        /// Invoked when the Cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void OtvoriRezervacijskuStranicu(object obj)
        {
            
            // Do something

            //HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Catalog.RezervacijaPotvrdaPage(InputMod));
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Rezervacije.RezervacijaPotvrdaPage(InputMod));
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
            // Do something
        }

        private readonly APIService _vozilaService = new APIService("Automobil");
        public async Task InitMethod()
        {
            AutomobilSearchRequest search = new AutomobilSearchRequest() { AutomobilId = this.AutomobilId, Dostupan = true };
            List<Automobil> lista = await _vozilaService.Get<List<Automobil>>(search);
            Automobil x = lista.FirstOrDefault();
            InputMod._automobil =x;

            Boja = x.Boja;
            BrojSjedista = x.BrojSjedista;
            BrojVrata = x.BrojVrata;
            CijenaIznajmljivanja = x.CijenaIznajmljivanja;
            CijenaKaskoOsiguranja = x.CijenaKaskoOsiguranja;
            Dostupan = x.Dostupan;
            DostupanTekst = x.DostupanTekst;
            GodinaProizvodnje = x.GodinaProizvodnje;
            EmisioniStandard = x.EmisioniStandard;
            Gorivo = x.Gorivo;
            KategorijaId = x.KategorijaId;
            ModelId = x.ModelId;
            ProizvodjacModel = x.ProizvodjacModel;
            Kubikaza = x.Kubikaza;
            Novo = x.Novo;
            Potrosnja = x.Potrosnja;
            RegistarskaOznaka = x.RegistarskaOznaka;
            RegistrovanDo = x.RegistrovanDo;
            Slika = x.Slika;
            SlikaThumb = x.SlikaThumb;
            Transmisija = x.Transmisija;
            SnagaMotora = x.SnagaMotora;

        }

        #endregion
    }
}
