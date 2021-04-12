using RentACarApp.Model.Models;
using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Poruke;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Poruke
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaPorukaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListaDostupnihVozilaPage" /> class.
        /// </summary>

        private ListaPorukaViewModel model = null;
        public int KlijentID;
        public List<Automobil> ListaDostupnihVozila;
        public ListaPorukaPage(ListaPorukaViewModel inputM)
        {
            InitializeComponent();
            
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            this.BindingContext = model = new ListaPorukaViewModel() { listaPoruka=inputM.listaPoruka, RezervacijaId=inputM.RezervacijaId, KlijentId=inputM.KlijentId};
            var poruka = (Label)FindByName("PorukaLabel");
            if(model.listaPoruka.Count==0)
            {
                poruka.IsVisible = true;
            }
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await model.Init();
        }


    }
}