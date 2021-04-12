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
    public partial class LoginPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage" /> class.
        /// </summary>
        public LoginPageViewModel model;
        public LoginPage()
        {
            InitializeComponent();
            this.BindingContext = model = new LoginPageViewModel();
        }

        
        protected override void OnAppearing()
        {
            var properties = App.Current.Properties;
            if (properties.ContainsKey("username") && properties.ContainsKey("password"))
            {                
                model.InitMethod();                
            }

            if (properties.ContainsKey("username"))
            {
                model.Username = properties["username"] as string;
            }

            base.OnAppearing();            
        }

        private async void btnLogin_Clicked(object sender, System.EventArgs e)
        {
            SfButton loginButton = (SfButton)FindByName("btnLogin");
            loginButton.IsEnabled = false;
            await model.LoginClicked();
            loginButton.IsEnabled = true;
        }
    }
}