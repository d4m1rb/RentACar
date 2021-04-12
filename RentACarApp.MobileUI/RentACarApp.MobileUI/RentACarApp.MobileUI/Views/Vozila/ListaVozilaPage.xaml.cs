using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Vozila;
using RentACarApp.Model.Models;
using Syncfusion.ListView.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Vozila
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaVozilaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListaVozilaPage" /> class.
        /// </summary>
        
        private ListaVozilaViewModel model = null;
        public int KlijentID;
        public ListaVozilaPage()
        {
            InitializeComponent();
           // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = new ListaVozilaViewModel();
            model.VozilaListSource = (SfListView)FindByName("ListViewList");
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();            
        }

        private async void ListViewList_ItemTapped(object sender, Syncfusion.ListView.XForms.ItemTappedEventArgs e)
        {
            //var myListView = (ListView)sender;
            //var myItem = myListView.SelectedItem;
            //model.VozilaList.IndexOf(myItem);            

            var vozilo = e.ItemData as AutomobilVM;
            if (vozilo != null)
            {
                var AutomobilId = vozilo.AutomobilId;

                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Vozila.DetaljiVozilaPage(AutomobilId));
            }
        }
    }
}