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
using System.Text;
using System.Security.Cryptography;

namespace RentACarApp.MobileUI.ViewModels.Profil
{
    /// <summary>
    /// ViewModel for catalog page.
    /// </summary>
    [Preserve(AllMembers = true)]
    [DataContract]
    public class IzmjenaPasswordaViewModel : BaseViewModel
    {
        private readonly APIService _klijentService = new APIService("Klijent");

        public ICommand SaveCommand { get; set; }
        public ICommand OldPasswordCheckCommand { get; set; }
        public Command BackButtonCommand { get; set; }

        public async Task Save()
        {
            #region komentirano
            //try
            //{
            //    KlijentUpsertRequest klijentUpdateRequest = new KlijentUpsertRequest();

            //    var klijent = await _klijentService.GetById<Klijent>(KlijentId);

            //    klijentUpdateRequest.KlijentId = KlijentId;
            //    klijentUpdateRequest.Ime = klijent.Ime;
            //    klijentUpdateRequest.Prezime = klijent.Prezime;
            //    klijentUpdateRequest.UserName = klijent.UserName;
            //    klijentUpdateRequest.Email = klijent.Email;
            //    klijentUpdateRequest.Telefon = klijent.Telefon;
            //    klijentUpdateRequest.DatumRodjenja = klijent.DatumRodjenja;
            //    klijentUpdateRequest.DatumRegistracije = klijent.DatumRegistracije;
            //    klijentUpdateRequest.GradId = klijent.GradId;
            //    klijentUpdateRequest.Adresa = klijent.Adresa;
            //    klijentUpdateRequest.Slika = klijent.Slika;
            //    klijentUpdateRequest.SlikaThumb = klijent.SlikaThumb;
            //    klijentUpdateRequest.Status = true;

            //    if (!string.IsNullOrEmpty(Password) && !string.IsNullOrEmpty(PasswordConfirm))
            //    {
            //        klijentUpdateRequest.Password = Password;
            //        klijentUpdateRequest.PasswordPotvrda = PasswordConfirm;
            //    }
            //    else
            //    {
            //        throw new Exception("Morate unijeti password i potvrdu passworda");
            //    }

            //    if(string.IsNullOrEmpty(OldPassword))
            //    {
            //        throw new Exception("Morate unijeti stari password");
            //    }

            //    if(Password==PasswordConfirm && Password == OldPassword)
            //    {
            //        throw new Exception("Unesite novi password");
            //    }

            //    if (Password != PasswordConfirm)
            //    {
            //        throw new Exception("Password i Password potvrda se moraju podudarati!");
            //    }

            //    //provjera stare lozinke
            //    string Hash = GenerateHash(klijent.LozinkaSalt, OldPassword);

            //    if (Hash == klijent.LozinkaHash)
            //    {
            //        try
            //        {
            //            await _klijentService.Update<Klijent>(klijentUpdateRequest.KlijentId, klijentUpdateRequest);
            //            await HomePage.HomeStranicaInstanca.DisplayAlert("Obavijest", "Uspješno ste izmjenili lozinku", "OK");
            //            //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.Settings.PostavkePage(KlijentId);
            //            await HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();

            //        }
            //        catch (Exception ex)
            //        {
            //            await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{

            //    await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");
            //}
            #endregion
        }
        public async Task OldPasswordCheck()
        {
            var klijent = await _klijentService.GetById<Klijent>(KlijentId);
           
                string Hash = GenerateHash(klijent.LozinkaSalt, OldPassword);

                if (Hash != klijent.LozinkaHash)
                {
                    await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", "Stara lozinka nije ispravna", "OK");
                }
            
           
        }

        #region Fields

        private int klijentId;
        private string password;
        private string passwordConfirm;
        private string oldPassword;
       
        

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="IzmjenaProfilaViewModel" /> class.
        /// </summary>
        public IzmjenaPasswordaViewModel()
        {            
            SaveCommand = new Command(async () => await Save());
            OldPasswordCheckCommand = new Command(async () => await OldPasswordCheck());
            this.BackButtonCommand = new Command(BackButtonClicked);        

        }

        #endregion

        #region Public properties      

        public int KlijentId
        {
            get
            {
                return this.klijentId;
            }

            set
            {
                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return this.password;
            }

            set
            {
                this.password = value;
                this.NotifyPropertyChanged();
            }
        }

        public string PasswordConfirm
        {
            get
            {
                return this.passwordConfirm;
            }

            set
            {
                this.passwordConfirm = value;
                this.NotifyPropertyChanged();
            }
        }

        public string OldPassword
        {
            get
            {
                return this.oldPassword;
            }

            set
            {
                this.oldPassword = value;
                this.NotifyPropertyChanged();
            }
        }


        #endregion

        #region Command


        #endregion

        #region Methods

        /// <summary>
        /// Invoked when an item is selected.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void ItemSelected(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the items are sorted.
        /// </summary>
        /// <param name="attachedObject">The Object</param>
        private void SortClicked(object attachedObject)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the filter button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void FilterClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the favourite button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddFavouriteClicked(object obj)
        {
            if (obj is Product product)
                product.IsFavourite = !product.IsFavourite;
        }

        /// <summary>
        /// Invoked when the cart button is clicked.
        /// </summary>
        /// <param name="obj">The Object</param>
        private void AddToCartClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when cart icon button is clicked.
        /// </summary>
        /// <param name="obj"></param>
        private void CartClicked(object obj)
        {
            // Do something
        }

        private void BackButtonClicked(object obj)
        {
            // Do something
            HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
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
        #endregion
    }
}