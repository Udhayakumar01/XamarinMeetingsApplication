using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.FirebasePushNotification;

namespace XamarinMeeting
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
          //  MainPage = new AppShell();
          MainPage = new MainPage();
          //CrossFirebasePushNotification.Current.OnTokenRefresh += Current_OnTokenRefresh;
        }

       /*private void Current_OnTokenRefresh(object source, FirebasePushNotificationTokenEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"TOKEN : {e.Token}");

        }*/

        protected override void OnStart()
        {
            AppCenter.Start("android=a97e9d6c-b36e-4989-aff3-b6b1acd9323f;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }


        public static async Task PushModalAsync(Page page)
        {
            await Current.MainPage.Navigation.PushModalAsync(new NavigationPage(page)
            {
                BarBackgroundColor = Color.FromHex("2196F3"),
                BarTextColor = Color.White
            });
        }

        public static async Task DisplayAlert(string mesage)
        {
            await Current.MainPage.DisplayAlert(string.Empty, mesage, "OK");
        }

        public static async Task PopModalAsync(Page page)
        {
            await Current.MainPage.Navigation.PopModalAsync();
           
        }


    }
}
