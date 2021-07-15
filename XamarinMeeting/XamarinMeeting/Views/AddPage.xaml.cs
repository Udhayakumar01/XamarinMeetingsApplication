using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;

namespace XamarinMeeting.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPage : ContentPage
    {
        public Action<Meeting> Added { get; set; }

        public AddPage(DateTime selectedDate)
        {
            InitializeComponent();

            /*  MainPage mainpage = new MainPage();
              Users userName=mainpage.ToPassData();
              Organiser_Name.Text = userName.Name.ToString();*/
            datePicker.MinimumDate = new DateTime(1970, 1, 1);
            datePicker.MaximumDate = new DateTime(2030, 1, 1);

            datePicker.Date = selectedDate.Date;
            starttimePicker.Time = DateTime.Now.TimeOfDay;

   
        }

      async void AddMeeting_Clicked(object sender,EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Interviewer_Name.Text))
            {
                await App.DisplayAlert("Please enter Interviewer Name");
                return;
            }
            if (string.IsNullOrWhiteSpace(Interviewer_Email.Text))
            {
                await App.DisplayAlert("Please enter Interviewer Email");
                return;
            }

            if (string.IsNullOrWhiteSpace(grade.Text))
            {
                await App.DisplayAlert("Please enter Grade");
                return;
            }

            if (string.IsNullOrWhiteSpace(Attendee_Name.Text))
            {
                await App.DisplayAlert("Please enter Attendee Name");
                return;
            }

            if (string.IsNullOrWhiteSpace(title.Text))
            {
                await App.DisplayAlert("Please enter title");
                return;
            }

            Meeting meetingDetails = new Meeting()
            {
                OrganiserName = Organiser_Name.Text,
                InterviewerName = Interviewer_Name.Text,
                Email = Interviewer_Email.Text,
                Grade=grade.Text,
                AttendeeName= Attendee_Name.Text,
                Title= title.Text,
                Date= datePicker.Date,
                StartTime= starttimePicker.Time,
                EndTime=EndtimePicker.Time,

            };

            FirebaseHelper helper = new FirebaseHelper();
            await helper.AddMeeting(meetingDetails);

            DisplayAlert("Success","Details Added Successfully","OK");



            //To Assing the firbase data to the added event to dispaly in calendar page
            var mDetails=await helper.GetMeetings();
            foreach (var item in mDetails)
            {
                Added?.Invoke(new Meeting
                {
                    OrganiserName = item.OrganiserName,
                    InterviewerName = item.InterviewerName,
                    Email = item.Email,
                    Grade = item.Grade,
                    AttendeeName = item.AttendeeName,
                    Title = item.Title,
                    Date = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,

                });
            }


            await Navigation.PopModalAsync();

        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PopModalAsync();



        }

      
    }
}