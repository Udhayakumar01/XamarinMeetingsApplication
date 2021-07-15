using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;
using XamarinMeeting.ViewModel;

namespace XamarinMeeting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
 public partial class AddMeetings : ContentPage
    {
     
        
        public  AddMeetings(Users userDetails)
        {

           
            InitializeComponent();
            
            lblRole.Text = $"Welcome,{userDetails.Role} of {userDetails.Technology} Technology";
            
       

        }

      public static implicit operator AddMeetings(AddMeeting v)
        {
            throw new NotImplementedException();
        }

        private void ToolbarLogoutItem_Clicked(object sender, EventArgs e)
        {
            this.Navigation.PopModalAsync();
        }

        private void Btn_DayWeekViewClicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new DayWeekView());
        }
    }
}












