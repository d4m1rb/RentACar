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
using RentACarApp.MobileUI.Controls;

namespace RentACarApp.MobileUI.Views.Profil
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IzmjenaProfilaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IzmjenaProfilaPage" /> class.
        /// </summary>

        private APIService _klijentService = new APIService("Klijent");
        private APIService _gradService = new APIService("Grad");

        private IzmjenaProfilaViewModel model = null;
        public int KlijentID;
        
        
        public IzmjenaProfilaPage(IzmjenaProfilaViewModel mod)
        {
            InitializeComponent();
            // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = mod;
            KlijentID = mod.KlijentId;           
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
           // await model.Init();
        }

        private async void btnSnimi_Clicked(object sender, EventArgs e)
        {
            KlijentUpsertRequest klijentUpdateRequest = new KlijentUpsertRequest();

            klijentUpdateRequest.KlijentId = model.KlijentId;
            klijentUpdateRequest.Ime = model.Ime;
            klijentUpdateRequest.Prezime = model.Prezime;
            klijentUpdateRequest.UserName = model.UserName;
            klijentUpdateRequest.Email = model.Email;
            klijentUpdateRequest.Telefon = model.Telefon;
            klijentUpdateRequest.DatumRodjenja = model.DatumRodjenja;
            klijentUpdateRequest.DatumRegistracije = model.DatumRegistracije;
            klijentUpdateRequest.GradId = model.SelectedGrad.GradId;
            klijentUpdateRequest.Adresa = model.Adresa;
            klijentUpdateRequest.Slika = model.Slika;
            klijentUpdateRequest.SlikaThumb = model.Slika;
            klijentUpdateRequest.Status = true;



            try
            {
                var x= await _klijentService.Update<Klijent>(klijentUpdateRequest.KlijentId, klijentUpdateRequest);
                if(x==null)
                {
                    return;
                }

                await HomePage.HomeStranicaInstanca.DisplayAlert("Uspješna izmjena!","Uspješno ste izmijenili podatke o profilu.", "OK");
                //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.Settings.PostavkePage(KlijentID);
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();

            }
            catch (Exception ex)
            {
                await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");
                return;
            }
        }
                
    }
}