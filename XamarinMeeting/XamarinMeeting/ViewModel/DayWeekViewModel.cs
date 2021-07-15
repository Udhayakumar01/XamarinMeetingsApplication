using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;

namespace XamarinMeeting.ViewModel
{
    class DayWeekViewModel
    {
        DateTime today = DateTime.Today;

        List<Meeting> AllMeetings = new List<Meeting>();
        List<MeetingDetails> meetingDetailsList;

        /*async public void GetDetails()
        {
            FirebaseHelper helper = new FirebaseHelper();
               var meetingData  = await helper.GetMeetings();
            foreach(var item in meetingData)
            {
                AllMeetings.Add(item);
            }
            
           // AllMeetings = await helper.GetMeetings();
        }*/
        //public ObservableCollection<MeetingDetails> meetingDetails { get => GeMeetingDetails(); }
        public ObservableCollection<MeetingDetails> meetingDetailsCollection { get; set; }
         public  DayWeekViewModel()
        {
            FirebaseHelper helper = new FirebaseHelper();
            AllMeetings = helper.GetMeetings().Result;
            foreach (Meeting item in AllMeetings)
            {
                AllMeetings.Add(item);
            }
            GetMeetingDetails();
        }
        public void GetMeetingDetails()
        {
            
            meetingDetailsList = new List<MeetingDetails>();
            foreach (var item in AllMeetings)
            {
                MeetingDetails meetingDetails = new MeetingDetails();
                if (item.Date == today)
                {

                    for (int i = 1; i <= 7; i++)
                    {
                        meetingDetails.Topic = item.Title;
                        meetingDetails.Date = item.Date;
                        today.AddDays(1);
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
                            meetingDetails.Color = "";
                        meetingDetailsList.Add(meetingDetails);
                    }

                }
                else
                {
                    meetingDetails.Topic = "No Meetings scheduled";
                }
                // MeetingDetails meetingDetails = new MeetingDetails();

                //meetingDetails.Attendees = new ObservableCollection<Attendee> { };
            }
            meetingDetailsCollection = new ObservableCollection<MeetingDetails>(meetingDetailsList);

        }

    }
    public class MeetingDetails
    {
        public string Topic { get; set; }
        public string Color { get; set; }
        public DateTime Date { get; set; }
        //public ObservableCollection<Attendee> Attendees { get; set; }
    }
}
