using RentACarApp.MobileUI.Views.Postavke;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.Postavke
{
    /// <summary>
    /// ViewModel for Setting page 
    /// </summary> 
    [Preserve(AllMembers = true)]
    public class PostavkeViewModel : BaseViewModel
    {

        private int klijentId;

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

        #region Constructor

        /// <summary>
        /// Initializes a new instance of the <see cref="PostavkeViewModel" /> class
        /// </summary>
        public PostavkeViewModel()
        {
            this.BackButtonCommand = new Command(this.BackButtonClicked);
            this.EditProfileCommand = new Command(this.EditProfileClicked);
            this.PasswordEditCommand = new Command(this.PasswordEditClicked);
            this.LinkAccountCommand = new Command(this.LinkAccountClicked);
            this.HelpCommand = new Command(this.HelpClicked);
            this.TermsCommand = new Command(this.TermsServiceClicked);
            this.PolicyCommand = new Command(this.PrivacyPolicyClicked);
            this.FAQCommand = new Command(this.FAQClicked);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Gets or sets the command is executed when the favourite button is clicked.
        /// </summary>
        public Command BackButtonCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the edit profile option is clicked.
        /// </summary>
        public Command EditProfileCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the change password option is clicked.
        /// </summary>
        public Command PasswordEditCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the account link option is clicked.
        /// </summary>
        public Command LinkAccountCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the help option is clicked.
        /// </summary>
        public Command HelpCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the terms of service option is clicked.
        /// </summary>
        public Command TermsCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the privacy policy option is clicked.
        /// </summary>
        public Command PolicyCommand { get; set; }

        /// <summary>
        /// Gets or sets the command is executed when the FAQ option is clicked.
        /// </summary>
        public Command FAQCommand { get; set; }

        #endregion

        #region Methods

        /// <summary>
        /// Invoked when the back button clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void BackButtonClicked(object obj)
        {
            HomePage.HomeStranicaInstanca.Detail.Navigation.PopAsync();
        }

        /// <summary>
        /// Invoked when the edit profile option clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void EditProfileClicked(object obj)
        {
            
            //HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.ProfileEdit.ProfilPage(KlijentId);
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Profil.ProfilPage(KlijentId));
        }

        /// <summary>
        /// Invoked when the change password clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private async void PasswordEditClicked(object obj)
        {
            
           // HomePage.HomeStranicaInstanca.Detail = new RentACarApp.MobileUI.Views.ProfileEdit.IzmjenaPasswordaPage(KlijentId);
            await HomePage.HomeStranicaInstanca.Detail.Navigation.PushAsync(new RentACarApp.MobileUI.Views.Profil.IzmjenaPasswordaPage(KlijentId));
            // Do something
        }

        /// <summary>
        /// Invoked when the account link clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void LinkAccountClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the terms of service clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void TermsServiceClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the privacy and policy clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void PrivacyPolicyClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the FAQ clicked
        /// </summary>
        /// <param name="obj">The object</param>
        /// 

        private void FAQClicked(object obj)
        {
            // Do something
        }

        /// <summary>
        /// Invoked when the help option is clicked
        /// </summary>
        /// <param name="obj">The object</param>
        private void HelpClicked(object obj)
        {
            // Do something
        }

        #endregion
    }
}
