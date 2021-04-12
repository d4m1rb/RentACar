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

namespace RentACarApp.MobileUI.Views.Rezervacije
{
    /// <summary>
    /// Page to show the catalog list. 
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OcijeniRezervacijuPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OcijeniRezervacijuPage" /> class.
        /// </summary>

        private APIService _ocjenaService = new APIService("Ocjena");
        private APIService _gradService = new APIService("Grad");

        private OcijeniRezervacijuViewModel model = null;
        public int KlijentID;
        
        
        public OcijeniRezervacijuPage(OcijeniRezervacijuViewModel mod)
        {
            InitializeComponent();
            // this.BindingContext = CatalogDataService.Instance.ListaVozilaViewModel;
            BindingContext = model = mod;
            //KlijentID = mod.KlijentId;           
            
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            // await model.Init();
        }

        private async void btnSacuvaj_Clicked(object sender, EventArgs e)
        {
            if(model.SelectedOcjena!=0)
            {
                OcjenaUpsertRequest ocjenaRequest = new OcjenaUpsertRequest();
                ocjenaRequest.DatumEvidentiranja = DateTime.Today;
                ocjenaRequest.RezervacijaRentanjaId = model.RezervacijaRentanjaId;
                ocjenaRequest.Napomena = model.Napomena;
                ocjenaRequest.Ocjena1 = model.SelectedOcjena;

                try
                {
                    var x = await _ocjenaService.Insert<Ocjena>(ocjenaRequest);

                    if (x == null)
                    {
                        return;
                    }

                    await HomePage.HomeStranicaInstanca.DisplayAlert("Uspješna izmjena!", "Uspješno ste dodali ocjenu za rezervaciju.", "OK");
                    //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.Settings.PostavkePage(KlijentID);
                    await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();

                }
                catch (Exception ex)
                {
                    await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");
                    return;
                }
            }
            else
            {
                await HomePage.HomeStranicaInstanca.DisplayAlert("Greška","Ocjena mora biti odabrana!", "OK");
                return;
            }
           
        }
                
    }
}