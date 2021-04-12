using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.DataService;
using RentACarApp.MobileUI.Models;
using RentACarApp.MobileUI.ViewModels.Rezervacije;
using RentACarApp.MobileUI.ViewModels.Profil;
using RentACarApp.MobileUI.ViewModels.ProfileEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using Syncfusion.XForms.Buttons;
using System.Security.Cryptography;
using System.Text;

namespace RentACarApp.MobileUI.Views.Profil
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IzmjenaPasswordaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IzmjenaProfilaPage" /> class.
        /// </summary>

        private APIService _klijentService = new APIService("Klijent");
        private APIService _gradService = new APIService("Grad");

        private IzmjenaPasswordaViewModel model = null;
        public int KlijentID;
        
        
        public IzmjenaPasswordaPage(int KlijentId)
        {
            InitializeComponent();
            
            // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model= new IzmjenaPasswordaViewModel() { KlijentId=KlijentId};
            KlijentID = KlijentId;           
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           // await model.Init();
        }

       

        private async void oldPassword_Unfocused(object sender, FocusEventArgs e)
        {
            if (model.OldPassword != null)
            {
                await model.OldPasswordCheck();
            }
        }

        private async void btnSnimi_Clicked(object sender, EventArgs e)
        {
            try
            {
                KlijentUpsertRequest klijentUpdateRequest = new KlijentUpsertRequest();

                var klijent = await _klijentService.GetById<Klijent>(model.KlijentId);

                klijentUpdateRequest.KlijentId = model.KlijentId;
                klijentUpdateRequest.Ime = klijent.Ime;
                klijentUpdateRequest.Prezime = klijent.Prezime;
                klijentUpdateRequest.UserName = klijent.UserName;
                klijentUpdateRequest.Email = klijent.Email;
                klijentUpdateRequest.Telefon = klijent.Telefon;
                klijentUpdateRequest.DatumRodjenja = klijent.DatumRodjenja;
                klijentUpdateRequest.DatumRegistracije = klijent.DatumRegistracije;
                klijentUpdateRequest.GradId = klijent.GradId;
                klijentUpdateRequest.Adresa = klijent.Adresa;
                klijentUpdateRequest.Slika = klijent.Slika;
                klijentUpdateRequest.SlikaThumb = klijent.SlikaThumb;
                klijentUpdateRequest.Status = true;

                if (!string.IsNullOrEmpty(model.Password) && !string.IsNullOrEmpty(model.PasswordConfirm))
                {
                    klijentUpdateRequest.Password = model.Password;
                    klijentUpdateRequest.PasswordPotvrda = model.PasswordConfirm;
                }
                else
                {
                    throw new Exception("Morate unijeti password i potvrdu passworda");
                }

                if (string.IsNullOrEmpty(model.OldPassword))
                {
                    throw new Exception("Morate unijeti stari password");
                }

                if (model.Password == model.PasswordConfirm && model.Password == model.OldPassword)
                {
                    throw new Exception("Unesite novi password");
                }

                if (model.Password != model.PasswordConfirm)
                {
                    throw new Exception("Password i Password potvrda se moraju podudarati!");
                }

                //provjera stare lozinke
                string Hash = GenerateHash(klijent.LozinkaSalt, model.OldPassword);

                if (Hash == klijent.LozinkaHash)
                {
                    try
                    {
                        await _klijentService.Update<Klijent>(klijentUpdateRequest.KlijentId, klijentUpdateRequest);
                        await HomePage.HomeStranicaInstanca.DisplayAlert("Obavijest", "Uspješno ste izmjenili lozinku", "OK");
                        //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.Settings.PostavkePage(KlijentId);
                        await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();

                    }
                    catch (Exception ex)
                    {
                        await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");

                    }
                }
            }
            catch (Exception ex)
            {

                await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");
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
    }
}