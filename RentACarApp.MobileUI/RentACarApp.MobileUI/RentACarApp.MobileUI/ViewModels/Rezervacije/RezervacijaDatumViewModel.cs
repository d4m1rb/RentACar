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
    public class RezervacijaDatumViewModel : BaseViewModel
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");

        public ObservableCollection<Automobil> VozilaList { get; set; } = new ObservableCollection<Automobil>();
        public ObservableCollection<Automobil> preporucenaVozilaList { get; set; } = new ObservableCollection<Automobil>();
        public ObservableCollection<KategorijaVozila> kategorijaVozila { get; set; } = new ObservableCollection<KategorijaVozila>();


        KategorijaVozila _selectedKategorijaVozila = null;

        public KategorijaVozila SelectedKategorijaVozila
        {
            get { return _selectedKategorijaVozila; }
            set
            {
                
                if (value != null)
                {
                    InitCommand.Execute(null);
                }
                if (this._selectedKategorijaVozila == value)
                {
                    return;
                }

            }
        }

        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
            if (kategorijaVozila.Count == 0)
            {
                var kategorijaVozilaList = await _kategorijaVozilaService.Get<List<KategorijaVozila>>(null);

                foreach (var kategorija in kategorijaVozilaList)
                {
                    kategorijaVozila.Add(kategorija);
                }
            }
            AutomobilSearchRequest search = new AutomobilSearchRequest();

            if (SelectedKategorijaVozila != null)
            {
                search.KategorijaId = _selectedKategorijaVozila.KategorijaId;
            }
            search.Dostupan = true;
            var list = await _vozilaService.Get<IEnumerable<Automobil>>(search);

            VozilaList.Clear();
            preporucenaVozilaList.Clear();
            foreach (var vozilo in list)
            {

                if ((vozilo.RegistrovanDo - DateTime.Now).Value.Days > 15)
                {
                    VozilaList.Add(vozilo);
                    Recommender recommender = new Recommender();
                    var recommenderVozila = recommender.GetSlicnaVozila(vozilo.AutomobilId);

                    foreach (var item in recommenderVozila)
                    {
                        preporucenaVozilaList.Add(item);

                    }
                }
            }

        }

        #region Fields

        private DateTime rezervacijaOd;
        private DateTime rezervacijaDo;
        private string poruka;
        private bool porukaBool;
        private InputModel inputModel;

        private ObservableCollection<Category> filterOptions;

        private ObservableCollection<string> sortOptions;

        private Command addFavouriteCommand;

        private Command itemSelectedCommand;

        private Command sortCommand;

        private Command filterCommand;

        private Command addToCartCommand;

        public Command cardItemCommand;

        private string cartItemCount;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="RezervacijaDatumViewModel" /> class.
        /// </summary>
        public RezervacijaDatumViewModel()
        {
            InitCommand = new Command(async () => await Init());
            this.MenuCommand = new Command(this.MenuClicked);

            this.FilterOptions = new ObservableCollection<Category>
            {
                new Category
                {
                    Name = "Gender",
                    SubCategories = new List<string>
                    {
                        "Men",
                        "Women"
                    }
                },
                new Category
                {
                    Name = "Brand",
                    SubCategories = new List<string>
                    {
                        "Brand A",
                        "Brand B"
                    }
                },
                new Category
                {
                    Name = "Categories",
                    SubCategories = new List<string>
                    {
                        "Category A",
                        "Category B"
                    }
                },
                new Category
                {
                    Name = "Color",
                    SubCategories = new List<string>
                    {
                        "Maroon",
                        "Pink"
                    }
                },
                new Category
                {
                    Name = "Price",
                    SubCategories = new List<string>
                    {
                        "Above 3000",
                        "1000 to 3000",
                        "Below 1000"
                    }
                },
                new Category
                {
                    Name = "Size",
                    SubCategories = new List<string>
                    {
                        "S", "M", "L", "XL", "XXL"
                    }
                },
                new Category
                {
                    Name = "Patterns",
                    SubCategories = new List<string>
                    {
                        "Pattern 1", "Pattern 2"
                    }
                },
                new Category
                {
                    Name = "Offers",
                    SubCategories = new List<string>
                    {
                        "Buy 1 Get 1", "Buy 1 Get 2"
                    }
                },
                new Category
                {
                    Name = "Coupons",
                    SubCategories = new List<string>
                    {
                        "Coupon 1", "Coupon 2"
                    }
                },
            };

            this.SortOptions = new ObservableCollection<string>
            {
                "New Arrivals",
                "Price - high to low",
                "Price - Low to High",
                "Popularity",
                "Discount"
            };
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the item details in tile.
        /// </summary>
        [DataMember(Name = "products")]
        public ObservableCollection<Product> Products
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the property that has been bound with a list view, which displays the filter options.
        /// </summary>
        /// 

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

        public string Poruka
        {
            get
            {
                return this.poruka;
            }

            set
            {
                if (this.poruka == value)
                {
                    return;
                }

                this.poruka = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool PorukaBool
        {
            get
            {
                return this.porukaBool;
            }

            set
            {
                if (this.porukaBool == value)
                {
                    return;
                }

                this.porukaBool = value;
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
        public ObservableCollection<Category> FilterOptions
        {
            get
            {
                return this.filterOptions;
            }

            set
            {
                if (this.filterOptions == value)
                {
                    return;
                }

                this.filterOptions = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property has been bound with a list view, which displays the sort details.
        /// </summary>
        public ObservableCollection<string> SortOptions
        {
            get
            {
                return this.sortOptions;
            }

            set
            {
                if (this.sortOptions == value)
                {
                    return;
                }

                this.sortOptions = value;
                this.NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gets or sets the property that has been bound with view, which displays the cart items count.
        /// </summary>
        public string CartItemCount
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

        #region Command

        /// <summary>
        /// Gets or sets the command that will be executed when an item is selected.
        /// </summary>
        public Command ItemSelectedCommand
        {
            get { return this.itemSelectedCommand ?? (this.itemSelectedCommand = new Command(this.ItemSelected)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the sort button is clicked.
        /// </summary>
        public Command SortCommand
        {
            get { return this.sortCommand ?? (this.sortCommand = new Command(this.SortClicked)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the filter button is clicked.
        /// </summary>
        public Command FilterCommand
        {
            get { return this.filterCommand ?? (this.filterCommand = new Command(this.FilterClicked)); }
        }

        /// <summary>
        /// Gets or sets the command that will be executed when the Favourite button is clicked.
        /// </summary>
        public Command AddFavouriteCommand
        {
            get
            {
                return this.addFavouriteCommand ?? (this.addFavouriteCommand = new Command(this.AddFavouriteClicked));
            }
        }

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
        public Command CardItemCommand
        {
            get { return this.cardItemCommand ?? (this.cardItemCommand = new Command(this.CartClicked)); }
        }

        public Command MenuCommand { get; set; }

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

        private void MenuClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.IsPresented = true;
        }
        #endregion
    }
}