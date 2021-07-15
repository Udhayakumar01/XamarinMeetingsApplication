using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinMeeting
{
   public interface IEventTrackerDroid
    {
        void SendEvent(string eventId);
        void SendEvent(string eventId, string paramName, string value);
        void SendEvent(string eventId, IDictionary<string, string> parameters);
    }
}
