using Firebase.Database;
using Firebase.Database.Query;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinMeeting.Model;

namespace XamarinMeeting.Helper
{
    public class FirebaseHelper
    {
        private readonly string LoginDetails = "Login Details";
        private readonly string MeetingDetails = "MeetingDetails";
        readonly FirebaseClient firebase = new FirebaseClient("https://meeting-df4bb-default-rtdb.firebaseio.com/");

        public async Task AddLoginCredintials(string email, string pass, string role, string technology)
        {
            //Register
            /*   await firebase.Child(LoginDetails)
              .PostAsync(new Users(){Email=email,Password=pass,Role=role,Technology=technology});*/

        }

        public async Task<List<Users>> GetLoginCrediantls()
        {
            
           // return (await firebase.Child(LoginDetails).OnceAsync<JObject>()).ToList();
            
            return (await firebase.Child(LoginDetails)
                  .OnceAsync<Users>())
                  .Select(item => new Users
                   {
                       Name = item.Object.Name,
                       Email = item.Object.Email,
                       Password = item.Object.Password,
                       Role = item.Object.Role,
                       Technology = item.Object.Technology,


                   }).ToList();

           /* return (await firebase.Child(LoginDetails)
                  .OnceAsync<JObject>())
                  .Select(item => new Users
                  {
                      Name=item.Object.GetValue("Name").ToString(),
                      Email = item.Object.GetValue("Email").ToString(),
                      Password=item.Object.GetValue("Pasword").ToString(),
                      Role=item.Object.GetValue("Role").ToString(),
                      Technology=item.Object.GetValue("Technology").ToString(),


                  }).ToList<Users>();*/

        }

        //To Search a person using Credentials
        public async Task<Users> GetUser(string email, string password)
        {
            var allPersons = await GetLoginCrediantls();
            await firebase
                .Child(LoginDetails)
                .OnceAsync<Users>();
            return allPersons.FirstOrDefault(a => a.Email == email && a.Password == password);
        }

        //To Retrive Particular meeting details using Date
        public async Task<List<Meeting>> GetMeetings()
        {
            return (await firebase.Child(MeetingDetails)
                  .OnceAsync<Meeting>())
                  .Select(item => new Meeting
                  {
                      OrganiserName = item.Object.OrganiserName,
                      InterviewerName = item.Object.InterviewerName,
                      Email = item.Object.Email,
                      Grade = item.Object.Grade,
                      AttendeeName = item.Object.AttendeeName,
                      Title = item.Object.Title,
                      Date = item.Object.Date,
                      StartTime = item.Object.StartTime,
                      EndTime = item.Object.EndTime,


                  }).ToList();

        }




        public async Task<Meeting> GetMeetingDetails(DateTime date)
        {
            var allMeetings = await GetMeetings();
            await firebase
               .Child(MeetingDetails)
               .OnceAsync<Meeting>();
            return allMeetings.FirstOrDefault(a => a.Date == date);
        }

        //To Delete Meeting Details
        public async Task DeleteMeetingDetails(string title, DateTime date)
        {
            var toDeleteMeetingDetails = (await firebase
               .Child(MeetingDetails)
               .OnceAsync<Meeting>()).FirstOrDefault(a => a.Object.Date == date && a.Object.Title == title);
            await firebase.Child(MeetingDetails).Child(toDeleteMeetingDetails.Key).DeleteAsync();
        }





        //To Add meeting details
        public async Task AddMeeting(Meeting meetingDetails)
        {
            // Meeting newmeetingDetails =meetingDetails;
            await firebase.Child(MeetingDetails).
                PostAsync(meetingDetails);
        }

        //To Delete Meeting Details single meeting
        public async Task DeleteSingleMeetings(DateTime date)
        {
            var toDeleteMeetingDetails = (await firebase
               .Child(MeetingDetails)
               .OnceAsync<Meeting>()).FirstOrDefault(a => a.Object.Date == date);
            await firebase.Child(MeetingDetails).Child(toDeleteMeetingDetails.Key).DeleteAsync();
        }



        //To Udate Meeting Details
        public async Task<bool> UpdateMeeting(Meeting meeting)
        {
            var toUpdateMeeting = (await firebase
                .Child(MeetingDetails)
                .OnceAsync<Meeting>()).
                FirstOrDefault(a => a.Object.Title == meeting.Title && a.Object.OrganiserName == meeting.OrganiserName);
            if (toUpdateMeeting != null)
            {

                await firebase
                    .Child(MeetingDetails)
                    .Child(toUpdateMeeting.Key)
                    .PutAsync(new Meeting()
                    {

                        OrganiserName = meeting.OrganiserName,
                        Title = meeting.Title,
                        AttendeeName = meeting.AttendeeName,
                        Email = meeting.Email,
                        Grade = meeting.Grade,
                        StartTime = meeting.StartTime,
                        EndTime = meeting.EndTime,
                        Date = meeting.Date,
                        InterviewerName = meeting.InterviewerName,

                    });
                return true;
            }
            else
            {
                return false;
            }




        }



    }

}
