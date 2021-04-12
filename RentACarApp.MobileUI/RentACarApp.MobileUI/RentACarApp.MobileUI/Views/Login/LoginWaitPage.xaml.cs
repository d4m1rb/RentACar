using RentACarApp.MobileUI.ViewModels.Login;
using Syncfusion.XForms.Buttons;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Login
{
    /// <summary>
    /// Page to login with user name and password
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginWaitPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginWaitPage" /> class.
        /// </summary>

        public LoginWaitPage()
        {
            InitializeComponent();           
        }

        
        protected override void OnAppearing()
        {
            base.OnAppearing();            
        }
    }
}