using RentACarApp.Model.Models;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.ViewModels.Vozila;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Vozila
{
    /// <summary>
    /// The Detail page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetaljiVozilaPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DetaljiVozilaPage" /> class.
        /// </summary>
        private readonly APIService _vozilaService = new APIService("Automobil");
        int AutomobilID;
        DetaljiVozilaViewModel model;
        public DetaljiVozilaPage(int AutomobilId)
        {
            InitializeComponent();
            AutomobilID = AutomobilId;
            

            this.BindingContext = model=new DetaljiVozilaViewModel() {
                AutomobilId = AutomobilID,                
            };
        }

        

        /// <summary>
        /// Invoked when view size is changed.
        /// </summary>
        /// <param name="width">The Width</param>
        /// <param name="height">The Height</param>
        //protected override void OnSizeAllocated(double width, double height)
        //{
        //    base.OnSizeAllocated(width, height);

        //    if (width > height)
        //    {
        //        //Rotator.ItemTemplate = (DataTemplate)this.Resources["LandscapeTemplate"];
        //    }
        //    else
        //    {
        //        //Rotator.ItemTemplate = (DataTemplate)this.Resources["PortraitTemplate"];
        //    }
        //}

        protected override async void OnAppearing()
        {
            await model.InitMethod();
            base.OnAppearing();
            
            
        }
        

    }
}