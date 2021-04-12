using RentACarApp.MobileUI.ViewModels.Postavke;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI.Views.Postavke
{
    /// <summary>
    /// Page to show the setting.
    /// </summary>
    [Preserve(AllMembers = true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PostavkePage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostavkePage" /> class.
        /// </summary>
        /// 
        private PostavkeViewModel model = null;
        public PostavkePage(int KlijentId)
        {
            InitializeComponent();
            BindingContext = model = new PostavkeViewModel() { KlijentId=KlijentId};
        }
    }
}