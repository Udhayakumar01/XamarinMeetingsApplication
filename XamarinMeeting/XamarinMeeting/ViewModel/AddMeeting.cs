using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Plugin.Calendar.Models;
using XamarinMeeting.Helper;
using XamarinMeeting.Model;
using XamarinMeeting.Views;

namespace XamarinMeeting.ViewModel
{
	public class AddMeeting:BaseNotifyPropertyChanged
	{
		DateTime _selectedDate;
		EventCollection _events;
		public Action<Meeting> Added { get; set; }
		public ICommand AppointmentAddCommand { get; private set; }
		public ICommand AppointmentDeleteCommand { get; private set; }




		public ICommand EventSelectedCommand =>
			new Command(async (item) => await ExecuteEventSelectedCommand(item));





		public AddMeeting()
		{
		     DisplayMeetingDetails();
			 SelectedDate = DateTime.Now;
			 Events = new EventCollection();

			AppointmentAddCommand = new Command(TapOnAdd);
			AppointmentDeleteCommand = new Command(TapOnDelete);

			
		}


		


		private async Task ExecuteEventSelectedCommand(object item)
		{
			if (item is Meeting meeting)
			{
				var events = Events[SelectedDate] as ObservableCollection<Meeting>;
				var page = new UpdateMeeting(meeting);
				await App.PushModalAsync(page);
			}
		}





		private async void TapOnAdd(object obj)
		{
		   var page = new AddPage(SelectedDate);

			page.Added+=(appointment) => {
				if (Events.ContainsKey(appointment.Date))
				{
					var events = Events[appointment.Date] as ObservableCollection<Meeting>;
					events.Add(appointment);
				}
				else
				{
					Events.Add(appointment.Date, new ObservableCollection<Meeting> {appointment });
				}
			};

			await App.PushModalAsync(page);
		}

		async public void DisplayMeetingDetails()
		{
			FirebaseHelper helper = new FirebaseHelper();
			var mDetails = await helper.GetMeetings();
			List<Meeting> meetingDetailsList = mDetails;
			foreach (var item in meetingDetailsList)
			{
				if (Events.ContainsKey(item.Date))
				{
					var events = Events[item.Date] as ObservableCollection<Meeting>;
					events.Add(item);
				}
				else
				{
					Events.Add(item.Date, new ObservableCollection<Meeting> { item });
				}
			}

		}


		private async void TapOnDelete(object obj)
		{
			if (!Events.Any())
			{
				return;
			}

			if (Events[SelectedDate].Count == 1)
			{
				Events.Remove(SelectedDate);
				
				FirebaseHelper helper = new FirebaseHelper();
				
		        await helper.DeleteSingleMeetings(SelectedDate);

			App.DisplayAlert("Meeting Details deleted Successfully");

			}

			else
			{
				var events = Events[SelectedDate] as ObservableCollection<Meeting>;
				var page = new DeletePage(events);

				page.Deleted+= (appointments) => {
					if (appointments.Count == 0)
					{
						Events.Remove(SelectedDate);
					}
					else
					{
						Events[SelectedDate] = appointments;
					}
				};

				await App.PushModalAsync(page);
			}
		}

		public DateTime SelectedDate
		{
			get => _selectedDate;
			set
			{
				_selectedDate = value;
				OnPropertyChanged();
			}
		}

		public EventCollection Events
		{
			get => _events;
			set
			{
				_events = value;
				OnPropertyChanged();
			}
		}

        //public ICommand EventSelectedCommand { get => eventSelectedCommand; set => eventSelectedCommand = value; }
    }
}
