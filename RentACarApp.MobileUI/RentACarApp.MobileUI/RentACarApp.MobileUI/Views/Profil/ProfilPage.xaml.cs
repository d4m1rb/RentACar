using RentACarApp.Model.Models;
using RentACarApp.MobileUI.ViewModels.Profil;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Profil
{
    /// <summary>
    /// Page to show Contact profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilPage
    {
        
        private ProfilViewModel model = null;
        public ProfilPage(int klijent)
        {
            InitializeComponent();
            BindingContext = model = new ProfilViewModel() { KlijentId = klijent};

            var img = (Image)FindByName("SlikaProfila");
            model.slImage = img;

           // this.ProfileImage.Source = App.BaseImageUrl + "ContactProfileImage.png";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            await model.Init();
        }
    }
}