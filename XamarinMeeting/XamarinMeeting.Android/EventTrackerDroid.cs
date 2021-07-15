using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace XamarinMeeting.Droid
{
  public class EventTrackerDroid:IEventTrackerDroid
    {
        public void SendEvent(string eventId)
        {
            SendEvent(eventId, null);
        }

        public void SendEvent(string eventId, string paramName, string value)
        {
            SendEvent(eventId, new Dictionary<string, string>
            {
                {paramName, value}
            }); 
        }

        public void SendEvent(string eventId, IDictionary<string, string> parameters)
        {
           // var firebaseAnalytics = FirebaseAnalytics.GetInstance(Forms.Context);

            if (parameters == null)
            {
                //firebaseAnalytics.LogEvent(eventId, null);
                return;
            }

            var bundle = new Bundle();
            foreach (var param in parameters)
            {
                bundle.PutString(param.Key, param.Value);
            }

            //firebaseAnalytics.LogEvent(eventId, bundle);
        }
    }
}