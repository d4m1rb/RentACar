using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using Syncfusion.ListView.XForms;
using RentACarApp.MobileUI.Models;
using RentACarApp.Model.Requests;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using RentACarApp.Model.Models;
using System.Threading.Tasks;
using System.Windows.Input;
using System;
using RentACarApp.MobileUI.Helpers;

namespace RentACarApp.MobileUI.ViewModels.Poruke
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class ListaPorukaViewModel : BaseViewModel
    {

        
        public ICommand InitCommand { get; set; }

        public async Task Init()
        {
           

        }

        #region Fields

        private List<PorukaKontaktPoruka> _listaPoruka;
        private int _rezervacijaId;
        private int klijentId;
       

        private Command addToCartCommand;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ListaPorukaViewModel" /> class.
        /// </summary>
        public ListaPorukaViewModel()
        {
            listaPoruka = new List<PorukaKontaktPoruka>();
            
            InitCommand = new Command(async () => await Init());
            MenuCommand = new Command(MenuClicked);
        }

        #endregion

        #region Public properties

        public List<PorukaKontaktPoruka> listaPoruka
        {
            get
            {
                return this._listaPoruka;
            }

            set
            {
                if (this._listaPoruka == value)
                {
                    return;
                }

                this._listaPoruka = value;
                this.NotifyPropertyChanged();
            }
        }

        public int KlijentId
        {
            get
            {
                return this.klijentId;
            }

            set
            {
                if (this.klijentId == value)
                {
                    return;
                }

                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }

        public int RezervacijaId
        {
            get
            {
                return this._rezervacijaId;
            }

            set
            {
                if (this._rezervacijaId == value)
                {
                    return;
                }

                this._rezervacijaId = value;
                this.NotifyPropertyChanged();
            }
        }



        #endregion

        #region Command


        public Command AddToCartCommand
        {
            get { return this.addToCartCommand ?? (this.addToCartCommand = new Command(this.AddToCartClicked)); }
        }

        public Command MenuCommand { get; set; }

        #endregion

        #region Methods


        private void AddToCartClicked(object obj)
        {
            // Do something
        }

        private void MenuClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.IsPresented = true;
        }


        #endregion
    }
}