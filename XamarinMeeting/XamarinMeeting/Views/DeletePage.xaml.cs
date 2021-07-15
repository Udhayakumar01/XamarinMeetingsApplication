using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DeletePage : ContentPage
    {
		DeleteMeetingViewModel viewModel;

		public Action<ObservableCollection<Meeting>> Deleted { get; set; }

		public DeletePage(ObservableCollection<Meeting> appointmentList)
		{
			InitializeComponent();
			BindingContext = viewModel = new DeleteMeetingViewModel(appointmentList);
		}

		void ToolbarItem_Clicked(object sender, EventArgs e)
		{
			Navigation.PopModalAsync();
		}

		void Delete_Clicked(object sender, EventArgs e)
		{
			var selectedList = viewModel.Meetings.Where(x => x.IsSelected).ToList();
			FirebaseHelper helper = new FirebaseHelper();
		
			foreach (var item in selectedList)
			{
				helper.DeleteMeetingDetails(item.Title, item.Date);
				viewModel.Meetings.Remove(item);
			}

			Deleted?.Invoke(viewModel.Meetings);
			Navigation.PopModalAsync();
		}
	}
}