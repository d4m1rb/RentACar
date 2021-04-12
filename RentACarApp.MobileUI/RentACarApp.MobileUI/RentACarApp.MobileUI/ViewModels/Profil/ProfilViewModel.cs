using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ModelP = RentACarApp.MobileUI.Models.Profile;
using Syncfusion.XForms.Border;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Windows.Input;
using System;
using System.Collections.Generic;
using RentACarApp.Model.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.IO;
using RentACarApp.Model.Requests;
using RentACarApp.MobileUI.ViewModels.ProfileEdit;

namespace RentACarApp.MobileUI.ViewModels.Profil
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfilViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<ModelP> profileInfo;
        public int klijentId;
        public string ime;
        public string prezime;
        public string userName;
        public string email;
        public DateTime datumRodjenja;
        public DateTime datumRegistracije;
        public string telefon;
        public int gradId;
        public string adresa;
        public string lozinkaHash;
        public string lozinkaSalt;
        public bool status;
        public byte[] slika;
        public byte[] slikaThumb;
        public string slikaPath;
        public string gradString;
        public string imePrezime;
        public string statusString;
        public Grad selectedGrad;
        public ObservableCollection<Grad> gradovi;
        public int selectedIndexGrad;
        public Image slimage;

        public ICommand InitCommand { get; set; }
        public ICommand SaveCommand { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfilViewModel" /> class.
        /// </summary>

        private APIService _klijentService = new APIService("Klijent");
        private APIService _gradService = new APIService("Grad");
        public ProfilViewModel()
        {
            InitCommand = new Command(async () => await Init());
            SaveCommand = new Command(async () => await Save());

            

            this.ProfileNameCommand = new Command(this.ProfileNameClicked);
            this.EditCommand = new Command(this.EditButtonClicked);
            this.ViewAllCommand = new Command(this.ViewAllButtonClicked);
            this.MediaImageCommand = new Command(this.MediaImageClicked);
        }

        #endregion

        #region Public Property

        /// <summary>
        /// Gets or sets a collection of profile info.
        /// </summary>
        public ObservableCollection<ModelP> ProfileInfo
        {
            get
            {
                return this.profileInfo;
            }

            set
            {
                this.profileInfo = value;
                this.NotifyPropertyChanged();
            }
        }

        public Image slImage
        {
            get
            {
                return this.slimage;
            }

            set
            {
                this.slimage = value;
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
                this.klijentId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Ime
        {
            get
            {
                return this.ime;
            }

            set
            {
                this.ime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Prezime
        {
            get
            {
                return this.prezime;
            }

            set
            {
                this.prezime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string UserName
        {
            get
            {
                return this.userName;
            }

            set
            {
                this.userName = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime DatumRodjenja
        {
            get
            {
                return this.datumRodjenja;
            }

            set
            {
                this.datumRodjenja = value;
                this.NotifyPropertyChanged();
            }
        }

        public DateTime DatumRegistracije
        {
            get
            {
                return this.datumRegistracije;
            }

            set
            {
                this.datumRegistracije = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Telefon
        {
            get
            {
                return this.telefon;
            }

            set
            {
                this.telefon = value;
                this.NotifyPropertyChanged();
            }
        }

        public int GradId
        {
            get
            {
                return this.gradId;
            }

            set
            {
                this.gradId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string LozinkaHash
        {
            get
            {
                return this.lozinkaHash;
            }

            set
            {
                this.lozinkaHash = value;
                this.NotifyPropertyChanged();
            }
        }

        public string LozinkaSalt
        {
            get
            {
                return this.lozinkaSalt;
            }

            set
            {
                this.lozinkaSalt = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool Status
        {
            get
            {
                return this.status;
            }

            set
            {
                this.status = value;
                this.NotifyPropertyChanged();
            }
        }

        public byte[] Slika
        {
            get
            {
                return this.slika;
            }

            set
            {
                this.slika = value;
                this.NotifyPropertyChanged();
            }
        }

        public byte[] SlikaThumb
        {
            get
            {
                return this.slikaThumb;
            }

            set
            {
                this.slikaThumb = value;
                this.NotifyPropertyChanged();
            }
        }

        public string SlikaPath
        {
            get
            {
                return this.slikaPath;
            }

            set
            {
                this.slikaPath = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Adresa
        {
            get
            {
                return this.adresa;
            }

            set
            {
                this.adresa = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Email
        {
            get
            {
                return this.email;
            }

            set
            {
                this.email = value;
                this.NotifyPropertyChanged();
            }
        }

        public string ImePrezime
        {
            get
            {
                return this.imePrezime;
            }

            set
            {
                this.imePrezime = value;
                this.NotifyPropertyChanged();
            }
        }

        public string GradString
        {
            get
            {
                return this.gradString;
            }

            set
            {
                this.gradString = value;
                this.NotifyPropertyChanged();
            }
        }

        public Grad SelectedGrad
        {
            get
            {
                return this.selectedGrad;
            }

            set
            {
                this.selectedGrad = value;
                this.NotifyPropertyChanged();
            }
        }

        public string StatusString
        {
            get
            {
                return this.statusString;
            }

            set
            {
                this.statusString = value;
                this.NotifyPropertyChanged();
            }
        }

        public ObservableCollection<Grad> Gradovi
        {
            get
            {
                return this.gradovi;
            }

            set
            {
                this.gradovi = value;
                this.NotifyPropertyChanged();
            }
        }

        public int SelectedIndexGrad
        {
            get
            {
                return this.selectedIndexGrad;
            }

            set
            {
                this.selectedIndexGrad = value;
                this.NotifyPropertyChanged();
            }
        }

        #endregion

        #region Command

        /// <summary>
        /// Gets or sets the command that is executed when the profile name is clicked.
        /// </summary>
        public Command ProfileNameCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the edit button is clicked.
        /// </summary>
        public Command EditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the view all button is clicked.
        /// </summary>
        public Command ViewAllCommand { get; set; }

        /// <summary>
        /// Gets or sets the command that is executed when the media image is clicked.
        /// </summary>
        public Command MediaImageCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the profile name is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void ProfileNameClicked(object obj)
        {
            IzmjenaProfilaViewModel model = new IzmjenaProfilaViewModel();

            model.Ime = Ime;
            model.Prezime = Prezime;
            model.GradId = GradId;
            model.Adresa = Adresa;
            model.DatumRegistracije = DatumRegistracije;
            model.DatumRodjenja = DatumRodjenja;
            model.Email = Email;
            model.Gradovi = Gradovi;
            model.GradString = GradString;
            model.ImePrezime = ImePrezime;
            model.KlijentId = KlijentId;
            model.SelectedGrad = SelectedGrad;
            model.Slika = Slika;
            model.SlikaPath = SlikaPath;
            model.SlikaThumb = SlikaThumb;
            model.Status = Status;
            model.StatusString = StatusString;
            model.Telefon = Telefon;
            model.UserName = UserName;


            //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.ProfileEdit.IzmjenaProfilaPage(model);
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Profil.IzmjenaProfilaPage(model));
            //Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            //(obj as SfBorder).BackgroundColor = (Color)retVal;
            //await Task.Delay(100);

            //Application.Current.Resources.TryGetValue("Gray-White", out var oldVal);
            //(obj as SfBorder).BackgroundColor = (Color)oldVal;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private async void EditButtonClicked(object obj)
        {
            try
            {
                // Do something
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", "Učitavanje slike nije dozvoljeno", "OK");
                    return;
                }
                PickMediaOptions pick = new PickMediaOptions()
                {
                    PhotoSize = PhotoSize.Medium
                };
                var file = await CrossMedia.Current.PickPhotoAsync(pick);
                if (file == null)
                    return;


                Image i = slImage;/*(Image)HomePage.HomeStranicaInstanca.Detail.FindByName("SlikaProfila");*/
                i.Source = ImageSource.FromStream(() => file.GetStream());

                MemoryStream ms = new MemoryStream();
                file.GetStream().CopyTo(ms);
                Slika = ms.ToArray();
                await Save();
            }
            catch (Exception ex)
            {
                await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");
                throw;
            }
            
        }

        public async Task Save()
        {

            KlijentUpsertRequest klijentUpdateRequest = new KlijentUpsertRequest();

                klijentUpdateRequest.KlijentId = KlijentId;
                klijentUpdateRequest.Ime = Ime;
                klijentUpdateRequest.Prezime = Prezime;
                klijentUpdateRequest.UserName = UserName;
                klijentUpdateRequest.Email = Email;
                klijentUpdateRequest.Telefon = Telefon;
                klijentUpdateRequest.DatumRodjenja = DatumRodjenja;
            klijentUpdateRequest.DatumRegistracije = DatumRegistracije;
                klijentUpdateRequest.GradId = GradId;
                klijentUpdateRequest.Adresa = Adresa;
                klijentUpdateRequest.Slika = Slika;
                klijentUpdateRequest.SlikaThumb = Slika;
                klijentUpdateRequest.Status = true;
            
            
                       
                try
                {
                    await _klijentService.Update<Klijent>(klijentUpdateRequest.KlijentId, klijentUpdateRequest);

                }
                catch (Exception ex)
                {
                    await HomePage.HomeStranicaInstanca.DisplayAlert("Greška", ex.Message, "OK");

                }
            
        }

        /// <summary>
        /// Invoked when the view all button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void ViewAllButtonClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the media image is clicked.
        /// </summary>
        private void MediaImageClicked(object obj)
        {
            // Do something
        }

        public async Task Init()
        {
            var gradoviList = await _gradService.Get<ObservableCollection<Grad>>(null);
            Gradovi = new ObservableCollection<Grad>();
            foreach (var grad in gradoviList)
            {
                Gradovi.Add(grad);
            }



            Klijent k = await _klijentService.GetById<Klijent>(KlijentId);

            Adresa = k.Adresa;
            Email = k.Email;
            DatumRegistracije = k.DatumRegistracije;
            DatumRodjenja = k.DatumRodjenja;
            GradId = k.GradId;
            Ime = k.Ime;
            Prezime = k.Prezime;
            KlijentId = k.KlijentId;
            LozinkaHash = k.LozinkaHash;
            LozinkaSalt = k.LozinkaSalt;
            Slika = k.Slika;
            SlikaThumb = k.SlikaThumb;
            Status = k.Status;
            Telefon = k.Telefon;
            UserName = k.UserName;
            ImePrezime = Ime +" "+ Prezime;
            Grad xgrad=await _gradService.GetById<Grad>(GradId);
            GradString = xgrad.Naziv;            
            SelectedGrad = xgrad;
            SelectedIndexGrad = Gradovi.IndexOf(xgrad);
            
            if (status==true)
                StatusString = "Aktivan";
            else
                StatusString = "Neaktivan";

        }

        #endregion
    }
}