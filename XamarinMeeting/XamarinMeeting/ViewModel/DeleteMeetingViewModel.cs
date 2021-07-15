using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using XamarinMeeting.Model;

namespace XamarinMeeting.ViewModel
{
   public class DeleteMeetingViewModel:BaseNotifyPropertyChanged
		{
		ObservableCollection<Meeting> _meetings;

		public DeleteMeetingViewModel(ObservableCollection<Meeting> meetings)
		{
			Meetings = meetings;
		}

		public ObservableCollection<Meeting> Meetings
		{
			get => _meetings;
			set
			{
				_meetings = value;
				OnPropertyChanged();
			}
		}
	}
}
