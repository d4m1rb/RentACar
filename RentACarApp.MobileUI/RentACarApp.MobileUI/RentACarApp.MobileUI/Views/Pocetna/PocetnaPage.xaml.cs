using RentACarApp.MobileUI.ViewModels.Pocetna;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Pocetna
{
    /// <summary>
    /// Page to show the health care details.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PocetnaPage : ContentPage
    {
      
        /// <summary>
        /// Initializes a new instance of the <see cref="PocetnaPage" /> class.
        /// </summary>
        public PocetnaViewModel model;
        public int KlijentID;
        public PocetnaPage(int KlijentId)
        {
            InitializeComponent();
            KlijentID = KlijentId;
            this.BindingContext = model = new PocetnaViewModel() { KlijentId=KlijentId};
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}
