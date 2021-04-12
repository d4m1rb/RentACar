using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace RentACarApp.MobileUI
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMasterMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMasterMaster()
        {
            InitializeComponent();

            BindingContext = new HomePageMasterMasterViewModel();
            ListView = MenuItemsListView;
        }

        class HomePageMasterMasterViewModel : INotifyPropertyChanged
        {
            public ObservableCollection<HomePageMenuItemMenuItem> MenuItems { get; set; }

            public HomePageMasterMasterViewModel()
            {
                MenuItems = new ObservableCollection<HomePageMenuItemMenuItem>(new[]
                {
                    new HomePageMenuItemMenuItem { Id = 0, Title = "Početna", Icon="HomeIcon.png" },
                    new HomePageMenuItemMenuItem { Id = 1, Title = "Vozila", Icon="CarIcon.png" },
                    new HomePageMenuItemMenuItem { Id = 2, Title = "Iznajmi vozilo", Icon="RentIcon.png" },
                    new HomePageMenuItemMenuItem { Id = 3, Title = "Moje rezervacije", Icon="ReservationsIcon.png" },
                    new HomePageMenuItemMenuItem { Id = 4, Title = "Postavke", Icon="SettingsIcon.png" },
                    new HomePageMenuItemMenuItem { Id = 5, Title = "Odjava", Icon="LogoutIcon.png" },
                });
            }

            #region INotifyPropertyChanged Implementation
            public event PropertyChangedEventHandler PropertyChanged;
            void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            #endregion
        }
    }
}