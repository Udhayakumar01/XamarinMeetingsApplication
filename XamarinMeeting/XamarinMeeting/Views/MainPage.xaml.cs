using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;
using XamarinMeeting.SkiaAnimation;
using XamarinMeeting.Views;
using SkiaSharp.Views.Forms;

namespace XamarinMeeting
{
    public partial class MainPage : ContentPage
    {
        IEventTrackerDroid eventTracker;

        readonly HighlightForm _highlightForm;

        readonly FirebaseHelper firebasehelper = new FirebaseHelper();
        public MainPage()
        {
            
                InitializeComponent();
            //eventTracker=DependencyService.Get<IEventTrackerDroid>();
            //MyEntry.Completed += (s, e) => MyEntry.Focus();
            //Email.Completed += (object sender, EventArgs e) => { Password.Focus(); };
            //Password.Completed += (object sender, EventArgs e) => { btn_Login.Focus(); };
            var settings = new HighlightSettings()
            {
                StrokeWidth = 6,
                StrokeStartColor = Color.FromHex("#FF4600"),
                StrokeEndColor = Color.FromHex("#CC00AF"),
                AnimationDuration = TimeSpan.FromMilliseconds(900),
                AnimationEasing = Easing.CubicInOut,
            };

            _highlightForm = new HighlightForm(settings);

        }


        public Users details;
        //#BF043055
       private async void BtnLogin_Clicked(object sender, EventArgs e)
        {
            //_highlightForm.HighlightElement((View)sender, _skCanvasView, _formLayout);

            var current = Connectivity.NetworkAccess;
            var profiles = Connectivity.ConnectionProfiles;
            if (!IsFormValid())
            {
                DisplayAlert("Error", "Email and Password are requird fields", "OK");
                return;
            }
            if ((current == NetworkAccess.Internet)||(profiles.Contains(ConnectionProfile.WiFi)))
            {
                // Connection to internet is available
                
               var userDetails = await firebasehelper.GetUser(Email.Text, Password.Text);
                details = userDetails;
                if (userDetails != null)
                {

                    DisplayAlert("Success", "Login Details are valid", "OK");

                    Email.Text = string.Empty;
                    Password.Text = string.Empty;
                    await Navigation.PushModalAsync(new NavigationPage(new AddMeetings(userDetails)));

                }
                else
                {
                    DisplayAlert("Failed", "Login Details are Invalid", "OK");
                }


                Analytics.TrackEvent("Login btn clicked", new Dictionary<string, string>
            {
            { "Email id", Email.Text},
            { "Password", Password.Text} });

            }
            else
            {
                DisplayAlert("No Internet","Please connect to an internet connection","OK");
            }
        }

        


        //For validating the controls
        public bool IsFormValid() => IsEmailValid() && IsPasswordValid();
        public bool IsEmailValid() => !string.IsNullOrWhiteSpace(Email.Text);
        public bool IsPasswordValid() => !string.IsNullOrWhiteSpace(Password.Text);




        public Users ToPassData()
        {
            Users Details = details;
            return Details;

        }

        void EntryFocused(object sender, FocusEventArgs e)
        {
            _highlightForm.HighlightElement((View)sender, _skCanvasView, _formLayout);
        }

        void SkCanvasViewPaintSurfaceRequested(object sender, SKPaintSurfaceEventArgs e)
        {
            _highlightForm.Draw(_skCanvasView, e.Surface.Canvas);
        }

        void SkCanvasViewSizeChanged(object sender, EventArgs e)
        {
            _highlightForm.Invalidate(_skCanvasView, _formLayout);
        }

    }
}
