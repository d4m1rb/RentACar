using RentACarApp.MobileUI.Views.Login;
using RentACarApp.Model.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.Login
{
    /// <summary>
    /// ViewModel for login page.
    /// </summary>
    [Preserve(AllMembers = true)]
    public class LoginPageViewModel : BaseViewModel
    {
        private readonly APIService _serviceKorisnik = new APIService("Korisnik");
        private readonly APIService _serviceKlijent = new APIService("Klijent");
        public int KlijentID;
        public bool RedirectToLogin { get; set; } = false;

        string _username = string.Empty;
        public string Username
        {
            get { return _username; }
            set
            {
                if (this._username == value)
                {
                    return;
                }

                this._username = value;
                this.NotifyPropertyChanged();
            }
        }

        #region Fields

        private string password;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="LoginPageViewModel" /> class.
        /// </summary>
        public LoginPageViewModel()
        {
            this.LoginCommand = new Command(async ()=> await this.LoginClicked());
            this.SignUpCommand = new Command(this.SignUpClicked);
            this.ForgotPasswordCommand = new Command(this.ForgotPasswordClicked);
            this.SocialMediaLoginCommand = new Command(this.SocialLoggedIn);
        }

        #endregion

        #region property

        /// <summary>
        /// Gets or sets the property that is bound with an entry that gets the password from user in the login page.
        /// </summary>
        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                if (this.password == value)
                {
                    return;
                }

                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the Log In button is clicked.
        /// </summary>

        public ICommand LoginCommand { get; set; }


        /// <summary>
        /// Gets or sets the command that is executed when the Sign Up button is clicked.
        /// </summary>
        public Command SignUpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the Forgot Password button is clicked.
        /// </summary>
        public Command ForgotPasswordCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the social media login button is clicked.
        /// </summary>
        public Command SocialMediaLoginCommand { get; set; }

        #endregion

        #region methods

        /// <summary>
        /// Invoked when the Log In button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        public async Task LoginClicked()
        {
            try
            {               
                if (Username==null || Password==null)
                {
                    throw new Exception("Obavezno je unijeti Username i Password!");
                }

                APIService.Username = Username;
                APIService.Password = Password;

                await _serviceKlijent.Get<List<dynamic>>(null);
                KlijentSearchRequest klijentSearch = new KlijentSearchRequest();
                klijentSearch.UserName = APIService.Username;
                klijentSearch.Status = true;

                var korisnici = await _serviceKlijent.Get<List<RentACarApp.Model.Models.Klijent>>(klijentSearch);

                if (korisnici.Count > 0)
                {
                    var k = korisnici.FirstOrDefault();
                    string pwHash = GenerateHash(k.LozinkaSalt, APIService.Password);

                    if (k.UserName == klijentSearch.UserName && k.LozinkaHash == pwHash)
                    {
                        KlijentID = k.KlijentId;
                        Application.Current.MainPage = new HomePage(KlijentID);
                        var properties = App.Current.Properties;
                        
                        if (!properties.ContainsKey("username"))
                        {
                            properties.Add("username", Username);
                        }
                        else
                        {
                            properties["username"] = Username;
                        }

                        if (!properties.ContainsKey("password"))
                        {
                            properties.Add("password", Password);
                        }
                        else
                        {
                            properties["password"] = Password;
                        }                      
                    }
                    else
                        throw new Exception("Unos nije ispravan");
                }
                else
                    throw new Exception("User ne postoji");

            }
            catch (Exception ex)
            {
                string msg = "";
                if (ex.InnerException != null)
                    msg = ex.InnerException.ToString() + " - ";
                await Application.Current.MainPage.DisplayAlert("Greška", msg + ex.Message, "OK");
            }
        }

        public async Task InitMethod()
        {
            var properties = App.Current.Properties;

            if (properties.ContainsKey("username") && properties.ContainsKey("password"))
            {
                App.Current.MainPage = new RentACarApp.MobileUI.Views.Login.LoginWaitPage();

                Username = properties["username"] as string;
                Password = properties["password"] as string;

                APIService.Username = Username;
                APIService.Password = Password;

                await _serviceKlijent.Get<List<dynamic>>(null);
                KlijentSearchRequest klijentSearch = new KlijentSearchRequest();
                klijentSearch.UserName = APIService.Username;
                klijentSearch.Status = true;

                var korisnici = await _serviceKlijent.Get<List<RentACarApp.Model.Models.Klijent>>(klijentSearch);

                if (korisnici.Count > 0)
                {
                    var k = korisnici.FirstOrDefault();
                    string pwHash = GenerateHash(k.LozinkaSalt, APIService.Password);

                    if (k.UserName == klijentSearch.UserName && k.LozinkaHash == pwHash)
                    {
                        KlijentID = k.KlijentId;
                        Application.Current.MainPage = new HomePage(KlijentID);
                    }
                    else
                    {                                            
                        properties.Remove("password");
                        Application.Current.MainPage = new LoginPage();                        
                    }
                        
                }
                else
                {                   
                    properties.Remove("password");
                    Application.Current.MainPage = new LoginPage();
                }
            }
            else
            {                
                properties.Remove("password");
                Application.Current.MainPage = new LoginPage();
            }

        }

        public static string GenerateHash(string salt, string password)
        {
            byte[] src = Convert.FromBase64String(salt);
            byte[] bytes = Encoding.Unicode.GetBytes(password);
            byte[] dst = new byte[src.Length + bytes.Length];

            System.Buffer.BlockCopy(src, 0, dst, 0, src.Length);
            System.Buffer.BlockCopy(bytes, 0, dst, src.Length, bytes.Length);

            HashAlgorithm algorithm = HashAlgorithm.Create("SHA1");
            byte[] inArray = algorithm.ComputeHash(dst);
            return Convert.ToBase64String(inArray);
        }
        /// <summary>
        /// Invoked when the Sign Up button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SignUpClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the Forgot Password button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private async void ForgotPasswordClicked(object obj)
        {
            var label = obj as Label;
            label.BackgroundColor = Color.FromHex("#70FFFFFF");
            await Task.Delay(100);
            label.BackgroundColor = Color.Transparent;
        }

        /// <summary>
        /// Invoked when social media login button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void SocialLoggedIn(object obj)
        {
            // Do something
        }

        #endregion
    }
}