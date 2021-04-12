using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using ModelP = RentACarApp.MobileUI.Models.Profile;
using Syncfusion.XForms.Border;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace RentACarApp.MobileUI.ViewModels.ProfileEdit
{
    /// <summary>
    /// ViewModel for Individual profile page
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ProfilViewModel : BaseViewModel
    {
        #region Field

        private ObservableCollection<ModelP> profileInfo;

        #endregion

        #region Constructor

        /// <summary>
        /// Initializes a new instance for the <see cref="ProfilViewModel" /> class.
        /// </summary>
        public ProfilViewModel()
        {
            this.ProfileInfo = new ObservableCollection<ModelP>();

            for (var i = 0; i < 6; i++)
            {
                this.ProfileInfo.Add(new ModelP { ImagePath = App.BaseImageUrl + "ProfileImage1" + i + ".png" });
            }

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
            Application.Current.Resources.TryGetValue("Gray-100", out var retVal);
            (obj as SfBorder).BackgroundColor = (Color)retVal;
            await Task.Delay(100);

            Application.Current.Resources.TryGetValue("Gray-White", out var oldVal);
            (obj as SfBorder).BackgroundColor = (Color)oldVal;
        }

        /// <summary>
        /// Invoked when the edit button is clicked.
        /// </summary>
        /// <param name="obj">The object</param>
        private void EditButtonClicked(object obj)
        {
            // Do something
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

        #endregion
    }
}