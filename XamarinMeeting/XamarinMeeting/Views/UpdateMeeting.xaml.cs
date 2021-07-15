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
    public partial class UpdateMeeting : ContentPage
    {
        
            public UpdateMeeting(Meeting meeting)
            {
                InitializeComponent();



                Organiser_Name.Text = meeting.OrganiserName;
                Interviewer_Name.Text = meeting.InterviewerName;
                Attendee_Name.Text = meeting.AttendeeName;
                Interviewer_Email.Text = meeting.Email;
                title.Text = meeting.Title;
                grade.Text = meeting.Grade;
                datePicker.Date = meeting.Date;
                starttimePicker.Time = meeting.StartTime;
                EndtimePicker.Time = meeting.EndTime;




            }

        private async void UpdateMeetingDetails_Clicked(object sender, EventArgs e)
        {
            Meeting updateMeeting = new Meeting()
            {
                OrganiserName =Organiser_Name.Text,
                InterviewerName = Interviewer_Name.Text,
                AttendeeName = Attendee_Name.Text,
                Email = Interviewer_Email.Text,
                Title = title.Text,
                Grade = grade.Text,
                Date = datePicker.Date,
                StartTime = starttimePicker.Time,
                EndTime = EndtimePicker.Time,
            };

            FirebaseHelper helper = new FirebaseHelper();
            var isUpdated=helper.UpdateMeeting(updateMeeting);
            if (await isUpdated.ConfigureAwait(true))
            {
                DisplayAlert("Success", "Meeting Details Updated successfuly", "OK");
            }
            else
            {
                DisplayAlert("Failed", "Meeting Details failed to update", "OK");
                return;
            }




        }
    }
    }
