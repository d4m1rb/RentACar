using RentACarApp.MobileUI.Models;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Templates.Vozila
{
    /// <summary>
    /// Product list template.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaVozilaTemplate : Grid
    {
        /// <summary>
        /// Bindable property to set the parent bindingcontext.
        /// </summary>
        public static BindableProperty ParentBindingContextProperty =
         BindableProperty.Create(nameof(ParentBindingContext), typeof(object),
         typeof(ListaVozilaTemplate), null);

        /// <summary>
        /// Gets or sets the parent bindingcontext.
        /// </summary>
        public object ParentBindingContext
        {
            get { return GetValue(ParentBindingContextProperty); }
            set { SetValue(ParentBindingContextProperty, value); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ListaVozilaTemplate"/> class.
        /// </summary>
		public ListaVozilaTemplate()
        {
            InitializeComponent();
        }
       
        private async void DetaljiButton_Clicked(object sender, EventArgs e)
        {

            //var selectedCar = _viewModel.Items.First(item =>
            //item.Id == int.Parse((sender as Button).CommandParameter.ToString()));
            try 
            {
                SfButton x = (SfButton)sender;
                
                var AutomobilId = int.Parse(x.CommandParameter.ToString());

               // HomePage.HomeStranicaInstanca.Detail = new NavigationPage(new RentACarApp.MobileUI.Views.Detail.DetaljiVozilaPage(AutomobilId));
                await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Vozila.DetaljiVozilaPage(AutomobilId));
            }
            catch(Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Greška", ex.Message, "OK");
            }
           
        }
    }
}