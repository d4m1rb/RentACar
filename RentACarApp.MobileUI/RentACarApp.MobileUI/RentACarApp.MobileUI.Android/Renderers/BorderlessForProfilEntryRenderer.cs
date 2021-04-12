using Android.Views;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Application = Android.App.Application;

[assembly: ExportRenderer(typeof(RentACarApp.MobileUI.Controls.BorderlessForProfilEntry), typeof(RentACarApp.MobileUI.Droid.BorderlessForProfilEntryRenderer))]

namespace RentACarApp.MobileUI.Droid
{
    public class BorderlessForProfilEntryRenderer : EntryRenderer
    {
        public BorderlessForProfilEntryRenderer() : base(Application.Context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null)
            {
                this.Control.SetBackground(null);
                Control.Gravity = GravityFlags.Center;
                Control.SetPadding(0, 0, 0, 0);
            }
        }
    }
}