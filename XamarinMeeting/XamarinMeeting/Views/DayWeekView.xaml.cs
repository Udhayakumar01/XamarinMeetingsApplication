using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;

namespace XamarinMeeting.Views
{
    //[DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class DayWeekView : ContentPage
    {
        List<Meeting> meetingsData = new List<Meeting>();
        List<Meeting> AllMeetings = new List<Meeting>();

        DateTime today = DateTime.Today;
        DateTime endMonth;
        List<String> Month = new List<string>(new String[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" });

        public DayWeekView()

        {
            InitializeComponent();
           
            StartDate.Text = Convert.ToString(today.Day);
            EndDate.Text = Convert.ToString(today.Day + 6);
            for (int i = 1; i < today.Month; i++)
            {
                StartMonth.Text = Month[i];
            }
            endMonth = DateTime.Today;
            endMonth = endMonth.AddDays(7);
            for (int i = 1; i < endMonth.Month; i++)
            {
                EndMonth.Text = Month[i];
            }
            
            FirebaseHelper helper = new FirebaseHelper();
            meetingsData =  Task.Run(async()=> await helper.GetMeetings()).Result;
            
            foreach (var item in meetingsData)
            {
                AllMeetings.Add(item);
            }
            //GetMeetingDetails();
            this.BindingContext = this;

        }

        /*async public void  GetDetails()
        {
            FirebaseHelper helper = new FirebaseHelper();
            //var meetingData = await Task.Run(()=> helper.GetMeetings()).ConfigureAwait(false);
            var meetingData = await helper.GetMeetings().ConfigureAwait(false);
            foreach (var item in meetingData)
            {
                AllMeetings.Add(item);
            }

            //this.BindingContext = AllMeetings;
            // AllMeetings = await helper.GetMeetings();
        }*/
        public ObservableCollection<MeetingDetails> meetingDetails { get => GetMeetingDetails(); }

        public ObservableCollection<MeetingDetails> GetMeetingDetails()
        {

            List<MeetingDetails> meetingDetailsList = new List<MeetingDetails>();
            DateTime day = DateTime.Today;
            for (int i = 1; i <= 7; i++)
            {
                bool check = true;
                MeetingDetails meetingDetails;
                
                foreach (var item in AllMeetings)
                {
                    
                    meetingDetails = new MeetingDetails();
                    meetingDetails.Date = day;
                    if (day == item.Date)
                    {
                            check = false;
                            meetingDetails.Topic = item.Title;
                        meetingDetails.Organizer = item.OrganiserName;
                        meetingDetails.Attendees = new ObservableCollection<Attendee> { 
                            new Attendee { 
                                Name = item.AttendeeName, 
                                StartTime=item.StartTime, 
                                EndTime = item.EndTime} };
                            //meetingDetails.Date = item.Date;
                            if (i == 1)
                                meetingDetails.Color = "#B96CBD";
                            if (i == 2)
                                meetingDetails.Color = "#49A24D";
                            if (i == 3)
                                meetingDetails.Color = "#FDA838";
                            if (i == 4)
                                meetingDetails.Color = "#F75355";
                            if (i == 5)
                                meetingDetails.Color = "#00C6AE";
                            if (i == 6)
                                meetingDetails.Color = "#455399";
                            if (i == 7)
                                meetingDetails.Color = "#8B0000";
                            meetingDetailsList.Add(meetingDetails);
                        
                    }
                    
                    

                    // MeetingDetails meetingDetails = new MeetingDetails();

                    //meetingDetails.Attendees = new ObservableCollection<Attendee> { };
                }
                if (check)
                {
                    meetingDetails = new MeetingDetails();
                    meetingDetails.Date = day;
                    meetingDetails.Topic = "No Interviews Scheduled";
                    //meetingDetails.Date = day;
                    if (i == 1)
                        meetingDetails.Color = "#B96CBD";
                    if (i == 2)
                        meetingDetails.Color = "#49A24D";
                    if (i == 3)
                        meetingDetails.Color = "#FDA838";
                    if (i == 4)
                        meetingDetails.Color = "#F75355";
                    if (i == 5)
                        meetingDetails.Color = "#00C6AE";
                    if (i == 6)
                        meetingDetails.Color = "#455399";
                    if (i == 7)
                        meetingDetails.Color = "#8B0000";
                    meetingDetailsList.Add(meetingDetails);
                }
                day = day.AddDays(1);
                
                    //meetingDetails.Topic = "No Meetings scheduled";
                    //meetingDetailsList.Add(meetingDetails);
                
            }
            ObservableCollection<MeetingDetails> meetingDetailsCollection = new ObservableCollection<MeetingDetails>(meetingDetailsList);

            return meetingDetailsCollection;
        }
        
    }
    public class MeetingDetails
    {
        public string Topic { get; set; }
        public string Organizer { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        public ObservableCollection<Attendee> Attendees { get; set; }



    }

    public class Attendee
    {
        public string Name { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }


    }
}