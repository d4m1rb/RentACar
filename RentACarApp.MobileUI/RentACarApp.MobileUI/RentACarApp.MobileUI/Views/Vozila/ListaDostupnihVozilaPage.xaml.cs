using RentACarApp.Model.Models;
using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Vozila;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Syncfusion.ListView.XForms;

namespace RentACarApp.MobileUI.Views.Vozila
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaDostupnihVozilaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListaDostupnihVozilaPage" /> class.
        /// </summary>

        private ListaDostupnihVozilaViewModel model = null;
        public int KlijentID;
        public List<Automobil> ListaDostupnihVozila;
        public ListaDostupnihVozilaPage(InputModel inputM)
        {
            InitializeComponent();
            
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = new ListaDostupnihVozilaViewModel() { InputM=inputM,VozilaList=inputM._listaDostupnihVozila,Poruka=inputM._porukaDostupnaVozila, PorukaBool=inputM._porukaDostupnaVozilaBool };
            model.VozilaListSource = (SfListView)FindByName("ListViewList");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            //await model.Init();
        }

        private async void ListViewList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            var vozilo = e.ItemData as AutomobilVM;
            if (vozilo != null)
            {
                var AutomobilId = vozilo.AutomobilId;
                model.InputM._automobil = new RentACarApp.Model.Models.Automobil
                {
                    AutomobilId = AutomobilId
                };

                var inputM = model.InputM;                   

                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Vozila.DetaljiDostupnogVozilaPage(inputM));
            }
        }
    }
}