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
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ListaVozilaViewModel : BaseViewModel
    {
        private readonly APIService _vozilaService = new APIService("Automobil");
        private readonly APIService _ocjenaService = new APIService("Ocjena");
        private readonly APIService _kategorijaVozilaService = new APIService("KategorijaVozila");

        public ObservableCollection<AutomobilVM> VozilaList { get; set; } = new ObservableCollection<AutomobilVM>();
        public ObservableCollection<AutomobilVM> preporucenaVozilaList { get; set; } = new ObservableCollection<AutomobilVM>();
        public ObservableCollection<KategorijaVozila> kategorijaVozila { get; set; } = new ObservableCollection<KategorijaVozila>();
        public bool SortiranoUzlazno { get; set; } = false;
        public SfListView VozilaListSource { get; set; }

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
                    var ocjene = await _ocjenaService.Get<List<Ocjena>>(new OcjenaSearchRequest() { VoziloId = vozilo.AutomobilId });
                    decimal prosjecnaOcjena = 0;
                    bool prosjecnaBool = false;

                    if (ocjene.Count() != 0)
                    {
                        var sumaOcjena = 0; var brojOcjena = 0;
                        foreach (var ocjena in ocjene)
                        {
                            sumaOcjena += ocjena.Ocjena1;
                            brojOcjena++;
                        }

                        prosjecnaOcjena = sumaOcjena / (decimal)brojOcjena;
                        prosjecnaBool = true;
                    }

                    AutomobilVM vozilox = new AutomobilVM()
                    {
                        AutomobilId = vozilo.AutomobilId,
                        Boja = vozilo.Boja,
                        BrojSjedista = vozilo.BrojSjedista,
                        BrojVrata = vozilo.BrojVrata,
                        CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja,
                        CijenaKaskoOsiguranja = vozilo.CijenaKaskoOsiguranja,
                        Dostupan = vozilo.Dostupan,
                        DostupanTekst = vozilo.DostupanTekst,
                        EmisioniStandard = vozilo.EmisioniStandard,
                        GodinaProizvodnje = vozilo.GodinaProizvodnje,
                        Gorivo = vozilo.Gorivo,
                        KategorijaId = vozilo.KategorijaId,
                        Kubikaza = vozilo.Kubikaza,
                        ModelId = vozilo.ModelId,
                        Novo = vozilo.Novo,
                        Potrosnja = vozilo.Potrosnja,
                        ProizvodjacModel = vozilo.ProizvodjacModel,
                        RegistarskaOznaka = vozilo.RegistarskaOznaka,
                        ProsjecnaOcjena = 0,
                        RegistrovanDo = vozilo.RegistrovanDo,
                        Slika = vozilo.Slika,
                        SlikaThumb = vozilo.SlikaThumb,
                        SnagaMotora = vozilo.SnagaMotora,
                        Transmisija = vozilo.Transmisija
                    };

                    if (prosjecnaBool)
                    {
                        vozilox.ProsjecnaOcjena = prosjecnaOcjena;
                        vozilox.ImaProsjecnuOcjenu = true;
                        vozilox.NemaProsjecnuOcjenu = false;
                    }
                    else
                    {
                        vozilox.ImaProsjecnuOcjenu = false;
                        vozilox.NemaProsjecnuOcjenu = true;
                    }

                    VozilaList.Add(vozilox);

                    Recommender recommender = new Recommender();
                    var recommenderVozila = recommender.GetSlicnaVozila(vozilo.AutomobilId);

                    foreach (var item in recommenderVozila)
                    {
                        AutomobilVM itemx = new AutomobilVM()
                        {
                            AutomobilId = vozilo.AutomobilId,
                            Boja = vozilo.Boja,
                            BrojSjedista = vozilo.BrojSjedista,
                            BrojVrata = vozilo.BrojVrata,
                            CijenaIznajmljivanja = vozilo.CijenaIznajmljivanja,
                            CijenaKaskoOsiguranja = vozilo.CijenaKaskoOsiguranja,
                            Dostupan = vozilo.Dostupan,
                            DostupanTekst = vozilo.DostupanTekst,
                            EmisioniStandard = vozilo.EmisioniStandard,
                            GodinaProizvodnje = vozilo.GodinaProizvodnje,
                            Gorivo = vozilo.Gorivo,
                            KategorijaId = vozilo.KategorijaId,
                            Kubikaza = vozilo.Kubikaza,
                            ModelId = vozilo.ModelId,
                            Novo = vozilo.Novo,
                            Potrosnja = vozilo.Potrosnja,
                            ProizvodjacModel = vozilo.ProizvodjacModel,
                            ProsjecnaOcjena = 5,
                            RegistarskaOznaka = vozilo.RegistarskaOznaka,
                            RegistrovanDo = vozilo.RegistrovanDo,
                            Slika = vozilo.Slika,
                            SlikaThumb = vozilo.SlikaThumb,
                            SnagaMotora = vozilo.SnagaMotora,
                            Transmisija = vozilo.Transmisija
                        };

                        preporucenaVozilaList.Add(itemx);

                    }
                }
            }

        }

        #region Fields

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
        /// Initializes a new instance for the <see cref="ListaVozilaViewModel" /> class.
        /// </summary>
        public ListaVozilaViewModel()
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
            List<AutomobilVM> sortiranaLista;
           
            if (!SortiranoUzlazno)
            {
                sortiranaLista = VozilaList.OrderBy(x => x.CijenaIznajmljivanja).ToList();
                SortiranoUzlazno = true;
            }
            else
            {
                sortiranaLista = VozilaList.OrderByDescending(x => x.CijenaIznajmljivanja).ToList();
                SortiranoUzlazno = false;
            }

            VozilaListSource.ItemsSource = sortiranaLista;            
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